using System.Collections.ObjectModel;
using ArmBazaProject.Entities;
using ArmBazaProject.ViewModels;
using System;

namespace ArmBazaProject.Models
{
    [Serializable]
    public class Qualification : NotifyableObject
    {
        private string name;
        ObservableCollection<Member> Members { get; set; }

        public int Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public Qualification()
        {
            Members = new ObservableCollection<Member>();
        }

    }
}

