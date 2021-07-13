using ComplianceApi.Contexts;
using ComplianceApi.Contracts;
using ComplianceApi.Models;
using ComplianceApi.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Repositories
{
    public class SupplierRepository : RepositoryNavGe2016Base<Supplier>, ISupplierRepository
    {
        public SupplierRepository(NavGE2016DbContext repositoryContext)
          : base(repositoryContext)
        {
        }
        public async Task<PagedList<Supplier>> GetAllSuppliersAsync(SupplierPagingParameters supplierPagingParameters)
        {
            var suppliers = FindAll();

            SearchBy(ref suppliers, supplierPagingParameters);

            return await PagedList<Supplier>.ToPagedList(suppliers.OrderBy(supplier => supplier.FullName),
                supplierPagingParameters.PageNumber,
                supplierPagingParameters.PageSize);
        }
        private void SearchBy(ref IQueryable<Supplier> suppliers, SupplierPagingParameters supplierPagingParameters)
        {
            if (!suppliers.Any() || (supplierPagingParameters.FullName == null && supplierPagingParameters.Rnc == null && supplierPagingParameters.SupplierId == null))
                return;

            if (supplierPagingParameters.FullName != null)
                suppliers = suppliers.Where(o => o.FullName.ToLower().Contains(supplierPagingParameters.FullName.Trim().ToLower()));
            else if (supplierPagingParameters.Rnc != null)
                suppliers = suppliers.Where(o => o.Rnc.ToLower().Contains(supplierPagingParameters.Rnc.Trim().ToLower()));
            else
                suppliers = suppliers.Where(o => o.SupplierId.ToLower().Contains(supplierPagingParameters.SupplierId.Trim().ToLower()));

        }
        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            return await FindByCondition(supplier => supplier.SupplierId.Equals(supplierId))
                .FirstOrDefaultAsync();
        }
    }
}
