using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class MenuCategory
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
        public int BusinessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ItemOrder { get; set; }

    }
}
