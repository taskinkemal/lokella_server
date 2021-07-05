using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.TransferObjects
{
    [DataContract]
    public class MenuCategoryList
    {
        [DataMember]
        public List<int> Items { get; set; }
    }
}
