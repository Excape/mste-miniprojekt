using System.ComponentModel;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Core
{
    [DataContract]
    public abstract class DtoBase<T> : INotifyPropertyChanged, IValidatable
    {
        [DataMember]
        public byte[] RowVersion { get; set; }

        public abstract string Validate();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
