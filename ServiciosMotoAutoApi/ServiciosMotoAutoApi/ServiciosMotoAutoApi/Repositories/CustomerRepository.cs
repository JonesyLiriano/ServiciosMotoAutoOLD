using ComplianceApi.Contexts;
using ComplianceApi.Models;
using ComplianceApi.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Repositories
{
    public class CustomerRepository : RepositoryNavGe2016Base<Customer>, ICustomerRepository
    {
        public CustomerRepository(NavGE2016DbContext repositoryContext)
          : base(repositoryContext)
        {
        }
        public async Task<PagedList<Customer>> GetAllCustomersAsync(CustomerPagingParameters customerPagingParameters)
        {
            var customers = FindAll();

            SearchBy(ref customers, customerPagingParameters);          

            return await PagedList<Customer>.ToPagedList(customers.OrderBy(customer => customer.FullName),
                customerPagingParameters.PageNumber,
                customerPagingParameters.PageSize);
        }
        private void SearchBy(ref IQueryable<Customer> customers, CustomerPagingParameters customerPagingParameters)
        {
            if (!customers.Any() || (customerPagingParameters.FullName == null && customerPagingParameters.Rnc == null && customerPagingParameters.CustomerId == null))
                return;

            if (customerPagingParameters.FullName != null)
                customers = customers.Where(o => o.FullName.ToLower().Contains(customerPagingParameters.FullName.Trim().ToLower()));
            else if (customerPagingParameters.Rnc != null)
                customers = customers.Where(o => o.Rnc.ToLower().Contains(customerPagingParameters.Rnc.Trim().ToLower()));
            else
                customers = customers.Where(o => o.CustomerId.ToLower().Contains(customerPagingParameters.CustomerId.Trim().ToLower()));

        }
        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            return await FindByCondition(customer => customer.CustomerId.Equals(customerId))
                .FirstOrDefaultAsync();
        }       
    }
}
