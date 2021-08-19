using ArmBazaProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmBazaProject.BDModels
{
    public class Points : NotifyableObject
    {
        private string name;
        private int place;
        private int score;

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

        public int Place
        {
            get { return place; }
            set
            {
                place = value;
                OnPropertyChanged("Place");
            }
        }

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        public Points()
        {

        }

    }
}

