using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Models
{
    public class UserPagingParameters: PagingParameters
    {
        public string UserName { get; set; }
    }
}
