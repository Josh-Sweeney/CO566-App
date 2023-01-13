using System;
using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories
{
    public class BinsFactory
    {
        public static IBins Get()
        {
            SettingsRepository settings = new SettingsRepository();

            switch (settings.ERP)
            {
                case "EPICOR":
                    return new BinsEpicor();
                default:
                    return null;
            }
        }
    }
}
