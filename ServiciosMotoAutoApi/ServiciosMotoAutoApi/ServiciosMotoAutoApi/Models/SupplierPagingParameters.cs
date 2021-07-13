using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Models
{
    public class SupplierPagingParameters: PagingParameters
    {
        public string FullName { get; set; }
        public string Rnc { get; set; }
        public string SupplierId { get; set; }
    }
}
