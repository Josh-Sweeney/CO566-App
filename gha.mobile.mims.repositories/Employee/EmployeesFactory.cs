using System;
using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories
{
    public class EmployeesFactory
    {
        public static IEmployees Get()
        {
            SettingsRepository settings = new SettingsRepository();

            switch (settings.ERP)
            {
                case "EPICOR":
                    return new EmployeesEpicor();
                default:
                    return null;
            }
        }
    }
}
