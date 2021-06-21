using System;
using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class SpecialOffer
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int BusinessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int FileId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public byte Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime DateTo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HourFrom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HourTo { get; set; }

        public bool ActiveHours(DateTime date)
        {
            var minute = date.Hour * 60 + date.Minute;

            if (HourFrom == null || HourTo == null)
            {
                return true;
            }

            return Parse(HourFrom) <= minute && Parse(HourTo) >= minute;
        }

        private static int Parse(string value)
        {
            var hour = 0;
            var minute = 0;

            if (value != null)
            {
                var parsed = value.Split(':', StringSplitOptions.None);
                hour = Convert.ToInt32(parsed[0]);
                minute = Convert.ToInt32(parsed[1]);
            }

            return hour * 60 + minute;
        }
    }
}
