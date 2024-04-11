using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private const string ApiKey = ""; // Wpisz tutaj swój klucz API
        private string CityName = "Częstochowa"; // Wpisz tutaj nazwę miasta
        private double Lat = int.MaxValue;
        private double Lon = int.MaxValue;


        public MainPage()
        {
            InitializeComponent();
            GetWeather();
        }
        private async void GetWeather()
        {
            try
            {
                if (Lat == int.MaxValue && Lon == int.MaxValue)
                {
                    var geo = await FeatchGeo(CityName);
                    if (double.TryParse(geo.Lat.ToString(), out _))
                    {
                        Lat = geo.Lat;
                        Lon = geo.Lon;
                    }
                }
                else if (CityNameBox.Text != null && CityNameBox.Text != "")
                {
                    var geo = await FeatchGeo(CityNameBox.Text);
                    if (double.TryParse(geo.Lat.ToString(), out _))
                    {
                        Lat = geo.Lat;
                        Lon = geo.Lon;
                    }
                }
                else
                {
                    return;
                }
                int statusCode;

                CurrentWeatherModel CWeather = await FetchCurrentWeather(Lat.ToString(), Lon.ToString());
                if (int.TryParse(CWeather.Cod.ToString(), out statusCode) && statusCode >= 200 && statusCode < 300)
                {
                    DisplayCurrentWeather(CWeather);
                }
                ForecastWeatherModel FWeather = await FetchForcastWeather(Lat.ToString(), Lon.ToString());
                if (int.TryParse(FWeather.Cod.ToString(), out statusCode) && statusCode >= 200 && statusCode < 300)
                {
                    DisplayForcastWeather(FWeather, 8);
                }
            }
            catch (Exception e)
            {
                DisplayAlert("Error", "City not found", "OK");
                return;
            }
           
        }

        private void DisplayCurrentWeather(CurrentWeatherModel weather)
        {
            LCountry1.Text = weather.Sys.Country;
            LTemperature1.Text = weather.Main.Temp.ToString();
            LFeltTemperature1.Text = weather.Main.Feels_like.ToString();
            LPressure1.Text = weather.Main.Pressure.ToString();
            LHumidity1.Text = weather.Main.Humidity.ToString();
            LVisibility1.Text = weather.Visibility.ToString();
            LWindSpeed1.Text = weather.Wind.Speed.ToString();
            LDescription1.Text = weather.Weather[0].Description;
            LCity1.Text = weather.Name;
        }
        private void DisplayForcastWeather(ForecastWeatherModel forecast, int index) 
        {
            LCountry2.Text = forecast.City.Country;
            LTemperature2.Text = forecast.List[index].Main.Temp.ToString();
            LFeltTemperature2.Text = forecast.List[index].Main.FeelsLike.ToString();
            LPressure2.Text = forecast.List[index].Main.Pressure.ToString();
            LHumidity2.Text = forecast.List[index].Main.Humidity.ToString();
            LVisibility2.Text = forecast.List[index].Visibility.ToString();
            LWindSpeed2.Text = forecast.List[index].Wind.Speed.ToString();
            LDescription2.Text = forecast.List[index].Weather[0].Description;
            LCity2.Text = forecast.City.Name;
        }
        
        private static async Task<GeoModel> FeatchGeo(string cityName)
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&appid={ApiKey}");
            var json = await res.Content.ReadAsStringAsync();
            List<GeoModel> Geo = JsonConvert.DeserializeObject<List<GeoModel>>(json);
            return Geo[0];
        }
        private static async Task<ForecastWeatherModel> FetchForcastWeather(string Lat, string Lon)
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"http://api.openweathermap.org/data/2.5/forecast?lat={Lat}&lon={Lon}&cnt=9&units=metric&appid={ApiKey}");
            var json = await res.Content.ReadAsStringAsync();
            ForecastWeatherModel ForecastWeather = JsonConvert.DeserializeObject<ForecastWeatherModel>(json);
            return ForecastWeather;
        }
        private static async Task<CurrentWeatherModel> FetchCurrentWeather(string Lat, string Lon)
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={Lat}&lon={Lon}&units=metric&appid={ApiKey}");
            var json = await res.Content.ReadAsStringAsync();
            CurrentWeatherModel CurrentWeather = JsonConvert.DeserializeObject<CurrentWeatherModel>(json);
            return CurrentWeather;
        }

        private void btnNameSearch_Clicked(object sender, EventArgs e)
        {
            GetWeather();
        }
    }

}