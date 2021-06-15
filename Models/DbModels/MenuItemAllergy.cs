using System.Runtime.Serialization;

namespace Models.DbModels
{
    [DataContract]
    public class MenuItemAllergy
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int MenuItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public short AllergyId { get; set; }
    }
}
