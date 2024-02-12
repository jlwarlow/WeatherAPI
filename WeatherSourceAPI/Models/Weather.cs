namespace WeatherSourceAPI.Models;

public class Weather
{
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double WindSpeed { get; set; }
    public string? Icon { get; set; }
}
