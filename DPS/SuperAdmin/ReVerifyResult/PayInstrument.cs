
namespace DPS.SuperAdmin.ReVerifyResult
{
    public class PayInstrument
    {
        public PayModeSpecificData payModeSpecificData { get; set; }
        public PayDetails payDetails { get; set; }
        public SettlementDetails settlementDetails { get; set; }
        public ResponseDetails responseDetails { get; set; }
    }
}