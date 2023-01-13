using System;
using System.Collections.Generic;
using System.Linq;
using EpicorRestAPI;
using gha.mobile.common.repositories;
using gha.mobile.mims.entities;
using LicenseServerHelper;
using Newtonsoft.Json.Linq;

namespace gha.mobile.mims.repositories
{
    public class WarehousesEpicor : Epicor, IWarehouses
    {
        public List<Warehouse> Get()
        {
            List<Warehouse> warehouses = new List<Warehouse>();

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

                    PostData.Add("whereClause", $"");
                    PostData.Add("pageSize", $"0");
                    PostData.Add("absolutePage", $"0");

                    JObject WhseResult = EpicorRest.DynamicPost("Erp.BO.WarehseSearchSvc", $"GetList()", PostData, true,
                        statusCode, callContextHeader);

                    JArray WhseArray = (JArray)WhseResult["ds"]["WarehseSearchList"];
                    dynamic WhseDynamic = WhseArray;

                    foreach (dynamic value in WhseDynamic)
                    {
                        // Ignore results that are not a part of this plant
                        if (value.Plant != EpicorRest.CallSettings.Plant)
                            continue;

                        Warehouse whse = new Warehouse();

                        whse.Plant = value.Plant;
                        whse.WarehouseCode = value.WarehouseCode;
                        whse.Description = value.Description;

                        warehouses.Add(whse);
                    }
                }
            }
            catch (Exception ex)
            {
                var except = ex;
            }

            return warehouses;
        }
    }
}