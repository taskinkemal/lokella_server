using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Models.DbModels;

namespace Models.TransferObjects
{
    [DataContract]
    public class MenuItemTo
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public MenuItem Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public MenuCategory Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<MenuItemPrice> Prices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<CatalogAdditive> Additives { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<CatalogAllergy> Allergies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<CatalogMenuItemTag> Tags { get; set; }
    }
}
