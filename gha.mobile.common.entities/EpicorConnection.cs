using System;
using EpicorRestAPI;

namespace gha.mobile.common.entities
{
    public class ERPConnection
    {
        public string AppPoolHost { get; set; }
        public string AppPoolInstance { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Plant { get; set; }

        public bool Test()
        {
            return false;
        }

    }
}
