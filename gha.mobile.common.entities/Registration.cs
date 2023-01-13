namespace gha.mobile.common.entities
{
    public class Registration
    {
        public string LicenceKey { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public int NoOfDevices { get; set; }

        public bool IsValid()
        {
            bool result = true;

            if (string.IsNullOrEmpty(FullName) ||
                string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrEmpty(Company) ||
                string.IsNullOrWhiteSpace(Company) ||
                string.IsNullOrEmpty(EmailAddress) ||
                string.IsNullOrWhiteSpace(EmailAddress) ||
                string.IsNullOrEmpty(ContactNumber) ||
                string.IsNullOrWhiteSpace(ContactNumber)) result = false;

            if (NoOfDevices == 0) result = false;

            return result;
        }

        public bool IsNotFilledIn()
        {
            bool result = false;

            if (string.IsNullOrEmpty(FullName) &&
                string.IsNullOrWhiteSpace(FullName) &&
                string.IsNullOrEmpty(Company) &&
                string.IsNullOrWhiteSpace(Company) &&
                string.IsNullOrEmpty(EmailAddress) &&
                string.IsNullOrWhiteSpace(EmailAddress) &&
                string.IsNullOrEmpty(ContactNumber) &&
                string.IsNullOrWhiteSpace(ContactNumber)) result = true;

            return result;
        }
    }
}
