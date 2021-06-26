using System.Runtime.Serialization;

namespace Models.DbModels
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BusinessInfo
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
        public string FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int PostCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string WebSite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Facebook { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Instagram { get; set; }

    }
}
