﻿using System;
using System.Collections.Generic;

namespace HelloGoddess.Infrastructure.Auditing
{
    internal class AuditingConfiguration : IAuditingConfiguration
    {
        public bool IsEnabled { get; set; }

        public bool IsEnabledForAnonymousUsers { get; set; }

        public IAuditingSelectorList Selectors { get; }

        public List<Type> IgnoredTypes { get; }

        public AuditingConfiguration()
        {
            IsEnabled = true;
            Selectors = new AuditingSelectorList();
            IgnoredTypes = new List<Type>();
        }
    }
}