using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Models
{
    public class DocumentPagingParameters: PagingParameters
    {
        public string EntityId { get; set; }
    }
}
