using System;
using System.Runtime.Serialization;

namespace Models.DbModels
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UserToken
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime InsertedAt { get; set; } = DateTime.Now;

    }
}
