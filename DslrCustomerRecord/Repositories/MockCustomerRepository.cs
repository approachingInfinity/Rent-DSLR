using DslrCustomerRecord.Models;
using DslrCustomerRecord.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DslrCustomerRecord.Repositories
{
    
    public class MockCustomerRepository : ICustomerRecordRepository
    {

        private readonly IConfiguration _configuration;
        private List<CustomerDetailsViewModel> customerDetailsList;
        private List<Customer> customerList;
       // private List<RentDuration> rentList;
        //private List<CameraBrand> cameraList;
        public MockCustomerRepository(IConfiguration configuration) {
         _configuration = configuration;
            customerList = new List<Customer>()
         {

             new Customer(){customerId=1,customerName="abc",customerAddress="abc town",ContactNumber="12345678"},
             new Customer(){customerId=2,customerName="bcd",customerAddress="bcd town",ContactNumber="234567880"}
         };

        /*
         *      rentList = new List<RentDuration>()
        {
            new RentDuration(){rentId=1,currentDate="12:34:32",rentHour=2,rentMinute=30}
        };   cameraList = new List<Camera>()
           {
               new Camera(){camId=1,camName="nikon",camModel="d3500"}
           };
         */
       

         
        }

        public void deleteCustomer(int id)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "spDeleteCustomer";
           // SqlCommand cmd = new SqlCommand(query, connection);
           
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
               cmd.Parameters.AddWithValue("@id", id);
               cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int earningByEachMonth(string query)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, connection);
            int month = 0;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["totalPrice"];
                    if (ver != DBNull.Value)
                    {
                        month = Convert.ToInt32(reader["totalPrice"]);

                    }
                    //month = Convert.ToInt32(reader["totalPrice"]);
                }
            }
            connection.Close();
            return month;
        }

        public IEnumerable<int> earningsByMonth()
        {
            
            int jan,feb,mar,april,may,jun,july,aug,sep,oct,nov,dec = 0;

            List<int> list = new List<int>();
            string q1 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=1";
            string q2 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=2";
            string q3 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=3";
            string q4 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=4";
            string q5 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=5";
            string q6 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=6";
            string q7 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=7";
            string q8 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=8";
            string q9 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=9";
            string q10 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=10";
            string q11 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=11";
            string q12 = "SELECT SUM(totalPrice) AS totalPrice from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE()) AND DATEPART(MONTH, currentDate)=12";
           
            list.Add(earningByEachMonth(q1));
            list.Add(earningByEachMonth(q2));
            list.Add(earningByEachMonth(q3));
            list.Add(earningByEachMonth(q4));
            list.Add(earningByEachMonth(q5));
            list.Add(earningByEachMonth(q6));
            list.Add(earningByEachMonth(q7));
            list.Add(earningByEachMonth(q8));
            list.Add(earningByEachMonth(q9));
            list.Add(earningByEachMonth(q10));
            list.Add(earningByEachMonth(q11));
            list.Add(earningByEachMonth(q12));

            return list;
        }

        public CustomerByMonthViewModel getAllCustomerByMonth()
        {

            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT SUM(totalPrice) AS thisYears from rent_price WHERE DATEPART(YEAR, currentDate) = DATEPART(YEAR, GETDATE())";
            SqlCommand cmd = new SqlCommand(query, connection);
            int thisYearsEarnings = 0;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["thisYears"];
                    if (ver != DBNull.Value)
                    {
                        thisYearsEarnings = Convert.ToInt32(reader["thisYears"]);

                    }
                    //month = Convert.ToInt32(reader["totalPrice"]);
                }
            }
            connection.Close();

            
            string queryPrevYear = "SELECT SUM(totalPrice) AS preYears from rent_price WHERE DATEPART(YEAR, currentDate) =2022";
            SqlCommand preCmd = new SqlCommand(queryPrevYear, connection);
            int preYearsEarning = 0;
            connection.Open();
            using (SqlDataReader reader = preCmd.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["preYears"];
                    if (ver != DBNull.Value)
                    {
                        preYearsEarning = Convert.ToInt32(reader["preYears"]);

                    }
                    //month = Convert.ToInt32(reader["totalPrice"]);
                }
            }
            connection.Close();

            CustomerByMonthViewModel customerByMonth = new CustomerByMonthViewModel();
           customerByMonth.customerDetailsByMonth = getAllCustomers();
            customerByMonth.totalEarningsByMonth =earningsByMonth();
            customerByMonth.thisYearsEarnings= thisYearsEarnings;
            customerByMonth.lastYearsEearnings = preYearsEarning;



            return customerByMonth;
        }

        public IEnumerable<IEnumerable<CustomerDetailsViewModel>> getAllCustomers()
        {
           List<List<CustomerDetailsViewModel>>  allCustomer = new List<List<CustomerDetailsViewModel>>();

          
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByJan").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByFeb").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByMarch").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByApril").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByMay").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByJun").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByJul").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByAug").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersBySep").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByOct").ToList());
            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByNov").ToList());

            allCustomer.Add(getCustomersByMonths("spGetAllCustomersByDec").ToList());


            return allCustomer;
        }

        public List<CameraModel> getCameraModels()
        {
            List<CameraModel> cameraModels= new List<CameraModel>();
           // List<SelectListItem> categories = new List<SelectListItem>();

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT modelId, modelName FROM camera_model";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cameraModels.Add(new CameraModel
                            {
                                
                                modelId = Convert.ToInt32(reader["modelId"]),
                                modelName = Convert.ToString(reader["modelName"])
                            });
                        }
                    }
                }
            }
           
            return cameraModels;
        }

        public CustomerDetailsViewModel getCustomerById(int id)
        {
            CustomerDetailsViewModel customer= new CustomerDetailsViewModel();
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "getCustomerById";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (reader.Read())
                {
                   
                    customer.id = Convert.ToInt32(reader["custId"]);
                    customer.name = Convert.ToString(reader["custName"]);
                    customer.address = Convert.ToString(reader["custAddress"]);
                    customer.number = Convert.ToString(reader["custContact"]);
                    customer.camName = Convert.ToString(reader["brandName"]);
                    customer.camModel = Convert.ToString(reader["modelName"]);
                    customer.rentHour = Convert.ToInt32(reader["rentHour"]);
                    customer.rentMinute = Convert.ToInt32(reader["rentMinute"]);
                    customer.totalPrice = Convert.ToInt32(reader["totalPrice"]);
                    
                }


            }
            connection.Close();
            return customer;
        }

        public ModelPriceViewModel getCustomerDetails()
        {
            List<CustomerDetailsViewModel> custDetails = new List<CustomerDetailsViewModel>();
            
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "spCustDetails";
            SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
               
                    while (reader.Read())
                    {
                    CustomerDetailsViewModel customer = new CustomerDetailsViewModel();
                    customer.id = Convert.ToInt32(reader["custId"]);
                    customer.name = Convert.ToString(reader["custName"]);
                    customer.address = Convert.ToString(reader["custAddress"]);
                    customer.number = Convert.ToString(reader["custContact"]);
                    customer.camName = Convert.ToString(reader["brandName"]);
                    customer.camModel = Convert.ToString(reader["modelName"]);
                    customer.rentHour = Convert.ToInt32(reader["rentHour"]);
                    customer.rentMinute = Convert.ToInt32(reader["rentMinute"]);
                    customer.totalPrice = Convert.ToInt32(reader["totalPrice"]);
                    customer.due = Convert.ToInt32(reader["due"]);
                    custDetails.Add(customer);
                    }
                   

            }
            connection.Close();

            CustomerDetailsViewModel cust = new CustomerDetailsViewModel();
            List<CameraModelWithPrice> camModelPrice= new List<CameraModelWithPrice>();
            string query1 = "TotalCollectionByCamera";
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd1.ExecuteReader())
            {

                while (reader.Read())
                {
                   
                    CameraModelWithPrice modelPrice = new CameraModelWithPrice(); 
                    modelPrice.modelName = Convert.ToString(reader["modelName"]);
                    modelPrice.totalCollection = Convert.ToInt32(reader["totalPrice"]);
                    modelPrice.repeatition = Convert.ToInt32(reader["repeat"]);
                    camModelPrice.Add(modelPrice);
                   
                    
                }


            }
            
            connection.Close();
           //get total price collected current MOontth
            int totalCollectionByMonth=0;
            string cByMonth = "TotalCollectonThisMonth";
            SqlCommand cmd2 = new SqlCommand(cByMonth, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd2.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["totalPrice"];
                    if (ver != DBNull.Value)
                    {
                    totalCollectionByMonth = Convert.ToInt32(reader["totalPrice"]);

                    }
                }


            }

            connection.Close();

            //total collection totay

            int totalCollectionToday=0;
            string cToday = "spTotalCollectonToday";
            SqlCommand cmd3 = new SqlCommand(cToday, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd3.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["totalPrice"];
                    if(ver != DBNull.Value)
                    {
                     totalCollectionToday = Convert.ToInt32(reader["totalPrice"]);
                    }
                   
                }


            }

            connection.Close();

            //ado.net logic to get total collection this week

            int totalCollectionThisWeek = 0;
            string cByWeek = "spTotalCollectionThisWeek";
            SqlCommand cmd4 = new SqlCommand(cByWeek, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd4.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd4.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["totalPrice"];
                    if (ver != DBNull.Value)
                    {
                        totalCollectionThisWeek = Convert.ToInt32(reader["totalPrice"]);

                    }
                }


            }

            connection.Close();


            var customerDeatails = new ModelPriceViewModel
            {
                cust = custDetails,
                modelPrice = camModelPrice,
                totalCollectionThisMonth= totalCollectionByMonth,
                totalCollectionToday=totalCollectionToday,
                totalCollectionThisWeek=totalCollectionThisWeek
                
            };
         
            return customerDeatails;
        }

        public IEnumerable<Customer> getCustomers()
        {
            return customerList;
        }

        public IEnumerable<CustomerDetailsViewModel> getCustomersByMonths(string query)
        {
            List<CustomerDetailsViewModel> allCust = new List<CustomerDetailsViewModel>();
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.rameters.AddWithValue("@id", id);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    CustomerDetailsViewModel customer = new CustomerDetailsViewModel();
                    customer.id= Convert.ToInt32(reader["custId"]);
                    customer.name = Convert.ToString(reader["custName"]);
                    customer.address = Convert.ToString(reader["custAddress"]);
                    customer.number = Convert.ToString(reader["custContact"]);
                    customer.camName = Convert.ToString(reader["brandName"]);
                    customer.camModel = Convert.ToString(reader["modelName"]);
                    customer.rentHour = Convert.ToInt32(reader["rentHour"]);
                    customer.rentMinute = Convert.ToInt32(reader["rentMinute"]);
                    customer.totalPrice = Convert.ToInt32(reader["totalPrice"]);
                    allCust.Add(customer);
                }


            }
            connection.Close(); 
            return allCust;
        }

        public void InsertCustomerDetails(AddOrEditViewModel model)
        {
            //insert into customers
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "insert into customers(custName,custAddress,custContact,modelId) values(@custName,@custAddress,@custContact,@modelId)";
            SqlCommand cmd=new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@custName",model.name);
            cmd.Parameters.AddWithValue("@custAddress",model.address);
            cmd.Parameters.AddWithValue("@custContact",model.number);
            cmd.Parameters.AddWithValue("@modelId", model.modelId);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();


            //get custId


            int custId=0;
                connection.Open();

                string custIdQuery = "SELECT TOP 1 custId FROM customers ORDER BY custId DESC"; // Replace YourTable with the actual table name
                using (SqlCommand command = new SqlCommand(custIdQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                      Customer customer = new Customer();
                    if (reader.Read())
                    {
                    custId = Convert.ToInt32(reader["custId"]);
                    }
                        
                        
                    }
                }
            
                connection.Close();
            //insert into rent_duration
            string durationQuery = "insert into rent_duration(rentHour,rentMinute,custId,modelId) values(@rentHour,@rentMinute,@custId,@modelId)";
            SqlCommand durationCmd= new SqlCommand(durationQuery, connection);
            durationCmd.Parameters.AddWithValue("@rentHour", model.rentHour);
            durationCmd.Parameters.AddWithValue("@rentMinute", model.rentMinute);
            durationCmd.Parameters.AddWithValue("@custId",custId);
            durationCmd.Parameters.AddWithValue("@modelId", model.modelId);
            connection.Open();
            durationCmd.ExecuteNonQuery();
            connection.Close();


            //rent price
            string priceQuery = "insert into rent_price(totalPrice,custId,modelId,due) values(@totalPrice,@custId,@modelId,@due)";
            SqlCommand priceCmd = new SqlCommand(priceQuery, connection);
            priceCmd.Parameters.AddWithValue("@totalPrice", model.totalPrice);
            priceCmd.Parameters.AddWithValue("@custId", custId);
            priceCmd.Parameters.AddWithValue("@modelId", model.modelId);
            priceCmd.Parameters.AddWithValue("@due", model.due);
            connection.Open();
            priceCmd.ExecuteNonQuery();
            connection.Close();
          
        }

        public ModelPriceViewModel searchCustomers(dynamic searchTerm)
        {
            List<CustomerDetailsViewModel> custDetails = new List<CustomerDetailsViewModel>();

            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "spSearchCustomers";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@search",searchTerm);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    CustomerDetailsViewModel customer = new CustomerDetailsViewModel();
                    customer.id = Convert.ToInt32(reader["custId"]);
                    customer.name = Convert.ToString(reader["custName"]);
                    customer.address = Convert.ToString(reader["custAddress"]);
                    customer.number = Convert.ToString(reader["custContact"]);
                    customer.camName = Convert.ToString(reader["brandName"]);
                    customer.camModel = Convert.ToString(reader["modelName"]);
                    customer.rentHour = Convert.ToInt32(reader["rentHour"]);
                    customer.rentMinute = Convert.ToInt32(reader["rentMinute"]);
                    customer.totalPrice = Convert.ToInt32(reader["totalPrice"]);
                    custDetails.Add(customer);
                }


            }
            connection.Close();

            CustomerDetailsViewModel cust = new CustomerDetailsViewModel();
            List<CameraModelWithPrice> camModelPrice = new List<CameraModelWithPrice>();
            string query1 = "TotalCollectionByCamera";
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd1.ExecuteReader())
            {

                while (reader.Read())
                {

                    CameraModelWithPrice modelPrice = new CameraModelWithPrice();
                    modelPrice.modelName = Convert.ToString(reader["modelName"]);
                    modelPrice.totalCollection = Convert.ToInt32(reader["totalPrice"]);
                    modelPrice.repeatition = Convert.ToInt32(reader["repeat"]);
                    camModelPrice.Add(modelPrice);


                }


            }

            connection.Close();
            //get total price collected current MOontth
            int totalCollectionByMonth = 0;
            string cByMonth = "TotalCollectonThisMonth";
            SqlCommand cmd2 = new SqlCommand(cByMonth, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd2.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["totalPrice"];
                    if (ver != DBNull.Value)
                    {
                        totalCollectionByMonth = Convert.ToInt32(reader["totalPrice"]);

                    }
                }


            }

            connection.Close();

            //total collection totay

            int totalCollectionToday = 0;
            string cToday = "spTotalCollectonToday";
            SqlCommand cmd3 = new SqlCommand(cToday, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd3.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd3.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["totalPrice"];
                    if (ver != DBNull.Value)
                    {
                        totalCollectionToday = Convert.ToInt32(reader["totalPrice"]);
                    }

                }


            }

            connection.Close();

            //ado.net logic to get total collection this week

            int totalCollectionThisWeek = 0;
            string cByWeek = "spTotalCollectionThisWeek";
            SqlCommand cmd4 = new SqlCommand(cByWeek, connection);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd4.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd4.ExecuteReader())
            {

                if (reader.Read())
                {
                    object ver = reader["totalPrice"];
                    if (ver != DBNull.Value)
                    {
                        totalCollectionThisWeek = Convert.ToInt32(reader["totalPrice"]);

                    }
                }


            }

            connection.Close();


            var customerDeatails = new ModelPriceViewModel
            {
                cust = custDetails,
                modelPrice = camModelPrice,
                totalCollectionThisMonth = totalCollectionByMonth,
                totalCollectionToday = totalCollectionToday,
                totalCollectionThisWeek = totalCollectionThisWeek

            };

            return customerDeatails;
        }

        public void UpdateCustomerDetails(EditCustomerDetailsViewModel model)
        {
           
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "spEditCustomerDetails";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@id",model.id);
            cmd.Parameters.AddWithValue("@name",model.name);
            cmd.Parameters.AddWithValue("@address",model.address);
            cmd.Parameters.AddWithValue("@contact",model.number);
            cmd.Parameters.AddWithValue("@modelId", model.modelId);
            cmd.Parameters.AddWithValue("@rentHour",model.rentHour);
            cmd.Parameters.AddWithValue("@rentMinute",model.rentMinute);
            cmd.Parameters.AddWithValue("@totalCost",model.totalPrice);
            
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
