using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using ArmBazaProject.BDModels;
using ArmBazaProject.Models;

namespace ArmBazaProject.ViewModels
{
    class CompetitionViewModel : ViewModelBase
    {
        readonly Random random = new Random(Guid.NewGuid().GetHashCode());

        private Сompetition сompetitionLeftHand;
        private Сompetition сompetitionRightHand;
        private ObservableCollection<MemberViewModel> allMembers;
        private DataBaseModel dataBaseModel;
        private MemberViewModel someMember;

        public double manLimit;
        public double womanLimit;



        private string[] namesB = new string[] { "Саша", "Егор", "Ваня", "Артем",
            "Илья", "Миша", "Костя", "Игорь", "Захар","Борис", "Витя", "Вова", "Валера",
        "Кирилл", "Леня", "Глеб", "Гена","Денис", "Лев", "Дима", "Олег"};

        private string[] secondNamesB = new string[] { "Иванов", "Петров", "Давыдов", "Пушкин",
            "Лермонтов", "Высоцкий", "Блок", "Даль", "Гусев","Горох", "Солодков", "Малиновский", "Бартновский",
        "Фадеев", "Куприн", "Булгаков", "Толстой","Твен", "Ломоносов", "Орехов", "Лодков"};

        private string[] namesG = new string[] { "Катя", "Настя", "Лида", "Маша",
            "Даша", "Лена", "Алена", "Аня", "Вика","Ангелина", "Диана", "Надя", "Карина",
        "Аврора", "Агния", "Мила", "Саша","Люба", "Оля", "Эмира", "Вера"};

        private string[] secondNamesG = new string[] { "Барто", "Брежнева", "Авдеева", "Бабушкина",
            "Бородина", "Васильева", "Глухова", "Гарбузова", "Давыдова","Егорова", "Жилина", "Зайцева", "Казакова",
        "Карустина", "Козлова", "Лапина", "Леонова","Макеева", "Акулова", "Павлова", "Рубцова"};
        public string[] genders = new string[] { "м", "ж" };
        public string[] hands = new string[] { "Правая", "Левая", "Обе" };
        public string[] teams = new string[] {};
        public string[] points = new string[] { "STUDENTS", "STANDART" };
        public string[] categories = new string[] { "STANDART", "SENIOR", "YOUTH 21", "JUNIOR 18",
            "MASTER", "TOURNAMENT1", "TOURNAMENT2", "TOURNAMENT3", "CUP2021" };

        #region Свойства доступа
        public Сompetition CompetitionLeftHand
        {
            get { return сompetitionLeftHand; }
            set
            {
                сompetitionLeftHand = value;
                OnPropertyChanged("CompetitionLeftHand");
            }
        }

        public ObservableCollection<MemberViewModel> AllMembers
        {
            get { return allMembers; }
            set
            {
                allMembers = value;
                OnPropertyChanged("AllMembers");
            }
        }

        public Сompetition CompetitionRightHand
        {
            get { return сompetitionRightHand; }
            set
            {
                сompetitionRightHand = value;
                OnPropertyChanged("CompetitionRightHand");
            }
        }

        public DataBaseModel DataBaseModel
        {
            get { return dataBaseModel; }
            set
            {
                dataBaseModel = value;
            }
        }

        #endregion

        public CompetitionViewModel()
        {
            сompetitionLeftHand = new Сompetition();
            сompetitionRightHand = new Сompetition();
            allMembers = new ObservableCollection<MemberViewModel>();
            someMember = new MemberViewModel();
            dataBaseModel = new DataBaseModel();

            //for (int i = 0; i < 200; i++)
            //{
            //    MemberViewModel member = new MemberViewModel();
            //    member.Member.Weight = random.Next(45, 110);
            //    member.Member.Gender = genders[random.Next(0, 2)];
            //    member.Hand = hands[random.Next(0, 3)];
            //    if (member.Member.Gender == "м")
            //    {
            //        member.Member.FullName = namesB[random.Next(0, namesB.Length)] +
            //            " " + secondNamesB[random.Next(0, secondNamesB.Length)];
            //    }
            //    else
            //    {
            //        member.Member.FullName = namesG[random.Next(0, namesG.Length)] +
            //            " " + secondNamesG[random.Next(0, secondNamesG.Length)];
            //    }
            //    member.TeamName = dataBaseModel.GetAllTeams()[random.Next(0, dataBaseModel.GetAllTeams().Length)];
            //    if (member.Hand == "Обе")
            //    {
            //        member.IsSportTeamLeftHand = true;
            //        member.IsSportTeamRightHand = true;
            //    }
            //    else if (member.Hand == "Левая")
            //    {
            //        member.IsSportTeamLeftHand = true;
            //    }
            //    else
            //    {
            //        member.IsSportTeamRightHand = true;
            //    }

            //    allMembers.Add(member);
            //}
        }

        #region Данные из БД

        public IEnumerable<MemberViewModel> GetInfoAboutMembersByParam(string param)
        {
            return dataBaseModel.GetAllMembersByParam(param);
        }

        #endregion


        #region Сортировка данных
        public void SetWeights(List<Category> categoryGirls, List<Category> categoryBoys)
        {
            CompetitionLeftHand.SetWeights(categoryGirls, categoryBoys);
            CompetitionRightHand.SetWeights(categoryGirls, categoryBoys);
        }

        private void SortByHand()
        {
            for(int i = 0; i < allMembers.Count; i++)
            {
                someMember = (MemberViewModel)allMembers[i].Clone();
                switch (allMembers[i].Hand)
                {
                    case "Левая":
                        someMember.isLeftHand = true;
                        сompetitionLeftHand.Members.Add(someMember);
                        break;
                    case "Правая":
                        someMember.isRightHand = true;
                        сompetitionRightHand.Members.Add(someMember);
                        break;
                    case "Обе":
                        someMember.isLeftHand = true;
                        someMember.isRightHand = true;
                        сompetitionLeftHand.Members.Add(someMember);
                        someMember = (MemberViewModel)allMembers[i].Clone();
                        сompetitionRightHand.Members.Add(someMember);
                        break;
                }
            }
        }
        private void SortByGender(Сompetition сompetition)
        {
            foreach (MemberViewModel member in сompetition.Members)
            {
                if (member.Member.Gender == "ж")
                {
                    сompetition.MembersGirls.Add(member);
                }
                else if (member.Member.Gender == "м")
                {
                    сompetition.MembersBoys.Add(member);
                }
            }

        }

        private void SetGroupWeight(CategoryViewModel[] categories, float[] weights, List<MemberViewModel> members, List<Category> category, double limitWeight)
        {

            for (int i = 0; i < categories.Length; i++)
            {
                categories[i] = new CategoryViewModel(weights[i], category[i].Weight);
                if (members.Count > 0)
                {
                    categories[i].WeightCategory.CategoryGender = members[0].Member.Gender;
                }

            }

            for (int k = 0; k < members.Count; k++)
            {
                for (int j = 0; j < categories.Length; j++)
                {
                    if (j != categories.Length - 1)
                    {
                        if (members[k].Member.Weight <= categories[0].WeightCategory.CategoryWeight + limitWeight)
                        {
                            categories[0].AddMember(members[k]);
                            break;
                        }
                        else if (members[k].Member.Weight > categories[j].WeightCategory.CategoryWeight + limitWeight && members[k].Member.Weight <= categories[j + 1].WeightCategory.CategoryWeight + limitWeight)
                        {
                            categories[j + 1].AddMember(members[k]);
                        }
                    }
                    else if (members[k].Member.Weight >= categories[j].WeightCategory.CategoryWeight + limitWeight)
                    {
                        categories[j].AddMember(members[k]);
                    }

                }

            }
        }

        public void SortAllMembers(List<Category> categoryGirls, List<Category> categoryBoys)
        {
            SortByHand();
            SortByGender(сompetitionLeftHand);
            SortByGender(сompetitionRightHand);
            SetGroupWeight(сompetitionLeftHand.CategoriesG, сompetitionLeftHand.GirlsWeights, сompetitionLeftHand.MembersGirls, categoryGirls, womanLimit);
            SetGroupWeight(сompetitionRightHand.CategoriesG, сompetitionRightHand.GirlsWeights, сompetitionRightHand.MembersGirls, categoryGirls, womanLimit);
            SetGroupWeight(сompetitionLeftHand.CategoriesB, сompetitionLeftHand.BoysWeights, сompetitionLeftHand.MembersBoys, categoryBoys, manLimit);
            SetGroupWeight(сompetitionRightHand.CategoriesB, сompetitionRightHand.BoysWeights, сompetitionRightHand.MembersBoys, categoryBoys, manLimit);
            SortTeams(сompetitionLeftHand.CategoriesG);
            SortTeams(сompetitionRightHand.CategoriesG);
            SortTeams(сompetitionLeftHand.CategoriesB);
            SortTeams(сompetitionRightHand.CategoriesB);
        }


        #endregion

        private void SortTeams(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                categories[i].Teams = new ObservableCollection<Entities.TeamModel>();
                categories[i].SetTeams();
            }
        }

        public void SetPoints(List<Points> pointsList, string categoryName)
        {
            CompetitionLeftHand.CategoryName = categoryName;
            CompetitionRightHand.CategoryName = categoryName;
            CompetitionLeftHand.SetPoints(pointsList);
            CompetitionLeftHand.SetPoints(pointsList);
        }

        public void SetWeightsLimits(string weightG, string weightB)
        {
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            if (weightG == "")
            {
                womanLimit = 0; 
            }
            else
            {

                womanLimit = double.Parse(weightG, NumberStyles.Any, ci);
            }
            if (weightB == "")
            {
                manLimit = 0;
            }
            else
            {
                manLimit = double.Parse(weightB, NumberStyles.Any, ci);
            }
            
           
        }
    }
}
