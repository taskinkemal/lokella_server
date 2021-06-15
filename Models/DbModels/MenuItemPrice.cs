using System;
using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class MenuItemPrice
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
        public int MenuItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Unit { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }


    }
}
