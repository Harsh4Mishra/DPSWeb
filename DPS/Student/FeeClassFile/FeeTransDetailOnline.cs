namespace DPS.Student.FeeClassFile
{
    public class FeeTransDetailOnline
    {
        public int? ReceiptNo { get; set; }         // Nullable to match SQL NULL
        public string FeeType { get; set; }          // Fee Type
        public string FeeName { get; set; }          // Fee Name
        public decimal? PrevBalAmt { get; set; }     // Previous Balance Amount
        public decimal? FeeAmt { get; set; }         // Fee Amount
        public decimal? DisAmt { get; set; }         // Discount Amount
        public decimal? PaidFeeAmt { get; set; }     // Paid Fee Amount
        public int? FeeTypeSeqNo { get; set; }       // Fee Type Sequence Number
        public int? FeeHeadSeqNo { get; set; }       // Fee Head Sequence Number
    }
}