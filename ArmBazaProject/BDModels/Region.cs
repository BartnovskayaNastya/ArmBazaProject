using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ArmBazaProject.Entities;

namespace ArmBazaProject.Models
{
    public class Region : NotifyableObject
    {
        private string name;
        public virtual ObservableCollection<Team> Teams { get; set; }

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
        public Region()
        {
            Teams = new ObservableCollection<Team>();
        }

        
    }
}

