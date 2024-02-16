using System.ComponentModel.DataAnnotations;

namespace DslrCustomerRecord.ViewModels
{
    public class EditCustomerDetailsViewModel
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string number { get; set; }
        [Required]
        public int modelId { get; set; }
        [Required]
        public int rentHour { get; set; }
        [Required]
        public int rentMinute { get; set; }
        [Required]
        public int totalPrice { get; set; }
    }
}
