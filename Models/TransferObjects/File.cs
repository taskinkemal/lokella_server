using System.Runtime.Serialization;

namespace Models.TransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class File
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string FileBase64 { get; set; }
    }
}
