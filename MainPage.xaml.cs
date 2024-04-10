using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp.Models;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private const string ApiKey = ""; // Wpisz tutaj swój klucz API
        private string CityName = "Częstochowa"; // Wpisz tutaj nazwę miasta
        private int Lat = int.MaxValue;
        private int Lon = int.MaxValue;


        public MainPage()
        {
            InitializeComponent();
            GetWeather();
        }

        private async void GetWeather()
        {
            if (Lat == int.MaxValue || Lon == int.MaxValue)
            {
                var geo = await FeatchGeo(CityName);
                if (double.TryParse(geo.Lat.ToString(), out _))
                {
                    geo.Lat = Lat;
                    geo.Lon = Lon;
                }
            }

            CurrentWeatherModel CWeather = await FetchCurrentWeather(Lat.ToString(), Lon.ToString());
            if (int.TryParse(CWeather.Cod.ToString(), out int statusCode) && statusCode >= 200 && statusCode < 300)
            {
                DisplayCurrentWeather(CWeather);
            }
        }

        private void DisplayCurrentWeather(CurrentWeatherModel weather)
        {
            _ = 0;
        }
        
        private static async Task<GeoModel> FeatchGeo(string cityName)
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&appid={ApiKey}");
            var json = await res.Content.ReadAsStringAsync();
            GeoModel Geo = JsonConvert.DeserializeObject<GeoModel>(json);
            return Geo;
        }
        private static async Task<ForecastWeatherModel> FetchForcastWeather(string Lat, string Lon)
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"http://api.openweathermap.org/data/2.5/forecast?lat={Lat}&lon={Lon}&units=metric&appid={ApiKey}");
            var json = await res.Content.ReadAsStringAsync();
            ForecastWeatherModel ForecastWeather = JsonConvert.DeserializeObject<ForecastWeatherModel>(json);
            return ForecastWeather;
        }
        private static async Task<CurrentWeatherModel> FetchCurrentWeather(string Lat, string Lon)
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={Lat}&lon={Lon}&appid={ApiKey}");
            var json = await res.Content.ReadAsStringAsync();
            CurrentWeatherModel CurrentWeather = JsonConvert.DeserializeObject<CurrentWeatherModel>(json);
            return CurrentWeather;
        }
    }

}