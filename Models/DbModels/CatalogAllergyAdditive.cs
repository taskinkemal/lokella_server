using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class CatalogAllergyAdditive
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
        public string ShortName { get; set; }
    }
}
