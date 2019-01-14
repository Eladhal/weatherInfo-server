using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weatherInfoProj.Models
{
    public class APIWeather
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }
    public class Current
    {
        public float temp { get; set; }
        public float wind { get; set; }
        public float feelslike { get; set; }
    }
    public class Location
    {
        public string name { get; set; }
    }
}
