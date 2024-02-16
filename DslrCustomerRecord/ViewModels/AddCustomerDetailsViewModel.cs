using DslrCustomerRecord.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DslrCustomerRecord.ViewModels
{
    public class AddCustomerDetailsViewModel
    {
        public List<SelectListItem>? modelList { get; set; }
        public CameraModel cameraModels { get; set; }
        public CameraBrand cameraBrands { get; set; }
        public Customer customers { get; set; }
        public RentDuration rentDuration { get; set; }
        public RentPrice rentPrice { get; set; }
    }
}
