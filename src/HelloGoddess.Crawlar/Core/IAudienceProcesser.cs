using HelloGoddess.Crawlar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Crawlar.Core
{
    interface IAudienceProcesser
    {
        void Process(Audience audience);
    }
}
