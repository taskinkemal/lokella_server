using System;
using System.Runtime.Serialization;

namespace Models.DbModels
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BusinessUser
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int BusinessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public short Role { get; set; }

    }
}