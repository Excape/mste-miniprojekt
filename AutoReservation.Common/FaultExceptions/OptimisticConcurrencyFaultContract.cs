using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.FaultExceptions
{
    [DataContract]
    public class OptimisticConcurrencyFaultContract
    {
        [DataMember]
        public string Operation { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
