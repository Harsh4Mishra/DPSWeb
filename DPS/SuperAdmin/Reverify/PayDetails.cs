namespace DPS.SuperAdmin.Reverify
{
    public class PayDetails
    {
        public int amount { get; set; }
        public string txnCurrency { get; set; }
        public string signature { get; set; }
    }
}