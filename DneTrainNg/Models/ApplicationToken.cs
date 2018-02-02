using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models
{
    public class ApplicationToken
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public int Type { get; set; }

        public string Value { get; set; }

        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
