using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArmBazaProject.Entities
{
    public class TeamModel : NotifyableObject
    {
        private string name;
        private ObservableCollection<MemberViewModel> members;
        private int place;
        private int score;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
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

        public int Place
        {
            get { return place; }
            set
            {
                place = value;
                OnPropertyChanged("Place");
            }
        }

        public ObservableCollection<MemberViewModel> Members
        {
            get { return members; }
            set
            {
                members = value;
                OnPropertyChanged("Members");
            }
        }

        public TeamModel(string name)
        {
            this.name = name;
            members = new ObservableCollection<MemberViewModel>();
        }

       
    }
}
