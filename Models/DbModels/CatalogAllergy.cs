using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class CatalogAllergy
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
    }
}
