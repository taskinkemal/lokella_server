using System;
using System.Runtime.Serialization;
using Models.DbModels;

namespace Models.TransferObjects
{
    [DataContract]
    public class CustomerVisit
    {
        [DataMember]
        public Customer Visitor { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }
    }
}
