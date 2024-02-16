using System.ComponentModel.DataAnnotations;

namespace DslrCustomerRecord.ViewModels
{
    public class AddOrEditViewModel
    {
        public int id { get; set; }




        [Required]
        public string name { get; set; }
        


        [Required(ErrorMessage = "Address is required.")]
        [StringLength(1000, ErrorMessage = "Address must not exceed 1000 characters.")]
        [Display(Name = "Address")]
        public string address { get; set; }
       


        [Required(ErrorMessage = "contact filed cant be empty")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [Display(Name = "Contact Number")]
        [StringLength(10, ErrorMessage = "contact number shoudnt be more than 10")]
        public string number { get; set; }
        [Required]
        public int modelId { get; set; }
        [Required]
        public int rentHour { get; set; }
        [Required]
        public int rentMinute { get; set; }
        [Required]
        public int totalPrice { get; set; }
        public int due { get; set; }
    }
}
