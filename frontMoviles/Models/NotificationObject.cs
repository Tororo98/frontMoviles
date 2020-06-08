using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace frontMoviles.Models
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}