using mParticle.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using mParticle.Data.Definitions;

namespace mParticle.LoadGenerator.services
{
   
    public class LoadGeneratorService : ILoadGeneratorService
    {
        private readonly ILogger _logger;
        private IHttpRequestRepository _httpRequestRepository { get; set; }
        public LoadGeneratorService(IHttpRequestRepository httpRequestRepository, ILogger<LoadGeneratorService> logger)
        {
            _logger = logger;
            _httpRequestRepository = httpRequestRepository;
        }
        public async Task RequestHTTP(ConfigRequestDTO configRequestDTO)
        {
            bool alwaysRun = true;
            while (alwaysRun)
            {
                await Run(configRequestDTO);

            }
           
        }

        private async Task Run(ConfigRequestDTO configRequestDTO)
        {
            List<Test> taskList = new List<Test>();
            IEnumerable<HttpResponseDTO> httpResponseDTOList = new List<HttpResponseDTO>();
            for (int requestCount = 1; requestCount < configRequestDTO.TargetRPS; requestCount++)
            {
                _logger.LogInformation($"Current Request: {requestCount} Total Request: {configRequestDTO.TargetRPS}");
                HttpRequestDTO httpRequestDTO = new HttpRequestDTO()
                {
                    name = "Jhon Castillo",
                    date = DateTime.UtcNow,
                    requests_sent = requestCount
                };
                taskList.Add(new Test(_httpRequestRepository, configRequestDTO, httpRequestDTO));
            }
            httpResponseDTOList = await Task.WhenAll(taskList.Select(x => x.RunTest()));
            await GetSummary(httpResponseDTOList);
        }
        private async Task GetSummary(IEnumerable<HttpResponseDTO> httpResponseDTOList)
        {
            int sumSuccessfulRequest = 0;
            int sumNotSuccessfulRequest = 0;
            if (httpResponseDTOList.Any())
            {
                sumSuccessfulRequest = httpResponseDTOList.Count(x => x.successful);
                sumNotSuccessfulRequest = httpResponseDTOList.Count(x => !x.successful);
            }
            _logger.LogInformation($"Successful Request: {sumSuccessfulRequest} Not Successful Request: {sumNotSuccessfulRequest}");
        }
    }
}
