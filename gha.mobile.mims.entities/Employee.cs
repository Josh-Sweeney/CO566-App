using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace gha.mobile.mims.entities
{
    public class Employee
    {
        [PrimaryKey]
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string KnownAs { get; set; }
    }
}
