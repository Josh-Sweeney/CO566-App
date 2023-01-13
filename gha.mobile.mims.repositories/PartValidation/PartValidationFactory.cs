using System;
using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories
{
    public class PartValidationFactory
    {
        public static IPartValidation Get()
        {
            SettingsRepository settings = new SettingsRepository();

            switch (settings.ERP)
            {
                case "EPICOR":
                    return new PartValidationEpicor();
                default:
                    return null;
            }
        }
    }
}
