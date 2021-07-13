using ComplianceApi.Contexts;
using ComplianceApi.Contracts;
using ComplianceApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Repositories
{
    public class RepositoryNavGe2016Wrapper : IRepositoryNavGe2016Wrapper
    {
        private NavGE2016DbContext _repoContext;
        private ICustomerRepository _customer;
        private ISupplierRepository _supplier;
        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_repoContext);
                }
                return _customer;
            }
        }

        public ISupplierRepository Supplier
        {
            get
            {
                if (_supplier == null)
                {
                    _supplier = new SupplierRepository(_repoContext);
                }
                return _supplier;
            }
        }

        public RepositoryNavGe2016Wrapper(NavGE2016DbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}

