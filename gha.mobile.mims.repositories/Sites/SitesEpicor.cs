using System;
using System.Collections.Generic;
using EpicorRestAPI;
using gha.mobile.common.helpers;
using gha.mobile.mims.entities;
using LicenseServerHelper;
using Newtonsoft.Json.Linq;

namespace gha.mobile.mims.repositories
{
    public class SitesEpicor : Epicor, ISites
    {
        public List<Site> GetSites(Employee employee)
        {
            List<Site> sites = new List<Site>();

            try
            {
                EpicorRest.AppPoolHost = settings.ERPServer;
                EpicorRest.AppPoolInstance = settings.ERPPDatabase;
                EpicorRest.UserName = settings.ERPUserName;
                EpicorRest.Password = Secret.DecryptText(settings.ERPPassword, ststen.outr());
                EpicorRest.IgnoreCertErrors = true;
                EpicorRest.License = (string.Compare(settings.ERPLicense, "Web") == 0) ? EpicorLicenseType.WebService : EpicorLicenseType.Default;
                EpicorRest.CallSettings = new CallSettings(settings.ERPCompany, string.Empty, string.Empty, string.Empty);
                
                var Company = settings.ERPCompany;

                using (EpicorSession sess = new EpicorSession())
                {
                    Dictionary<string, string> GetEmployeeData = new Dictionary<string, string>();
                    
                    GetEmployeeData.Add("$filter", $"EmpID eq '{employee.EmployeeID}'");

                    dynamic EmployeeResult = EpicorRest.DynamicGet("Erp.BO.EmpBasicSvc", $"EmpBasics({Company}, {employee.EmployeeID})", GetEmployeeData, statusCode, callContextHeader);
                    
                    if (!EmployeeResult.ContainsKey("GHA_Plants_c"))
                    {
                        sites.Add(new Site(){ SiteId = "MfgSys", SiteName = "Main"});
                        return sites;
                    }

                    string filter = EmployeeResult.GHA_Plants_c;
                    filter = filter.ParsePlantsForFilter();
                    
                    Dictionary<string, string> GetSiteData = new Dictionary<string, string>();
                    
                    GetSiteData.Add("$select", "Plant1, Name");
                    GetSiteData.Add("$filter", filter);
                    GetSiteData.Add("$orderby", "Name");

                    JObject PlantResult = EpicorRest.DynamicGet("Erp.BO.PlantSvc", $"Plants()", GetSiteData, statusCode, callContextHeader);

                    JArray PlantArray = (JArray)PlantResult["value"];
                    dynamic PlantDynamic = PlantArray;

                    foreach (dynamic value in PlantDynamic)
                    {
                        Site site = new Site();

                        site.SiteId = value.Plant1;
                        site.SiteName = value.Name;

                        sites.Add(site);
                    }
                }
            }
            catch (Exception ex)
            {
                var except = ex;
            }
            return sites;
        }


    }
}
