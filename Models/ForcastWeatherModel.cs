namespace WeatherApp.Models
{
    class ForecastWeatherModel
    {
        public string Cod { get; set; }
        public int Message { get; set; }
        public int Cnt { get; set; }
        public List<WeatherList> List { get; set; }
        public CityData City { get; set; }
    }

    class WeatherList
    {
        public long Dt { get; set; }
        public MainData Main { get; set; }
        public List<WeatherData> Weather { get; set; }
        public CloudsData Clouds { get; set; }
        public WindData Wind { get; set; }
        public int Visibility { get; set; }
        public double Pop { get; set; }
        public SysData Sys { get; set; }
        public string DtTxt { get; set; }
    }

    class MainData
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }
        public int Humidity { get; set; }
        public double TempKf { get; set; }
    }

    class WeatherData
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    class CloudsData
    {
        public int All { get; set; }
    }

    class WindData
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }
    }

    class SysData
    {
        public string Pod { get; set; }
    }

    class CityData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CoordData Coord { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public int Timezone { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }

    class CoordData
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
