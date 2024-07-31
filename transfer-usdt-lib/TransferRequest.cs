using System.ComponentModel.DataAnnotations;

namespace TransferUsdtLib
{
    public class TransferRequest
    {
        [Required]
        [Display(Name = "Receiver Address")]
        public string ReceiverAddress { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Amount (USDT)")]
        public decimal Amount { get; set; }
    }
}
