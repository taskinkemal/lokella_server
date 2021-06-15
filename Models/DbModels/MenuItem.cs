using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class MenuItem
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
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int PhotoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ItemOrder { get; set; }
    }
}
