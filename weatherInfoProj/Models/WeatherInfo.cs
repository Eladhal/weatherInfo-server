using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weatherInfoProj.Models
{
    public class WeatherInfo
    {
        public string city { get; set; }
        public float tmp { get; set; }
        public float feelLike { get; set; }
        public float wind { get; set; }
    }
}
