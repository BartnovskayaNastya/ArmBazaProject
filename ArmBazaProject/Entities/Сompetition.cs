using ArmBazaProject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArmBazaProject.Models
{

    public struct PointsData
    {
        public PointsData(int intPlace, int intPoints)
        {
            Place = intPlace;
            Points = intPoints;
        }

        public int Place { get; private set; }
        public int Points { get; private set; }
    }
    public class Сompetition : NotifyableObject
    {
        private ObservableCollection<MemberViewModel> members;
        private List<MemberViewModel> membersGirls;
        private List<MemberViewModel> membersBoys;
        private CategoryViewModel[] categoriesG;
        private CategoryViewModel[] categoriesB;


        public int[] girlsWeightsSTANDART = new int[6] { 50, 55, 60, 65, 70, 71};
        public int[] boysWeightsSTANDART = new int[9] { 60, 65, 70, 75, 80, 85, 90, 100, 101 };
        public int[] girlsWeightsSENIOR = new int[7] { 50, 55, 60, 65, 70, 80, 81 };
        public int[] boysWeightsSENIOR = new int[11] { 55, 60, 65, 70, 75, 80, 85, 90, 100, 110, 111 };
        public int[] girlsWeightsYOUTH21 = new int[6] { 50, 55, 60, 65, 70, 71 };
        public int[] boysWeightsYOUTH21 = new int[9] { 55, 60, 65, 70, 75, 80, 85, 90, 91 };

        private List<PointsData> pointsStudent = new List<PointsData>() { new PointsData(1, 36), new PointsData(2, 33),
        new PointsData(3, 30), new PointsData(4, 27), new PointsData(5, 24), new PointsData(6, 21), new PointsData(7, 19),
        new PointsData(8, 17), new PointsData(9, 15), new PointsData(10, 13),new PointsData(11, 11),new PointsData(12, 10),
        new PointsData(13, 9), new PointsData(14, 8), new PointsData(15, 7),new PointsData(16, 6),new PointsData(17, 5),
        new PointsData(18, 4), new PointsData(19, 3), new PointsData(20, 2),new PointsData(21, 1)};

        private List<PointsData> pointsStandart = new List<PointsData>() { new PointsData(1, 25), new PointsData(2, 17),
        new PointsData(3, 9), new PointsData(4, 5), new PointsData(5, 3), new PointsData(6, 2)};

        public List<PointsData> pointsDatas = new List<PointsData>();

        public int[] girlsWeights;
        public int[] boysWeights;

        private string[] genders = new string[] { "м", "ж" };
        private string[] hands = new string[] { "Левая", "Правая", "Обе"};


        private string categotyName;
        private string name;
        private string location;
        private DateTime date;
        private string competitionJudges;
        private string secretarys;


        public string[] Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                OnPropertyChanged("Genders");
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public string Secretarys
        {
            get { return secretarys; }
            set
            {
                secretarys = value;
                OnPropertyChanged("Secretarys");
            }
        }
        public string Judges
        {
            get { return competitionJudges; }
            set
            {
                competitionJudges = value;
                OnPropertyChanged("Judges");
            }
        }
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
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

        public string CategoryName
        {
            get { return categotyName; }
            set
            {
                categotyName = value;
                OnPropertyChanged("CategoryName");
            }
        }


        public string[] Hands
        {
            get { return hands; }
            set
            {
                hands = value;
                OnPropertyChanged("Hands");
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

        public CategoryViewModel[] CategoriesG
        {
            get { return categoriesG; }
            set
            {
                categoriesG = value;
                OnPropertyChanged("CategoriesG");
            }
        }

        public CategoryViewModel[] CategoriesB
        {
            get { return categoriesB; }
            set
            {
                categoriesB = value;
                OnPropertyChanged("CategoriesB");
            }
        }

        public List<MemberViewModel> MembersGirls
        {
            get { return membersGirls; }
            set
            {
                membersGirls = value;
                OnPropertyChanged("MembersGirls");
            }
        }

        public List<MemberViewModel> MembersBoys
        {
            get { return membersBoys; }
            set
            {
                membersBoys = value;
                OnPropertyChanged("MembersBoys");
            }
        }

        public int[] GirlsWeights
        {
            get { return girlsWeights; }
            set
            {
                girlsWeights = value;
                OnPropertyChanged("GirlsWeights");
            }
        }

        public int[] BoysWeights
        {
            get { return boysWeights; }
            set
            {
                boysWeights = value;
                OnPropertyChanged("BoysWeights");
            }
        }




        public Сompetition()
        {
            members = new ObservableCollection<MemberViewModel>();
            membersGirls = new List<MemberViewModel>();
            membersBoys = new List<MemberViewModel>();
            /*switch (name)
            {
                case "Senior":
                    girlsWeights = girlsWeightsSENIOR;
                    boysWeights = boysWeightsSENIOR;
                    break;
                case "Standart":
                    girlsWeights = girlsWeightsSTANDART;
                    boysWeights = boysWeightsSTANDART;
                    break;
                case "Youth":
                    girlsWeights = girlsWeightsYOUTH21;
                    boysWeights = boysWeightsYOUTH21;
                    break;

            }

            switch (points)
            {
                case "Student":
                    pointsDatas = pointsStudent;
                    break;
                case "Standart":
                    pointsDatas = pointsStandart;
                    break;

            }
            */
            categoriesG = new CategoryViewModel[girlsWeights.Length];
            categoriesB = new CategoryViewModel[boysWeights.Length];




        }

    }
}
