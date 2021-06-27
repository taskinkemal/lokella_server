using System;
using System.Runtime.Serialization;

namespace Models.DbModels
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CustomerVisit
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int CustomerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int BusinessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime VisitDate { get; set; } = DateTime.Now;

    }
}
