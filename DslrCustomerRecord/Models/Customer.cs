using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DslrCustomerRecord.Models
{
    public class Customer
    {
        public int customerId { get; set; }

        public string customerName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(1000, ErrorMessage = "Address must not exceed 1000 characters.")]
        [Display(Name = "Address")]
        public string customerAddress { get; set; }

        [Required(ErrorMessage = "contact filed cant be empty")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [Display(Name = "Contact Number")]
        [StringLength(10,ErrorMessage="contact number shoudnt be more than 10")]
         public string? ContactNumber { get; set; }
        public int modelId { get; set; }

    }
    }

