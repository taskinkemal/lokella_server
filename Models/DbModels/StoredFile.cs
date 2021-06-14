using System.Runtime.Serialization;

namespace Models.DbModels
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class StoredFile
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
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public byte[] FileContent { get; set; }
    }
}
