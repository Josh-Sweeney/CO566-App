using SQLite;

namespace gha.mobile.common.entities
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
