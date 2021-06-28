using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWYFT_CRM.Models
{
    public class EmailTemplate
    {
        int Id { get; set; }
        int UserId { get; set; }
        string Suubject { get; set; }
        string Content { get; set; }
    }
}
