using ArmBazaProject.Entities;

namespace ArmBazaProject
{
    public class WeightCategory : NotifyableObject
    {
        private float weight;
        private string gender;
        private string hand;
        private string weightName;
        

        public float CategoryWeight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("CategoryWeight");
            }
        }

        public string WeightName
        {
            get { return weightName; }
            set
            {
                weightName = value;
                OnPropertyChanged("WeightName");
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

        public WeightCategory(float weight, string name)
        {
            this.weight = weight;
            weightName = name;
        }


    }
}
