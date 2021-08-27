using ArmBazaProject.Models;
using System.Collections.ObjectModel;
using ArmBazaProject.Entities;
using System;

namespace ArmBazaProject
{
    [Serializable]
    public class Team : NotifyableObject
    {
        //int regionId;
        string name;
        string trainerName;

        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public virtual ObservableCollection<Member> Members { get; set; }

        public int Id { get; set; }

        public string TrainerName
        {
            get { return trainerName; }
            set
            {
                trainerName = value;
                OnPropertyChanged("TrainerName");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

    


    public Team()
        {
            Members = new ObservableCollection<Member>();
        }

        public Team(string name)
        {
            this.name = name;
        }


    }
}
