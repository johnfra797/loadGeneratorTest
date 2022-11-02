using System;
using System.Collections.Generic;
using System.Text;

namespace mParticle.Domain.DTO
{
    public class HttpResponseDTO
    {
        public bool successful { get; set; }
        public bool isError { get; set; }
        public string error { get; set; }
    }
}