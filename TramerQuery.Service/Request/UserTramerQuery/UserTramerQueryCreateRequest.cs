using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TramerQuery.Service.Request.UserTramerQuery
{
    public class UserTramerQueryCreateRequest
    {
        public int UserId { get; set; }
        public int TramerQueryResultId { get; set; }
        public int? OldTramerQueryResultId { get; set; }
        public bool IsExistQuery { get; set; }
        public decimal Price { get; set; }
    }
}
