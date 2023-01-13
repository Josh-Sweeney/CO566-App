using System;
using System.Collections.Generic;
using EpicorRestAPI;
using gha.mobile.mims.entities;
using LicenseServerHelper;
using Newtonsoft.Json.Linq;

namespace gha.mobile.mims.repositories
{
    public class BinsEpicor : Epicor, IBins
    {
        public List<Bin> Get(Warehouse warehouse)
        {
            if (TryGetBinsViaBAQ(warehouse.WarehouseCode, out var bins).Success)
                return bins;

            if (TryGetBinsViaREST(warehouse, out bins).Success)
                return bins;

            return new List<Bin>();
        }

        public Bin Validate(string warehouse, string bin)
        {
            try
            {
                EpicorRest.AppPoolHost = settings.ERPServer;
                EpicorRest.AppPoolInstance = settings.ERPPDatabase;
                EpicorRest.UserName = settings.ERPUserName;
                EpicorRest.Password = Secret.DecryptText(settings.ERPPassword, ststen.outr());
                EpicorRest.IgnoreCertErrors = true;
                EpicorRest.License = (string.Compare(settings.ERPLicense, "Web") == 0)
                    ? EpicorLicenseType.WebService
                    : EpicorLicenseType.Default;
                EpicorRest.CallSettings =
                    new CallSettings(settings.ERPCompany, settings.Plant, string.Empty, string.Empty);

                using (EpicorSession sess = new EpicorSession())
                {
                    dynamic whseResult = EpicorRest.DynamicGet("Erp.BO.WhseBinSvc",
                        $"WhseBins({settings.ERPCompany}, {warehouse}, {bin})", null, statusCode, callContextHeader);

                    if (whseResult != null)
                    {
                        return new Bin()
                        {
                            Plant = whseResult.WarehouseCodePlant,
                            WarehouseCode = whseResult.WarehouseCode,
                            WarehouseDescription = whseResult.WarehouseCodeDescription,
                            BinNum = whseResult.BinNum,
                            Description = whseResult.Description
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                var except = ex;
            }

            return new Bin();
        }

        public List<Bin> GetStocktake(Warehouse warehouse, string countID)
        {
            List<Bin> bins = new List<Bin>();

            try
            {
                EpicorRest.AppPoolHost = settings.ERPServer;
                EpicorRest.AppPoolInstance = settings.ERPPDatabase;
                EpicorRest.UserName = settings.ERPUserName;
                EpicorRest.Password = Secret.DecryptText(settings.ERPPassword, ststen.outr());
                EpicorRest.IgnoreCertErrors = true;
                EpicorRest.License = (string.Compare(settings.ERPLicense, "Web") == 0)
                    ? EpicorLicenseType.WebService
                    : EpicorLicenseType.Default;
                EpicorRest.CallSettings =
                    new CallSettings(settings.ERPCompany, settings.Plant, string.Empty, string.Empty);

                using (EpicorSession sess = new EpicorSession())
                {
                    Dictionary<string, string> GetData = new Dictionary<string, string>();

                    GetData.Add("$select", $"WarehouseCode, WhseDescDescription, BinNum, BinDescDescription, Open");
                    GetData.Add("$filter", $"WarehouseCode eq '{warehouse.WarehouseCode}'");

                    JObject BinResult = EpicorRest.DynamicGet("Erp.BO.GHACountProcessingSvc",
                        $"GHACountProcessings({settings.ERPCompany},{countID})/GHACountBins", GetData, statusCode,
                        callContextHeader);

                    JArray BinArray = (JArray)BinResult["value"];
                    dynamic BinDynamic = BinArray;

                    foreach (dynamic value in BinDynamic)
                    {
                        if (value.WarehouseCode == warehouse.WarehouseCode && value.Open == "true")
                        {
                            Bin bin = new Bin();

                            bin.WarehouseCode = value.WarehouseCode;
                            bin.WarehouseDescription = value.WhseDescDescription;
                            bin.BinNum = value.BinNum;
                            bin.Description = value.BinDescDescription;

                            bins.Add(bin);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var except = ex;
            }

            return bins;
        }

        private EpicorResponse TryGetBinsViaBAQ(string warehouseCode, out List<Bin> outputBins)
        {
            var request = new Dictionary<string, string>
            {
                { "WarehouseCode", warehouseCode }
            };

            var response = EpicorRest.DynamicGet("BaqSvc", "GHA_MIMS_Bins", request, statusCode, callContextHeader);

            outputBins = new List<Bin>();

            if (response.ContainsKey("ErrorMessage"))
                return new EpicorResponse { Success = false, ErrorMessage = response.ErrorMessage };

            foreach (var value in response.value)
            {
                // Ignore results that are not a part of this plant
                if (value.Calculated_Plant != EpicorRest.CallSettings.Plant)
                    continue;

                outputBins.Add(new Bin
                {
                    Plant = value.Calculated_Plant,
                    WarehouseCode = value.Calculated_WarehouseCode,
                    WarehouseDescription = value.Calculated_WarehouseDescription,
                    BinNum = value.Calculated_BinNum,
                    Description = value.Calculated_BinDescription,
                    BinType = value.BinType,
                    VendorVendorID = value.Calculated_VendorID
                });
            }

            return new EpicorResponse { Success = true };
        }

        private EpicorResponse TryGetBinsViaREST(Warehouse warehouse, out List<Bin> outputBins)
        {
            var bins = new List<Bin>();

            try
            {
                var getData = new Dictionary<string, string>
                {
                    { "$filter", $"WarehouseCode eq '{warehouse.WarehouseCode}'" },
                    { "$top", "100000" }
                };

                JObject BinResult = EpicorRest.DynamicGet("Erp.BO.WhseBinSvc", "WhseBins", getData, statusCode,
                    callContextHeader);

                var BinArray = (JArray)BinResult["value"];
                dynamic BinDynamic = BinArray;

                if (BinDynamic is null)
                {
                    outputBins = new List<Bin>();
                    return new EpicorResponse { Success = false, ErrorMessage = "No Bins Found" };
                }

                foreach (var value in BinDynamic)
                {
                    // Ignore results that are not a part of this plant
                    if (value.WarehouseCodePlant != EpicorRest.CallSettings.Plant)
                        continue;

                    var bin = new Bin
                    {
                        Plant = warehouse.Plant,
                        WarehouseCode = warehouse.WarehouseCode,
                        WarehouseDescription = warehouse.Description,
                        BinNum = value.BinNum,
                        Description = value.Description,
                        BinType = value.BinType,
                        VendorVendorID = value.VendorVendorID
                    };

                    bins.Add(bin);
                }
            }
            catch (Exception ex)
            {
                var except = ex;
            }

            outputBins = bins;

            return new EpicorResponse { Success = true };
        }
    }
}