using System.Runtime.Serialization;

namespace Models.DbModels
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BusinessCategory
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
