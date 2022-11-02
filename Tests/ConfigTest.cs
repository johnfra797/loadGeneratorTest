using System;
using mParticle.Domain.DTO;
using Xunit;

namespace Tests
{
    public sealed class ConfigTest
    {
        [Fact]
        public void Config_Null()
        {
            // Test a null path.
            ConfigRequestDTO arguments = ConfigRequestDTO.GetArguments(null);
            Assert.Null(arguments);

            // Test a path that does not name a file.
            arguments = ConfigRequestDTO.GetArguments("Gazonk+7");
            Assert.Null(arguments);
        }

        [Fact]
        public void Config_Valid()
        {
            // Test well-formed arguments.
            ConfigRequestDTO config = ConfigRequestDTO.ParseArguments("{ \"serverURL\" : \"https://www.mparticle.com\" , \"targetRPS\" : 10, \"authKey\" : \"Whatever\", \"userName\" : \"Fred\"}");
            Assert.NotNull(config);
            Assert.Equal("https://www.mparticle.com", config.ServerURL);
            Assert.Equal(10u, config.TargetRPS);
            Assert.Equal("Whatever", config.AuthKey);
            Assert.Equal("Fred", config.UserName);
        }

        [Fact]
        public void Config_MissingRequired()
        {
            // Test missing arguments.
            ConfigRequestDTO config = ConfigRequestDTO.ParseArguments("{ \"targetRPS\" : 10, \"authKey\" : \"Whatever\", \"userName\" : \"Fred\" }");
            Assert.Null(config);

            config = ConfigRequestDTO.ParseArguments("{ \"serverURL\" : \"https://www.mparticle.com\" , \"authKey\" : \"Whatever\", \"userName\" : \"Fred\"  }");
            Assert.Null(config);

            config = ConfigRequestDTO.ParseArguments("{ \"serverURL\" : \"https://www.mparticle.com\" , \"targetRPS\" : 10, \"userName\" : \"Fred\" }");
            Assert.Null(config);

            config = ConfigRequestDTO.ParseArguments("{ \"serverURL\" : \"https://www.mparticle.com\" , \"targetRPS\" : 10, \"authKey\" : \"Whatever\" }");
            Assert.Null(config);
        }

        [Fact]
        public void Config_InvalidValues()
        {
            // Test arguments with invalid values.
            ConfigRequestDTO config = ConfigRequestDTO.ParseArguments("{ \"serverURL\" : \"https://www.mparticle.com\" , \"targetRPS\" : 0, \"authKey\" : \"Whatever\", \"userName\" : \"Fred\"}");
            Assert.Null(config);

            config = ConfigRequestDTO.ParseArguments("{ \"serverURL\" : \"https://www.mparticle.com\" , \"targetRPS\" : 10, \"authKey\" : \"\", \"userName\" : \"Fred\" }");
            Assert.Null(config);

            config = ConfigRequestDTO.ParseArguments("{ \"serverURL\" : \"https://www.mparticle.com\" , \"targetRPS\" : 10, \"authKey\" : \"Whatever\", \"userName\" : \"\" }");
            Assert.Null(config);
        }
    }
}
