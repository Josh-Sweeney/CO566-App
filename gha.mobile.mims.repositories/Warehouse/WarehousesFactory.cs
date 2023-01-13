using System;
using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories
{
    public class WarehousesFactory
    {
        public static IWarehouses Get()
        {
            SettingsRepository settings = new SettingsRepository();

            switch (settings.ERP)
            {
                case "EPICOR":
                    return new WarehousesEpicor();
                default:
                    return null;
            }
        }
    }
}
