﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models.ViewModel
{
    public class TokenRequestViewModel
    {
        public string username { get; set; }
        public string password { get; set; }

        public string grand_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string refresh_token { get; set; }
    }
}
