namespace DslrCustomerRecord.ViewModels
{
    public class CustomerByMonthViewModel
    {
        public IEnumerable<IEnumerable<CustomerDetailsViewModel>> customerDetailsByMonth { get; set; }
        public IEnumerable<int> totalEarningsByMonth { get; set; }

        public int thisYearsEarnings { get; set; }
        public int lastYearsEearnings { get; set; }
    }
}
