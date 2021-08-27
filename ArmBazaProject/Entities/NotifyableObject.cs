
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace ArmBazaProject.Entities
{
    [Serializable]
    public class NotifyableObject : INotifyPropertyChanged
    {
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
