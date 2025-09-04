using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omgevingsmonitor_configurator
{
    public class Box
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Exposure { get; set; }
        public string Model { get; set; }
        public List<string> Grouptag { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CurrentLocation currentLocation { get; set; }
        public List<Sensor> Sensors { get; set; }
        public DateTime LastMeasurementAt { get; set; }
        public List<Location> Loc { get; set; }
        public Integrations integrations { get; set; }
        public string AccessToken { get; set; }
        public bool UseAuth { get; set; }

        public class CurrentLocation
        {
            public string Type { get; set; }
            public List<double> Coordinates { get; set; }
            public DateTime Timestamp { get; set; }
        }

        //public class CurrentLocation
        //{
        //    public double lng { get; set; }
        //    public double lat { get; set; }
        //    public double height { get; set; }
        //}

        public class Sensor
        {
            public string _id { get; set; }
            public string title { get; set; }
            public string unit { get; set; }
            public string sensorType { get; set; }
            public string icon { get; set; }
            public LastMeasurement lastMeasurement { get; set; }
        }

        public class LastMeasurement
        {
            public DateTime CreatedAt { get; set; }
            public string Value { get; set; }
        }

        public class Location
        {
            public Geometry Geometry { get; set; }
            public string Type { get; set; }
        }

        public class Geometry
        {
            public string Type { get; set; }
            public List<double> Coordinates { get; set; }
            public DateTime Timestamp { get; set; }
        }

        public class Integrations
        {
            public Mqtt Mqtt { get; set; }
        }

        public class Mqtt
        {
            public bool Enabled { get; set; }
        }
    }

    public class SensorIcon
    {
        public const string moisture = "osem-moisture";
        public const string temperatureC = "osem-temperature-celsius";
        public const string temperatureF = "osem-temperature-fahrenheit";
        public const string thermometer = "osem-thermometer";
        public const string windspeed = "osem-windspeed";
        public const string sprinkles = "osem-sprinkles";
        public const string brightness = "osem-brightness";
        public const string barometer = "osem-barometer";
        public const string humidity = "osem-humidity";
        public const string notAvailable = "osem-not-available";
        public const string gauge = "osem-gauge";
        public const string umbrella = "osem-umbrella";
        public const string clock = "osem-clock";
        public const string shock = "osem-shock";
        public const string fire = "osem-fire";
        public const string volume = "osem-volume-up";
        public const string cloud = "osem-cloud";
        public const string dashboard = "osem-dashboard";
        public const string particulateMatter = "osem-particulate-matter";
        public const string signal = "osem-signal";
        public const string microphone = "osem-microphone";
        public const string wifi = "osem-wifi";
        public const string battery = "osem-battery";
        public const string radioactive = "osem-radioactive";
        public const string co2 = "osem-co2";
    }
}
