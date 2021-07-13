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

namespace ComplianceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowOrigin")]
    public class DocumentsController : ControllerBase
    {
        private IRepositoryComplianceWrapper _repoWrapper;
        public DocumentsController(IRepositoryComplianceWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        // GET: api/documents
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments([FromQuery] DocumentPagingParameters documentPagingParameters)
        {
            var documents = await _repoWrapper.Document.GetAllDocumentsAsync(documentPagingParameters);
            return Ok(documents);
        }
        // GET: api/documents/5
        [HttpGet("{documentId}", Name = "GetDocumentById")]
        public async Task<IActionResult> GetDocumentById(int documentId)
        {
            var document = await _repoWrapper.Document.GetDocumentByIdAsync(documentId);
            if (document == null)
            {
                return NotFound("The Document record couldn't be found.");
            }
            return Ok(document);
        }
        // POST: api/documents
        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest("Document is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object.");
            }
            _repoWrapper.Document.CreateDocument(document);
            await _repoWrapper.SaveAsync();

            return CreatedAtRoute(
                  "GetDocumentById",
                  new { documentId = document.DocumentId },
                  document);
        }
        // PUT: api/documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(string documentRowVersion, [FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest("document is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var documentToUpdate = await _repoWrapper.Document.GetDocumentByRowVersionAsync(documentRowVersion);
            if (documentToUpdate == null)
            {
                return NotFound("The document record couldn't be found.");
            }

            _repoWrapper.Document.UpdateDocument(documentToUpdate, document);
            await _repoWrapper.SaveAsync();
            return NoContent();
        }
        // DELETE: api/documents/5
        [HttpDelete("{documentId}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            Document document = await _repoWrapper.Document.GetDocumentByIdAsync(documentId);
            if (document == null)
            {
                return NotFound("The Document record couldn't be found.");
            }
            _repoWrapper.Document.DeleteDocument(document);
            await _repoWrapper.SaveAsync();
            return NoContent();
        }
    }
}
