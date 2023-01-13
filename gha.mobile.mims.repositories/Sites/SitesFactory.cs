using System;
using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories
{
    public class SitesFactory
    {
        public static ISites Get()
        {
            SettingsRepository settings = new SettingsRepository();

            switch (settings.ERP)
            {
                case "EPICOR":
                    return new SitesEpicor();
                default:
                    return null;
            }
        }
    }
}
