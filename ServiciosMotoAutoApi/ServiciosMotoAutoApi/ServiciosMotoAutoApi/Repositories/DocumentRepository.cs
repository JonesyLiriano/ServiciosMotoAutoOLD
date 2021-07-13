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
    public class DocumentRepository : RepositoryComplianceBase<Document>, IDocumentRepository
    {
        public DocumentRepository(ComplianceDbContext repositoryContext)
           : base(repositoryContext)
        {
        }        
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync(DocumentPagingParameters documentPagingParameters)
        {
            return await FindByCondition(o => o.EntityId == documentPagingParameters.EntityId)
               .OrderBy(document => document)
               .ToListAsync();
        }
        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            return await FindByCondition(document => document.DocumentId.Equals(documentId))
                .FirstOrDefaultAsync();
        }
        public async Task<Document> GetDocumentByRowVersionAsync(string documentRowVersion)
        {
            return await FindByCondition(document => document.RowVersion.Equals(documentRowVersion))
                .FirstOrDefaultAsync();
        }
        public void CreateDocument(Document document)
        {
            Create(document);
        }
        public void UpdateDocument(Document document, Document entity)
        {
            document.EntityId = entity.EntityId;
            document.Category = entity.Category;
            document.Description = entity.Description;
            document.DocumentFile = entity.DocumentFile;
            document.DocumentName = entity.DocumentName;
            document.DocumentType = entity.DocumentType;
            document.ExpirationDate = entity.ExpirationDate;
            document.HasExpirationDate = entity.HasExpirationDate;
            document.UploadBy = entity.UploadBy;
            document.UploadDate = entity.UploadDate;
            Update(document);
        }
        public void DeleteDocument(Document document)
        {
            Delete(document);
        }
    }
}

