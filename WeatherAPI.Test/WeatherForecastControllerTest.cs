using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherAPI.Controllers;
using WeatherSourceAPI;

namespace WeatherAPI.Test
{
    [TestClass]
    public class WeatherForecastControllerTest
    {
        private IConfiguration _mockConfiguration;
        private Mock<ILogger<WeatherForecastController>> _mockLogger;
        private Mock<IWeatherSourceAPI> _mockWeatherSourceAPI;

        private WeatherForecastController _sut;

        [TestInitialize]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"AppId", "1234567890"}
            };

            _mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _mockWeatherSourceAPI = new Mock<IWeatherSourceAPI>();

            _sut = new WeatherForecastController(_mockConfiguration, _mockLogger.Object, _mockWeatherSourceAPI.Object);
        }

        [TestMethod]
        public async Task WeatherAPIReturnsSuccess()
        {
            _mockWeatherSourceAPI.Setup(x => x.GetCurrentWeather(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new WeatherSourceAPI.Models.Weather
            {
                Temperature = 2.5,
                Humidity = 80,
                WindSpeed = 24.5,
                Icon = "10d"
            });

            var result = await _sut.Get("London");

            Assert.IsNotNull(result);
            Assert.AreEqual(2.5, result.Temperature);
            Assert.AreEqual(80, result.Humidity);
            Assert.AreEqual(24.5, result.WindSpeed);
            Assert.AreEqual("10d", result.Icon);
        }

        [TestMethod]
        public async Task WeatherAPIReturnsNullForUnknownCity()
        {
            _mockWeatherSourceAPI.Setup(x => x.GetCurrentWeather(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((WeatherSourceAPI.Models.Weather)null);

            var result = await _sut.Get("London");

            Assert.IsNull(result);
        }
    }
}