using ComplianceApi.Contexts;
using ComplianceApi.Contracts;
using ComplianceApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Repositories
{
    public class RepositoryComplianceWrapper : IRepositoryComplianceWrapper
    {
        private ComplianceDbContext _repoContext; 
        private IDocumentRepository _document;
        private IUserRepository _user;
 
        public IDocumentRepository Document
        {
            get
            {
                if (_document == null)
                {
                    _document = new DocumentRepository(_repoContext);
                }
                return _document;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public RepositoryComplianceWrapper(ComplianceDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task SaveAsync()
        { 
            await _repoContext.SaveChangesAsync();
        }
    }
}
