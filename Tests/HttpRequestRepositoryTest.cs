using System;
using System.Threading.Tasks;
using mParticle.Data.Definitions;
using mParticle.Data.Implementations;
using mParticle.Domain.DTO;
using Xunit;

namespace Tests
{
    public class HttpRequestRepositoryTest
    {
        [Fact]
        public async void HttpRequestRepositoryTestOne()
        {
            var httpRepository = new HttpRequestRepository();
            HttpRequestDTO httpRequestDTO = new HttpRequestDTO()
            {
                name = "Jhon Castillo",
                date = DateTime.UtcNow,
                requests_sent = 1
            };
            ConfigRequestDTO configRequestDTO = new ConfigRequestDTO()
            {
                ServerURL = "https://bevan1p0d8.execute-api.us-east-1.amazonaws.com/Live",
                TargetRPS = 100,
                AuthKey = "5FQREQBeHX9jTwi1ts3vkaPkdAdUyNfNaITfZ75m",
                UserName = "TODOINSERT"
            };
            var httpResponse = await httpRepository.RequestHTTP(configRequestDTO, httpRequestDTO);
            Assert.NotNull(httpResponse);
            Assert.Equal(httpResponse.GetType(), new HttpResponseDTO().GetType());
        }
        [Fact]
        public async void HttpRequestRepositoryInternetConnectionTest()
        {
            var httpRepository = new HttpRequestRepository();
            HttpRequestDTO httpRequestDTO = new HttpRequestDTO()
            {
                name = "Jhon Castillo",
                date = DateTime.UtcNow,
                requests_sent = 1
            };
            ConfigRequestDTO configRequestDTO = new ConfigRequestDTO()
            {
                ServerURL = "https://bevan1p0d8.execute-api.us-east-1.amazonaws.com/Liveee",
                TargetRPS = 100,
                AuthKey = "5FQREQBeHX9jTwi1ts3vkaPkdAdUyNfNaITfZ75m",
                UserName = "TODOINSERT"
            };
            var httpResponse = await httpRepository.RequestHTTP(configRequestDTO, httpRequestDTO);
            Assert.NotNull(httpResponse);
            Assert.True(httpResponse.isError);
        }
    }
}
