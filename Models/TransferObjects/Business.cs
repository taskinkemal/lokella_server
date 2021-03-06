using System.Runtime.Serialization;
using Models.DbModels;

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

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string FontColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MenuSectionColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public BusinessInfo BusinessInfo { get; set; }


    }
}
