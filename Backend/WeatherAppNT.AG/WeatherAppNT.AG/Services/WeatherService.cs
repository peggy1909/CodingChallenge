using Microsoft.Data.Sqlite;
using System.Reflection.PortableExecutable;
using WeatherAppNT.AG.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        public WeatherService()
        {

        }

        public WeatherModel GetWeather(string city)
        {
            var weatherData = new WeatherModel();

            using (var connection = new SqliteConnection("Data Source=../../Database/weather.db")) 
            {
                connection.Open();

                int id = 0;

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM weatherData
                    WHERE name = $name
                ";
                command.Parameters.AddWithValue("$name", city.ToLower());

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    var main = new Main()
                    {
                        temp = reader.GetDouble(reader.GetOrdinal("temp")),
                        temp_min = reader.GetDouble(reader.GetOrdinal("temp_min")),
                        temp_max = reader.GetDouble(reader.GetOrdinal("temp_max")),
                        pressure = reader.GetInt32(reader.GetOrdinal("pressure")),
                        humidity = reader.GetInt32(reader.GetOrdinal("humidity"))
                    };

                    var wind = new Wind()
                    {
                        speed = reader.GetDouble(reader.GetOrdinal("wind_speed")),
                        deg = reader.GetInt32(reader.GetOrdinal("wind_deg"))
                    };

                    var sys = new Sys()
                    {
                        country = reader.GetString(reader.GetOrdinal("country"))
                    };

                    weatherData.name = reader.GetString(reader.GetOrdinal("name"));
                    weatherData.main = main;
                    weatherData.wind = wind;
                    weatherData.sys = sys;

                    id = reader.GetInt32(reader.GetOrdinal("id"));
                }

                var command2 = connection.CreateCommand();
                command2.CommandText =
                @"
                    SELECT *
                    FROM weather
                    WHERE weather_data_id = $id
                ";
                command2.Parameters.AddWithValue("$id", id);

                using (var reader = command2.ExecuteReader())
                {
                    reader.Read();

                    var weather = new Weather()
                    {
                        main = reader.GetString(reader.GetOrdinal("main")),
                        description = reader.GetString(reader.GetOrdinal("description")),
                        icon = reader.GetString(reader.GetOrdinal("icon")),
                    };
                    weatherData.weather = new List<Weather> { weather };
                }
            }

            return weatherData;
        }
    }
}
