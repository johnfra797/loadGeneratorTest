
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mParticle.Domain.DTO;

namespace mParticle.Data.Definitions
{
    public interface IHttpRequestRepository
    {
        Task<HttpResponseDTO> RequestHTTP(ConfigRequestDTO configRequestDTO,HttpRequestDTO httpRequestDTO);
    }
}
