using System;
using System.Collections.Generic;

namespace gha.mobile.mims.entities
{
    public class PartBin
    {
        public Warehouse Warehouse { get; set; }
        public Bin Bin { get; set; }
        public decimal Quantity { get; set; }
        public string UOM { get; set; }
        public int UOMDecP { get; set; }
        public bool ShowLot { get; set; }

        public string LotNum { get; set; }
        public decimal SelectedQuantity { get; set; }

        public bool IssuedComplete { get; set; }

        public string Zone { get; set; }
        public string Aisle { get; set; }
        public int Elevation { get; set; }
        public int BinSeq { get; set; }
    }
}
