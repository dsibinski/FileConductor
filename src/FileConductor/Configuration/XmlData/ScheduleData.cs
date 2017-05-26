using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class ScheduleData
    {

        [XmlElement("interval")]
        public string Interval { get; set; }

        [XmlElement("daysOfWeek")]
        public string DaysOfWeek { get; set; }

        [XmlElement("hours")]
        public string Hours { get; set; }

        [XmlElement("code")]
        public string Code { get; set; }
    }
}
