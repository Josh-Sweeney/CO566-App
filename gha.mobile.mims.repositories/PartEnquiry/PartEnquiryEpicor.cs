using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EpicorRestAPI;
using gha.mobile.mims.entities;
using LicenseServerHelper;
using Newtonsoft.Json.Linq;

namespace gha.mobile.mims.repositories
{
    public class PartEnquiryEpicor : Epicor, IPartEnquiry
    {
        public List<PartBin> Get(Part part, ObservableCollection<PartBin> PartBins)
        {
            List<PartBin> partBins = new List<PartBin>();

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
                    Dictionary<string, string> PostData = new Dictionary<string, string>();

                    PostData.Add("partNum", $"{part.PartNum}");
                    PostData.Add("whseCode", string.Empty);
                    PostData.Add("consolidateInvAttributes", "true");
                    PostData.Add("$orderby", "WhseCode, BinNum");

                    JObject PartResult = EpicorRest.DynamicPost("Erp.BO.PartBinSearchSvc", $"GetFullBinSearch()",
                        PostData, true, statusCode, callContextHeader);

                    JArray PartArray = (JArray)PartResult["ds"]["PartBinSearch"];
                    dynamic PartDynamic = PartArray;

                    foreach (dynamic value in PartDynamic)
                    {
                        // Ignore results that are not a part of this plant
                        if (value.Plant != EpicorRest.CallSettings.Plant)
                            continue;

                        PartBin partBin = new PartBin();

                        partBin.Warehouse = new Warehouse
                            { WarehouseCode = value.WhseCode, Description = value.WhseCodeDesc };
                        partBin.Bin = new Bin
                        {
                            WarehouseCode = value.WhseCode, WarehouseDescription = value.WhseCodeDesc,
                            BinNum = value.BinNum, Description = value.BinDesc
                        };
                        partBin.LotNum = value.LotNumber;
                        partBin.ShowLot = part.LotTracked;
                        partBin.Quantity = value.QtyOnHand;

                        if (PartBins != null)
                        {
                            var bin = PartBins
                                .Where(r => r.Bin.WarehouseCode == partBin.Bin.WarehouseCode)
                                .Where(r => r.Bin.BinNum == partBin.Bin.BinNum)
                                .FirstOrDefault();
                            if (bin != null) partBin.SelectedQuantity = bin.SelectedQuantity;
                        }

                        partBin.UOM = value.IUM;

                        partBins.Add(partBin);
                    }
                }
            }
            catch (Exception ex)
            {
                var except = ex;
            }

            return partBins;
        }
    }
}