using ArmBazaProject.BDModels;
using ArmBazaProject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArmBazaProject.Models
{
    public class Сompetition : NotifyableObject
    {
        private ObservableCollection<MemberViewModel> members;
        private List<MemberViewModel> membersGirls;
        private List<MemberViewModel> membersBoys;
        private CategoryViewModel[] categoriesG;
        private CategoryViewModel[] categoriesB;

        private float[] girlsWeights;
        private float[] boysWeights;

        private int[] points;

        private string[] girlsWeightsName;
        private string[] boysWeightsName;

        private string categoryName;
        private string name;
        private string location;
        private DateTime date;
        private string competitionJudges;
        private string secretarys;


        #region Properties

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public int[] Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged("Points");
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
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public string[] GirlsWeightsName
        {
            get { return girlsWeightsName; }
            set
            {
                girlsWeightsName = value;
                OnPropertyChanged("GirlsWeightsName");
            }
        }

        public string[] BoysWeightsName
        {
            get { return boysWeightsName; }
            set
            {
                boysWeightsName = value;
                OnPropertyChanged("BoysWeightsName");
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

        public float[] GirlsWeights
        {
            get { return girlsWeights; }
            set
            {
                girlsWeights = value;
                OnPropertyChanged("GirlsWeights");
            }
        }

        public float[] BoysWeights
        {
            get { return boysWeights; }
            set
            {
                boysWeights = value;
                OnPropertyChanged("BoysWeights");
            }
        }

        #endregion


        public Сompetition()
        {
            members = new ObservableCollection<MemberViewModel>();
            membersGirls = new List<MemberViewModel>();
            membersBoys = new List<MemberViewModel>();
        }

        public void SetPoints(List<BDModels.Points> pointsBD)
        {
            points = new int[pointsBD.Count];
            
            for(int i = 0; i < pointsBD.Count; i++)
            {
                points[i] = pointsBD[i].Score;
            }
        }


        public void SetWeights(List<Category> categoryGirls, List<Category> categoryBoys)
        {
            girlsWeights = SetWeight(categoryGirls);
            boysWeights = SetWeight(categoryBoys);

            girlsWeightsName = SetWeightNames(categoryGirls);
            boysWeightsName = SetWeightNames(categoryBoys);

            categoriesG = new CategoryViewModel[girlsWeights.Length];
            categoriesB = new CategoryViewModel[boysWeights.Length];
        }


        private float[] SetWeight(List<Category> categories)
        {
            float[]  someCategory = new float[categories.Count];

            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Weight.EndsWith("+"))
                {
                    someCategory[i] = int.Parse(categories[i-1].Weight) + 1;
                }
                else
                {
                    someCategory[i] = int.Parse(categories[i].Weight);
                }
                
            }
            return someCategory;
        }

        private string[] SetWeightNames(List<Category> categories)
        {
            string[] someCategory = new string[categories.Count];

            for (int i = 0; i < categories.Count; i++)
            {
                someCategory[i] = categories[i].Weight;
            }
            return someCategory;
        }
    }
}
