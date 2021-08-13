using ArmBazaProject.Entities;

namespace ArmBazaProject
{
    public class WeightCategory : NotifyableObject
    {
        private float weight;
        private string gender;
        private string hand;
        

        public float CategoryWeight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("CategoryWeight");
            }
        }

        

        public string CategoryGender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("CategoryGender");
            }
        }

        public string CategoryHand
        {
            get { return hand; }
            set
            {
                hand = value;
                OnPropertyChanged("CategoryHand");
            }
        }

        public WeightCategory(float weight)
        {
            this.weight = weight;
        }


    }
}
