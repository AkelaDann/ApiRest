﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Infraestructure.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public required string Secret { get; init; }
        public int ExpiryMinutes { get; init; }
        public required string Issuer {  get; init; }
        public required string Audience { get; init; }
    }
}
