using EpicorRestAPI;
using gha.mobile.common.entities;
using gha.mobile.mims.entities;
using LicenseServerHelper;

namespace gha.mobile.mims.repositories.Setup
{
    public class SetupEpicor : Epicor, ISetup
    {
        public SetupEpicor()
        {
        }

        public SetupEpicor(ERPConnection epicorConnection) : base(epicorConnection)
        {
        }

        public EpicorResponse TestConnection(ERPConnection erpConnection)
        {
            EpicorRest.AppPoolHost = erpConnection.AppPoolHost;
            EpicorRest.AppPoolInstance = erpConnection.AppPoolInstance;
            EpicorRest.CallSettings = new CallSettings(erpConnection.Company, string.Empty, string.Empty, string.Empty);
            EpicorRest.UserName = erpConnection.UserName;
            EpicorRest.Password = erpConnection.Password.DecryptText(ststen.outr());

            return EpicorRest.TestApi(out _) == false
                ? new EpicorResponse
                    { Success = false, ErrorMessage = "Epicor connection failed, please check your settings" }
                : new EpicorResponse { Success = true };
        }
    }
}