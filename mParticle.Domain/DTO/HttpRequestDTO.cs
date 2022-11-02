using System;
using System.Collections.Generic;
using System.Text;

namespace mParticle.Domain.DTO
{
    public class HttpRequestDTO
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public int requests_sent { get; set; }
    }
}
