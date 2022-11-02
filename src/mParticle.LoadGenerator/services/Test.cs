using mParticle.Data.Definitions;
using mParticle.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mParticle.LoadGenerator.services
{
    public class Test
    {
        private IHttpRequestRepository _httpRequestRepository { get; set; }
        private ConfigRequestDTO _configRequestDTO { get; set; }
        private HttpRequestDTO _httpRequestDTO { get; set; }
        public Test(IHttpRequestRepository httpRequestRepository, ConfigRequestDTO configRequestDTO, HttpRequestDTO httpRequestDTO)
        {
            _httpRequestRepository = httpRequestRepository;
            _configRequestDTO = configRequestDTO;
            _httpRequestDTO = httpRequestDTO;
        }
        public async Task<HttpResponseDTO> RunTest()
        {
            return await _httpRequestRepository.RequestHTTP(_configRequestDTO, _httpRequestDTO);
        }
    }
}
