using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Models
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }
        [ForeignKey("EntityId")]
        [StringLength(100)]
        public string EntityId { get; set; }
        [StringLength(100)]
        public string Category { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public bool HasExpirationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        [StringLength(100)]
        public string DocumentType { get; set; }
        [StringLength(100)]
        public string DocumentName { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string DocumentFile { get; set; }
        public DateTime UploadDate { get; set; }
        [StringLength(100)]
        public string UploadBy { get; set; }
        [Timestamp]
        [Column(TypeName = "timestamp")]
        public byte[] RowVersion { get; set; }

    }
}
