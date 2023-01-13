using System;

namespace gha.mobile.common.entities
{
    public class DeviceRegistrationResponse
    {
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
        public string DeviceLicence { get; set; }
        public string CustomerLicence { get; set; }
        public string CustomerName { get; set; }
        public string ApplicationLicence { get; set; }
        public string ApplicationName { get; set; }
        public string Message { get; set; }
        public bool Testing { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }
    }
}