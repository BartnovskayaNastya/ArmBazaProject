using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmBazaProject.Entities
{
    class ProtocolTeam : NotifyableObject
    {
        #region переменные
        private List<string> pointsLeftHandVM;
        private List<string> pointsRightHandVM;
        private List<string> categories;
        private List<Points> pointsLeftHand;
        private List<Points> pointsRightHand;

        private int resultLeftHand;
        private int resultRightHand;

        private int totalResult;
        #endregion

        private int scoreG;
        private int scoreB;

        private int placeB;
        private int placeG;

        private int totalScore;
        private int totalPlace;

        

        private string name;

        #region properties

        public int ScoreG
        {
            get { return scoreG; }
            set
            {
                scoreG = value;
                OnPropertyChanged("ScoreG");
            }
        }

        public int ScoreB
        {
            get { return scoreB; }
            set
            {
                scoreB = value;
                OnPropertyChanged("ScoreB");
            }
        }

        public int PlaceB
        {
            get { return placeB; }
            set
            {
                placeB = value;
                OnPropertyChanged("PlaceB");
            }
        }

        public int PlaceG
        {
            get { return placeG; }
            set
            {
                placeG = value;
                OnPropertyChanged("PlaceG");
            }
        }

        public int TotalScore
        {
            get { return totalScore; }
            set
            {
                totalScore = value;
                OnPropertyChanged("TotalScore");
            }
        }

        public int TotalPlace
        {
            get { return totalPlace; }
            set
            {
                totalPlace = value;
                OnPropertyChanged("ResultLeftHandG");
            }
        }

        public int TotalResult
        {
            get { return totalResult; }
            set
            {
                totalResult = value;
                OnPropertyChanged("TotalResult");
            }
        }

        public int ResultRightHand
        {
            get { return resultRightHand; }
            set
            {
                resultRightHand = value;
                OnPropertyChanged("ResultRightHand");
            }
        }

        public int ResultLeftHand
        {
            get { return resultLeftHand; }
            set
            {
                resultLeftHand = value;
                OnPropertyChanged("ResultLeftHand");
            }
        }

        public List<Points> PointsRightHand
        {
            get { return pointsRightHand; }
            set
            {
                pointsRightHand = value;
                OnPropertyChanged("PointsRightHand");
            }
        }

        public List<Points> PointsLeftHand
        {
            get { return pointsLeftHand; }
            set
            {
                pointsLeftHand = value;
                OnPropertyChanged("PointsLeftHand");
            }
        }

        public List<string> PointsLeftHandVM
        {
            get { return pointsLeftHandVM; }
            set
            {
                pointsLeftHandVM = value;
                OnPropertyChanged("PointsLeftHandVM");
            }
        }

        public List<string> PointsRightHandVM
        {
            get { return pointsRightHandVM; }
            set
            {
                pointsRightHandVM = value;
                OnPropertyChanged("PointsRightHandVM");
            }
        }

        public List<string> Сategories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged("Сategories");
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

        #endregion

        public ProtocolTeam(string teamName)
        {
            name = teamName;

            pointsLeftHand = new List<Points>();
            pointsRightHand = new List<Points>();
            categories = new List<string>();
            pointsLeftHandVM = new List<string>();
            pointsRightHandVM = new List<string>();
        }

    }
}
