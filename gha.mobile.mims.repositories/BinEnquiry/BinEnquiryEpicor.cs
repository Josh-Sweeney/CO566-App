using System;
using System.Collections.Generic;
using EpicorRestAPI;
using gha.mobile.common.helpers;
using gha.mobile.mims.entities;
using LicenseServerHelper;
using Newtonsoft.Json.Linq;

namespace gha.mobile.mims.repositories
{
    public class BinEnquiryEpicor : Epicor, IBinEnquiry
    {
        public List<BinPart> Get(string warehousecode, string binnum)
        {
            List<BinPart> binParts = new List<BinPart>();

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

                    //    "whseCode": "W1",
                    //    "binNum": "A01-01-01"


                    PostData.Add("whseCode", $"{warehousecode}");
                    PostData.Add("binNum", $"{binnum}");

                    JObject BinResult = EpicorRest.DynamicPost("Erp.BO.PartBinSearchSvc", $"GetBinContents()", PostData,
                        true, statusCode, callContextHeader);

                    JArray BinArray = (JArray)BinResult["ds"]["PartBinSearch"];
                    dynamic BinDynamic = BinArray;

                    foreach (dynamic value in BinDynamic)
                    {
                        // Ignore results that are not a part of this plant
                        if (value.Plant != EpicorRest.CallSettings.Plant)
                            continue;

                        BinPart binPart = new BinPart();

                        var partnum = value.PartNum;

                        Dictionary<string, string> GetData = new Dictionary<string, string>();

                        GetData.Add("$select", $"PartDescription, TrackLots");
                        GetData.Add("$filter", $"PartNum eq '{partnum}'");
                        var PartResult = EpicorRest.DynamicGet("Erp.BO.PartSvc", $"Parts", GetData, statusCode,
                            callContextHeader);

                        var partDescription = PartResult["value"][0]["PartDescription"].ToString();
                        var trackLots = (bool)PartResult["value"][0]["TrackLots"];

                        Logger.Log("PartDescription");

                        binPart.Part = new Part
                            { PartNum = partnum, Description = partDescription, LotTracked = trackLots };
                        binPart.LotNum = value.LotNumber;
                        binPart.UOM = value.IUM;
                        binPart.ShowLot = trackLots;
                        binPart.Quantity = value.QtyOnHand;

                        binParts.Add(binPart);
                    }
                }
            }
            catch (Exception ex)
            {
                var except = ex;
            }

            return binParts;
        }
    }
}