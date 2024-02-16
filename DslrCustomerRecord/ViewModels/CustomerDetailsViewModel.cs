using DslrCustomerRecord.Models;

namespace DslrCustomerRecord.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string camName { get; set; }
        public string camModel { get; set; }
        public int rentHour { get; set; }
        public int rentMinute { get; set; }
        public int totalPrice { get; set; }
        public int due { get; set; }

    }
}
