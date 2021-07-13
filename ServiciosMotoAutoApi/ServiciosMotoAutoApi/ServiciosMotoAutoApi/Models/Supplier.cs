using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Models
{
    [Table("Suppliers_View")]
    public class Supplier
    {
        [Key]
        public string SupplierId { get; set; }
        public string FullName { get; set; }
        public string Rnc { get; set; }
        public string Address { get; set; }
    }
}
