using System.Runtime.Serialization;

namespace Models.TransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Business
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
        public short Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public short Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string LogoBase64 { get; set; }
    }
}
