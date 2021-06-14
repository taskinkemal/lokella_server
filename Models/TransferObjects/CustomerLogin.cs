using System;
using System.Runtime.Serialization;
using Models.DbModels;

namespace Models.TransferObjects
{
    [DataContract]
    public class CustomerLogin : Customer
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DeviceId { get; set; }
    }
}
