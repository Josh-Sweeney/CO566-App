using System;
using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories
{
    public class PartEnquiryFactory
    {
        public static IPartEnquiry Get()
        {
            SettingsRepository settings = new SettingsRepository();

            switch (settings.ERP)
            {
                case "EPICOR":
                    return new PartEnquiryEpicor();
                default:
                    return null;
            }
        }
    }
}
