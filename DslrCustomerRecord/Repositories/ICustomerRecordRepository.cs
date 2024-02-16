using DslrCustomerRecord.Models;
using DslrCustomerRecord.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DslrCustomerRecord.Repositories
{
    public interface ICustomerRecordRepository
    {
        IEnumerable<Customer> getCustomers();
         ModelPriceViewModel  getCustomerDetails();
        List<CameraModel> getCameraModels();
        void InsertCustomerDetails(AddOrEditViewModel model);
        CustomerDetailsViewModel getCustomerById(int id);
        void UpdateCustomerDetails(EditCustomerDetailsViewModel model);
        IEnumerable<IEnumerable<CustomerDetailsViewModel>> getAllCustomers();
        void deleteCustomer(int id);
        ModelPriceViewModel searchCustomers(dynamic searchTerm);
        IEnumerable<CustomerDetailsViewModel> getCustomersByMonths(string query);
        CustomerByMonthViewModel getAllCustomerByMonth();
        IEnumerable<int> earningsByMonth();
        int earningByEachMonth(string query);
    }
}
