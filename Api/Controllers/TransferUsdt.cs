namespace Api.Controllers
{
    public class TransferUsdt
    {
        public string ToAdress { get; set; }
        public decimal AmountToSend { get; set; }
        public string PrivateKey { get; set; }
    }
}
