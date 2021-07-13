using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplianceApi.Contracts;
using ComplianceApi.Models;
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
    public class SuppliersController : ControllerBase
    {
        private IRepositoryNavGe2016Wrapper _repoWrapper;
        public SuppliersController(IRepositoryNavGe2016Wrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        // GET: api/Suppliers
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers([FromQuery] SupplierPagingParameters supplierPagingParameters)
        {
            var suppliers = await _repoWrapper.Supplier.GetAllSuppliersAsync(supplierPagingParameters);

            var metadata = new
            {
                suppliers.TotalCount,
                suppliers.PageSize,
                suppliers.CurrentPage,
                suppliers.TotalPages,
                suppliers.HasNext,
                suppliers.HasPrevious

            };
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(suppliers);
        }
        // GET: api/Suppliers/5
        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplierById([FromRoute] string supplierId)
        {
            var supplier = await _repoWrapper.Supplier.GetSupplierByIdAsync(supplierId);
            if (supplier == null)
            {
                return NotFound("The Supplier record couldn't be found.");
            }
            return Ok(supplier);
        }

    }
}
