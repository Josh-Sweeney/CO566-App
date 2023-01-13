using System;
using EpicorRestAPI;
using gha.mobile.common.entities;
using gha.mobile.common.helpers;
using gha.mobile.common.repositories;
using LicenseServerHelper;

namespace gha.mobile.mims.repositories
{
    public class Epicor
    {
        protected CallContextHeader callContextHeader = new CallContextHeader();
        protected SettingsRepository settings = new SettingsRepository();
        protected EpicorRestStatusCode statusCode = new EpicorRestStatusCode();

        public Epicor()
        {
            _ = SetupEpicorConnection();
        }

        public Epicor(ERPConnection epicorConnection)
        {
            EpicorRest.AppPoolHost = epicorConnection.AppPoolHost;
            EpicorRest.AppPoolInstance = epicorConnection.AppPoolInstance;
            EpicorRest.UserName = epicorConnection.UserName;
            EpicorRest.Password = epicorConnection.Password;
            EpicorRest.IgnoreCertErrors = true;
            EpicorRest.CallSettings = new CallSettings(epicorConnection.Company, epicorConnection.Plant, string.Empty,
                string.Empty);
        }

        public bool SetupEpicorConnection()
        {
            try
            {
                EpicorRest.AppPoolHost = settings.ERPServer;
                EpicorRest.AppPoolInstance = settings.ERPPDatabase;
                EpicorRest.UserName = settings.ERPUserName;
                EpicorRest.Password = Secret.DecryptText(settings.ERPPassword, ststen.outr());
                EpicorRest.IgnoreCertErrors = true;
                EpicorRest.License = EpicorLicenseType.WebService;
                EpicorRest.CallSettings =
                    new CallSettings(settings.ERPCompany, settings.Plant ?? string.Empty, string.Empty, string.Empty);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Epicor connection failed: {ex.Message}");
                return false;
            }
        }
    }
}