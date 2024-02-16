using System.ComponentModel.DataAnnotations;

namespace DslrCustomerRecord.Models
{
    public class RentDuration
    {
        public int durationId { get; set; }
       
        public DateTime currentDate { get; set; }
        [Required]
        public int rentHour { get; set; }
        [Required]
        public int rentMinute { get; set;}
        public int custId { get; set; }
        public int modelId { get; set; }
    }
}
