using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class CatalogMenuItemTag
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public short Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int PhotoId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public short ItemOrder { get; set; }
    }
}
