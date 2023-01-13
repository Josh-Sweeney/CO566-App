using System.Collections.Generic;

namespace gha.mobile.mims.entities
{
    public class Part
    {
        public string PartNum { get; set; }
        public string Description { get; set; }
        public bool LotTracked { get; set; }
        public bool SerialTracked { get; set; }
        public string UOM { get; set; }

        public bool QtyBearing { get; set; }
    }
}