using DslrCustomerRecord.Models;
using DslrCustomerRecord.Repositories;
using DslrCustomerRecord.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace DslrCustomerRecord.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRecordRepository repo;

        public HomeController(ILogger<HomeController> logger, ICustomerRecordRepository repository)
        {
            _logger = logger;
            repo = repository;
        }


        [Route("")]
        [Route("~/Home")]
        [Route("~/")]
        public IActionResult Index(string? search)
        {
            ViewBag.Search = search;
            var customers = repo.getCustomerDetails();
            if (string.IsNullOrEmpty(search))
            {

                return View(customers);
            }


            var model = repo.searchCustomers(search);
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddCustomerDetails()
        {

            /* 
               var list = repo.getCameraModels();
             AddCustomerDetailsViewModel cust=new();
            cust.modelList  = new List<SelectListItem>();

            foreach(var x in list)
            {
                cust.modelList.Add(new SelectListItem
                {
                    Text = x.modelName,
                    Value = Convert.ToString(x.modelId)
                }); 
            }

            ViewBag.CustList = list;
             
             */
            var model = new AddOrEditViewModel();
            model.due = 0;
            var list = repo.getCameraModels();
            ViewBag.CustList = new SelectList(list, "modelId", "modelName");


            return View();
        }
        [HttpPost]
        public IActionResult AddCustomerDetails(AddOrEditViewModel model)
        {  
          
            if (ModelState.IsValid)
            {
           repo.InsertCustomerDetails(model);
            return RedirectToActionPermanent("Index");
            }
          
            var list=repo.getCameraModels();
          ViewBag.CustList = new SelectList(list, "modelId", "modelName"); ;

            return View(model);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult EditCustomerDetails(int id)
        {
            var list = repo.getCameraModels();
            ViewBag.ModelList = new SelectList(list, "modelId", "modelName");
            var customer = repo.getCustomerById(id);
           
            int modeId = 0;
            foreach (var item in list)
            {
                if (item.modelName == customer.camModel)
                {
                    modeId = item.modelId; break;
                }
            }
            var model = new EditCustomerDetailsViewModel
            {
                id = customer.id,
                name = customer.name,
                address = customer.address,
                number = customer.number,
                modelId = modeId,
                rentHour = customer.rentHour,
                rentMinute  = customer.rentMinute,
                totalPrice = customer.totalPrice,
            };

           
            return View(model);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomerDetails(int id,EditCustomerDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateCustomerDetails(model);
            return RedirectToActionPermanent("Index");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AllCustomers()
        {
            var model=repo.getAllCustomerByMonth();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            repo.deleteCustomer(id);
            return RedirectToActionPermanent("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}