﻿using System.Collections.Generic;

namespace HelloGoddess.Infrastructure.Auditing
{
    internal class AuditingSelectorList : List<NamedTypeSelector>, IAuditingSelectorList
    {
        public bool RemoveByName(string name)
        {
            return RemoveAll(s => s.Name == name) > 0;
        }
    }
}