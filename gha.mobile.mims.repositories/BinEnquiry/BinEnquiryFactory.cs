using System;
using gha.mobile.common.repositories;

namespace gha.mobile.mims.repositories
{
    public class BinEnquiryFactory
    {
        public static IBinEnquiry Get()
        {
            SettingsRepository settings = new SettingsRepository();

            switch (settings.ERP)
            {
                case "EPICOR":
                    return new BinEnquiryEpicor();
                default:
                    return null;
            }
        }
    }
}
