﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.DTOs.Response
{
    public class AccountResponse
    {
        public short AccountId { get; set; }
        public string? AccountEmail { get; set; }
        public string? AccountName { get; set; }
        public int? AccountRole { get; set; }
    }
}
