using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using weatherInfoProj.Models;

namespace weatherInfoProj.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        readonly string urlGetCity = "http://api.apixu.com/v1/current.json?key=43c1bdb61d85452b803192306190301&q=";
        readonly string urlSearch = "http://api.apixu.com/v1/search.json?key=43c1bdb61d85452b803192306190301&q=";

        [HttpPost("[action]")]
        public WeatherInfo GetWeather([FromBody]string city)
        {
            // Call weather api
            return cityWeatherInfo(city);
        }

        [HttpPost("[action]")]
        public WeatherInfo[] GetCitiesForUsers(UserInfo userInfo)
        {
            // The DB will look like: 
            //{
            //   userName: aaa,
            //    password:bbb,
            //    cities: [cityName1,cityName2,cityName3]

            //}

            // query the db and get the cities belong to user
            var citiesFromDb = new string[] { "aaa", "bbb", "ccc" };
            WeatherInfo[] wetInfoArr = new WeatherInfo[3];
            for (int ind =0; ind< citiesFromDb.Length;ind++)
            {
                wetInfoArr[ind] = cityWeatherInfo(citiesFromDb[ind]);
            }

            return wetInfoArr;
       }

        [HttpPost("[action]")]
        public void Add(WeatherInfo weatherInfo, UserInfo userInfo)
        {
            // Retrieve from DB the cities of user
            // if array is not full 
               // add another city 
            //else
               // add the new one to the start of array and make shift for the other two
        }

        [HttpGet] string[] searchCities(string term)
        {
            string fullUrl = urlSearch + term;

            string weathers = new WebClient().DownloadString(fullUrl);

            CityInfo[] weatherObjs = JsonConvert.DeserializeObject<CityInfo[]>(weathers);

            string[] citiesNames = weatherObjs.Select(weather => weather.name.Split(',')[0]).Take(5).ToArray();

            return citiesNames;
        }

        private WeatherInfo cityWeatherInfo(string city)
        {
            string fullUrl = urlGetCity + city;

            string weather = new WebClient().DownloadString(fullUrl);

            APIWeather weatherObj = JsonConvert.DeserializeObject<APIWeather>(weather);

            // Return result to client
            return new WeatherInfo
            {
                city = weatherObj.location.name,
                tmp = weatherObj.current.temp,
                wind = weatherObj.current.wind,
                feelLike = weatherObj.current.feelslike
            };
        }
    }
}
