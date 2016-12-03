using AutoReservation.Common.DataTransferObjects.Core;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : DtoBase<AutoDto>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Marke { get; set; }
        [DataMember]
        public int Tagestarif { get; set; }
        [DataMember]
        public int Basistarif { get; set; }
        [DataMember]
        public AutoKlasse AutoKlasse { get; set; }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (Tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && Basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

                return error.ToString();
            }

            public override string ToString()
                => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}";

        }
}
