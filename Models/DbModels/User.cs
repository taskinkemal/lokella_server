﻿using System.Runtime.Serialization;

namespace Models.DbModels
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class User
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
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public short Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PasswordSalt { get; set; }

    }
}
