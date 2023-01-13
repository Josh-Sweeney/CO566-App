using System;
namespace gha.mobile.common.entities
{
    public class CustomisationResponse
    {
        public string CompanyLogo { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string ImageResolution { get; set; }
        public bool ImageResolutionFix { get; set; }
        public string CustomisationStamp { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }
    }
}

