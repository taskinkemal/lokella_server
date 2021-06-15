using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class MenuItemAdditive
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
        public short AdditiveId { get; set; }
    }
}
