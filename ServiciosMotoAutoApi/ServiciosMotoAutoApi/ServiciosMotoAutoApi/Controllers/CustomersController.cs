using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplianceApi.Contracts;
using ComplianceApi.Models;
using ComplianceApi.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ComplianceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowOrigin")]
    public class CustomersController : ControllerBase
    {
        private IRepositoryNavGe2016Wrapper _repoWrapper;
        public CustomersController(IRepositoryNavGe2016Wrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] CustomerPagingParameters customerPagingParameters)
        {
            var customers = await _repoWrapper.Customer.GetAllCustomersAsync(customerPagingParameters);

            var metadata = new
            {
                customers.TotalCount,
                customers.PageSize,
                customers.CurrentPage,
                customers.TotalPages,
                customers.HasNext,
                customers.HasPrevious

            };
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(customers);
        }
        // GET: api/Customers/5
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] string customerId)
        {
            var customer = await _repoWrapper.Customer.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound("The Customer record couldn't be found.");
            }
            return Ok(customer);
        }     
      
    }
}
