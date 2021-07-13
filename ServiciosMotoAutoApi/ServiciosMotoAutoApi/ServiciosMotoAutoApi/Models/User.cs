using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplianceApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string Domain { get; set; }
        public bool Enabled { get; set; }
        public bool IsAdmin { get; set; }
        [Timestamp]
        [Column(TypeName = "timestamp")]
        public byte[] RowVersion { get; set; }
    }
}
