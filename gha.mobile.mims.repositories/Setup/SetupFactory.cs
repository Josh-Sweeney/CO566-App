using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories.Setup
{
    public class SetupFactory
    {
        public static ISetup Get()
        {
            var settings = new SettingsRepository();

            return settings.ERP switch
            {
                "EPICOR" => new SetupEpicor(),
                _ => null
            };
        }

        public static ISetup Get(string erpSystem)
        {
            return erpSystem switch
            {
                "EPICOR" => new SetupEpicor(),
                _ => null
            };
        }
    }
}