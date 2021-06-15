using System;
using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class MenuItemTag
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int MenuItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public short TagId { get; set; }
    }
}
