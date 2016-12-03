using AutoReservation.Common.DataTransferObjects.Core;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase<KundeDto>
    {
        private int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string vorname;
        [DataMember]
        public string Vorname
        {
            get { return vorname; }
            set
            {
                if (vorname != value)
                {
                    vorname = value;
                    OnPropertyChanged(nameof(Vorname));
                }
            }
        }

        private string nachname;
        [DataMember]
        public string Nachname
        {
            get { return nachname; }
            set
            {
                if (nachname != value)
                {
                    nachname = value;
                    OnPropertyChanged(nameof(Nachname));
                }
            }
        }

        private DateTime geburtsdatum;
        [DataMember]
        public DateTime Geburtsdatum
        {
            get { return geburtsdatum; }
            set
            {
                if (geburtsdatum != value)
                {
                    geburtsdatum = value;
                    OnPropertyChanged(nameof(Geburtsdatum));
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Nachname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Vorname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Geburtsdatum == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}";

    }
}
