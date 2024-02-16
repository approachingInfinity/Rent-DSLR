using DslrCustomerRecord.Models;

namespace DslrCustomerRecord.ViewModels
{
    public class ModelPriceViewModel
    {
        public List<CustomerDetailsViewModel> cust { get; set; }
        public List<CameraModelWithPrice> modelPrice { get; set; }
        public int totalCollectionThisMonth { get; set; }
        public int totalCollectionToday { get; set; }

        public int totalCollectionThisWeek { get; set; }
    }
}
