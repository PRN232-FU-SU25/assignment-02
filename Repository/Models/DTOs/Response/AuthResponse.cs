﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.DTOs.Response
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }
        public AccountResponse Account { get; set; }
    }
}
