using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public string[] points = new string[] { "STUDENTS", "STANDART" };
        public string[] categories = new string[] { "STANDART", "SENIOR", "YOUTH 21", "JUNIOR 18",
            "MASTER", "TOURNAMENT1", "TOURNAMENT2", "TOURNAMENT3" };

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

            for (int i = 0; i < 100; i++)
            {
                MemberViewModel member = new MemberViewModel();
                member.Member.Weight = random.Next(45, 110);
                member.Member.Gender = genders[random.Next(0, 2)];
                member.Hand = hands[random.Next(0, 3)];
                if (member.Member.Gender == "м")
                {
                    member.Member.FullName = namesB[random.Next(0, namesB.Length)] +
                        " " + secondNamesB[random.Next(0, secondNamesB.Length)];
                }
                else
                {
                    member.Member.FullName = namesG[random.Next(0, namesG.Length)] +
                        " " + secondNamesG[random.Next(0, secondNamesG.Length)];
                }
                allMembers.Add(member);
            }
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
                        сompetitionLeftHand.Members.Add(someMember);
                        break;
                    case "Правая":
                        сompetitionRightHand.Members.Add(someMember);
                        break;
                    case "Обе":
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

        private void SetGroupWeight(CategoryViewModel[] categories, int[] weights, List<MemberViewModel> members, List<Category> category)
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
                        if (members[k].Member.Weight <= categories[0].WeightCategory.CategoryWeight)
                        {
                            categories[0].AddMember(members[k]);
                            break;
                        }
                        else if (members[k].Member.Weight > categories[j].WeightCategory.CategoryWeight && members[k].Member.Weight <= categories[j + 1].WeightCategory.CategoryWeight)
                        {
                            categories[j + 1].AddMember(members[k]);
                        }
                    }
                    else if (members[k].Member.Weight >= categories[j].WeightCategory.CategoryWeight)
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
            SetGroupWeight(сompetitionLeftHand.CategoriesG, сompetitionLeftHand.GirlsWeight, сompetitionLeftHand.MembersGirls, categoryGirls);
            SetGroupWeight(сompetitionRightHand.CategoriesG, сompetitionRightHand.GirlsWeight, сompetitionRightHand.MembersGirls, categoryGirls);
            SetGroupWeight(сompetitionLeftHand.CategoriesB, сompetitionLeftHand.BoysWeight, сompetitionLeftHand.MembersBoys, categoryBoys);
            SetGroupWeight(сompetitionRightHand.CategoriesB, сompetitionRightHand.BoysWeight, сompetitionRightHand.MembersBoys, categoryBoys);

        }

        #endregion

        public void GetAllPoints()
        {
            //GetPoints(CompetitionLeftHand);
           //GetPoints(CompetitionRightHand);
        }

        /*private void GetPoints(Сompetition competition)
        {
            int counter = 0;
            for (int i = 0; i < competition.CategoriesB.Length; i++)
            {
                for (int j = 0; j < competition.CategoriesB[i].PlaceMembers.Count; j++)
                {
                    for (int k = 0; k < competition.pointsDatas.Count; k++)
                    {
                        if (competition.CategoriesB[i].PlaceMembers[j].Place == competition.pointsDatas[k].Place)
                        {
                            competition.CategoriesB[i].PlaceMembers[j].Score = competition.pointsDatas[k].Points;
                            counter++;
                        }
                        
                    }
                }

                if (counter != competition.CategoriesB[i].PlaceMembers.Count)
                {
                    for (int k = counter - 1; k < competition.CategoriesB[i].PlaceMembers.Count; k++)
                    {
                        competition.CategoriesB[i].PlaceMembers[k].Score = competition.pointsDatas[competition.pointsDatas.Count - 1].Points;
                    }
                }
            }

            for (int i = 0; i < competition.CategoriesG.Length; i++)
            {
                for (int j = 0; j < competition.CategoriesG[i].PlaceMembers.Count; j++)
                {
                    for (int k = 0; k < competition.pointsDatas.Count; k++)
                    {
                        if (competition.CategoriesG[i].PlaceMembers[j].Place == competition.pointsDatas[k].Place)
                        {
                            competition.CategoriesG[i].PlaceMembers[j].Score = competition.pointsDatas[k].Points;
                        }
                        else
                        {
                            competition.CategoriesG[i].PlaceMembers[j].Score = 1;
                        }
                    }

                }
            }
        }*/

    }
}
