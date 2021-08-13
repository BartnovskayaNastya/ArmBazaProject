
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArmBazaProject.Entities
{
    public class NotifyableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
