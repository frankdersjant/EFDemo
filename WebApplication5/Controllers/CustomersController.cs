using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using WebApplication5.DTO;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersContext _customersContext;
        public CustomersController(CustomersContext customersContext)
        {
            _customersContext = customersContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customersContext.Customer);
        }

        [HttpGet("CreateCustomer")]
        public IActionResult CreateCustomer()
        {
            Customer newCustomer = new Customer { FirstName = "Ziggy", LastName = "Stardust" };
            _customersContext.Customer.Add(newCustomer);

            _customersContext.SaveChanges();

            return Ok(_customersContext.Customer);
        }

        [HttpPost("DeleteCustomer")]
        public IActionResult DeleteCustomer(int ID)
        {
            Customer foundCustomer = _customersContext.Customer.Find(ID);
            _customersContext.Customer.Remove(foundCustomer);

            _customersContext.SaveChanges();

            return Ok(_customersContext.Customer);
        }

        [HttpPost("EditCustomer")]
        public IActionResult EditCustomer(int ID, string FN)
        {
            Customer foundCustomer = _customersContext.Customer.Find(ID);
            foundCustomer.FirstName = FN;


            _customersContext.SaveChanges();

            return Ok(_customersContext.Customer);
        }


        [HttpPost("ModifyCustomer")]
        public IActionResult ModifyCustomer(Customer customer)
        {
            _customersContext.Entry(customer).State = EntityState.Modified;
            _customersContext.SaveChanges();

            return Ok(_customersContext.Customer);
        }


        //[HttpGet("Include")]
        //public IActionResult Include(int ID)
        //{
        //    var Customer = _customersContext.Customer
        //                    .Include(c => c.Invoice)
        //                    .Where(n => n.CustomerID == ID)
        //                    .ToList();

        //    return Ok(Customer);
        //}

        [HttpGet("CustomerInvoices")]
        public IActionResult CustomerInvoices()
        {
            var Customers = _customersContext.Customer
                            .Include(c => c.Invoice)
                            .ToList();

            return Ok(Customers);
        }

        [HttpGet("EasyLinq")]
        public IActionResult EasyLinq()
        {
            //method syntax
            var Customer = _customersContext.Customer
                            .Where(c => c.LastName == "Stardust")
                            .FirstOrDefault<Customer>();

            //query syntax
            var query = from st in _customersContext.Customer
                        where st.LastName == "Billy"
                        select st;

            return Ok(Customer);
        }

        [HttpGet("LN")]
        public IActionResult LN()
        {
            var Customer = _customersContext.Customer.OrderBy(c => c.LastName).ToList();

            return Ok(Customer);
        }

        [HttpGet("Projection")]
        public IActionResult Projection()
        {
            var CustDTO = _customersContext.Customer.
                            Select(p => new CustomerDTO { LastName = p.LastName })
                            .ToList();

            return Ok(CustDTO);
        }

        [HttpGet("SelectNotEasy")]
        public IActionResult SelectNotEasy()
        {
            var CustInvoice = _customersContext.Customer
                            .Where(c => c.FirstName.StartsWith("Z"))
                            .SelectMany(c => c.Invoice, (c, i) =>
                            new
                            {
                                c.LastName,
                                i.InvoiceDate
                            })
                            .ToList();

            return Ok(CustInvoice);
        }

        [HttpGet("ThenInclude")]
        public IActionResult ThenInclude()
        {
            var CustInvoice = _customersContext.Customer
                            .Where(c => c.FirstName.StartsWith("Z"))
                            .Include(i => i.Invoice)
                            .ThenInclude(j => j.InvoiceLines)
                            .ToList();

            return Ok(CustInvoice);
        }
    }
}
