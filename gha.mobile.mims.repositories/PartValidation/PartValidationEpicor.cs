using System;
using System.Collections.Generic;
using EpicorRestAPI;
using gha.mobile.mims.entities;
using LicenseServerHelper;
using Newtonsoft.Json.Linq;

namespace gha.mobile.mims.repositories
{
	public class PartValidationEpicor : Epicor, IPartValidation
	{
		public PartValidationEpicor() : base() { }

		public Part Get(string partnum)
		{
			Part part = new Part();
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

					PostData.Add("$select", "PartNum, PartDescription, TrackLots, TrackSerialNum, IUM, QtyBearing");
					PostData.Add("$filter", $"PartNum eq '{partnum}'");

					JObject PartResult = EpicorRest.DynamicGet("Erp.BO.PartSvc", $"Parts", PostData, statusCode,
						callContextHeader);

					part.PartNum = PartResult.SelectToken("value[0].PartNum").ToString();
					part.Description = PartResult.SelectToken("value[0].PartDescription").ToString();
					part.LotTracked = (bool)PartResult.SelectToken("value[0].TrackLots");
					part.SerialTracked = (bool)PartResult.SelectToken("value[0].TrackSerialNum");
					part.UOM = PartResult.SelectToken("value[0].IUM").ToString();
					part.QtyBearing = (bool)PartResult.SelectToken("value[0].QtyBearing");

					//PostData.Clear();
					//PostData.Add("PartNumber", $"{partnum}");

					//JObject GetResult = EpicorRest.DynamicPost("Erp.BO.PartSvc", $"PartExists()", PostData, true, statusCode, callContextHeader);

					//bool result = (bool)GetResult["ds"];

					//if (result == false) return part;

					//PostData.Clear();
					//PostData.Add("partNum", $"{partnum}");

					//JObject PartResult = EpicorRest.DynamicPost("Erp.BO.PartSvc", $"GetByID()", PostData, true, statusCode, callContextHeader);

					//part.PartNum = PartResult.SelectToken("ds.Part[0].PartNum").ToString();
					//part.Description = PartResult.SelectToken("ds.Part[0].PartDescription").ToString();
					//part.LotTracked = (bool)PartResult.SelectToken("ds.Part[0].TrackLots");
					//part.UOM = PartResult.SelectToken("ds.Part[0].IUM").ToString();

					return part;
				}
			}
			catch (Exception ex)
			{
				var except = ex;
			}

			return part;
		}
	}
}
