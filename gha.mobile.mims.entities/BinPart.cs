namespace gha.mobile.mims.entities
{
    public class BinPart
    {
        public Part Part { get; set; }
        public decimal Quantity { get; set; }
        public string UOM { get; set; }
        public int UOMDecP { get; set; }
        public bool ShowLot { get; set; }
        public string LotNum { get; set; }
    }
}
