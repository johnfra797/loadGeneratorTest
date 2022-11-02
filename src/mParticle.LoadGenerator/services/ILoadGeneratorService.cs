using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mParticle.Domain.DTO;

namespace mParticle.LoadGenerator.services
{
    public interface ILoadGeneratorService
    {
        Task RequestHTTP(ConfigRequestDTO configRequestDTO);
    }
}
