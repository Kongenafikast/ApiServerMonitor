using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace APIServerMonitor.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int GroupId { get; set; }
        public decimal Value { get; set; }
        public DateTime Created { get; set; }
    }
    public class SensorType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

    }
    public class SensorGroup
    {
        public int Id { get; set; }
        public SensorType TypeId { get; set; }
    }
    public class SensorMain
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public SensorGroup GroupId { get; set; }
    }
    public class SensorDataFromSensor
    {
        public int GroupId { get; set; }
        public float Temprature { get; set; }
        public float Humitity { get; set; }
        public float Co2 { get; set; }
        public float TVOC { get; set; }


    }
}
