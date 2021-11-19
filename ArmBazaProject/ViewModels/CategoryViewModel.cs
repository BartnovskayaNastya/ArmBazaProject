using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArmBazaProject.Entities;
using ArmBazaProject.Models;
using ArmBazaProject.ViewModels;

namespace ArmBazaProject
{

    public delegate void FirstMembersHandler(ObservableCollection<MemberViewModel> members);

    public class CategoryViewModel : ViewModelBase
    {
        #region объявление
        readonly Random random = new Random();
        int index = 0;
        int indexA;
        int indexB;
        bool isAall = false;
        bool isBall = false;
        bool isDone = false;
        bool isDonisimo = false;


        private WeightCategory weightCategory;

        private ObservableCollection<ToureViewModel> toures;
        ToureViewModel toure;
        ToureViewModel toureToAdd;
        ToureViewModel reserveToureToAdd;


        MemberViewModel someMemberFirst;
        MemberViewModel someMemberSecond;
        MemberViewModel someMember;

        private ObservableCollection<MemberViewModel> resultMembers;

        private ObservableCollection<MemberViewModel> allMembers;
        private ObservableCollection<MemberViewModel> placeMembers;
        private ObservableCollection<MemberViewModel> firstTourMembers;
        private ObservableCollection<MemberViewModel> semifinal;
        private ToureViewModel final;



        private ObservableCollection<TeamModel> teams;

        private ObservableCollection<TeamModel> resultTeams;


        #endregion


        #region свойства доступа

        public IDelegateCommand StartToureCommand { protected set; get; }
        public IDelegateCommand FirstToureCommand { protected set; get; }

        public IDelegateCommand DrawCommand { protected set; get; }

        public IDelegateCommand FinalCommandA { protected set; get; }
        public IDelegateCommand FinalCommandB { protected set; get; }
        public IDelegateCommand SemiFinalCommand { protected set; get; }


        public WeightCategory WeightCategory
        {
            get { return weightCategory; }
            set
            {
                if (weightCategory != value)
                {
                    weightCategory = value;
                    OnPropertyChanged("WeightCategory");
                }
            }
        }

        public ObservableCollection<TeamModel> ResultTeams
        {
            get { return resultTeams; }
            set
            {
                if (resultTeams != value)
                {
                    resultTeams = value;
                    OnPropertyChanged("ResultTeams");
                }
            }
        }

        public ObservableCollection<TeamModel> Teams
        {
            get { return teams; }
            set
            {
                if (teams != value)
                {
                    teams = value;
                    OnPropertyChanged("Teams");
                }
            }
        }

        public ObservableCollection<ToureViewModel> Toures
        {
            get { return toures; }
            set
            {
                if (toures != value)
                {
                    toures = value;
                    OnPropertyChanged("Toures");
                }
            }
        }

        public ToureViewModel Final
        {
            get { return final; }
            set
            {
                if (final != value)
                {
                    final = value;
                    OnPropertyChanged("Final");
                }
            }
        }

        public ObservableCollection<MemberViewModel> SemiFinal
        {
            get { return semifinal; }
            set
            {
                if (semifinal != value)
                {
                    semifinal = value;
                    OnPropertyChanged("SemiFinal");
                }
            }
        }
        public ObservableCollection<MemberViewModel> PlaceMembers
        {
            get { return placeMembers; }
            set
            {
                if (placeMembers != value)
                {
                    placeMembers = value;
                    OnPropertyChanged("PlaceMembers");
                }
            }
        }

        public ObservableCollection<MemberViewModel> ResultMembers
        {
            get { return resultMembers; }
            set
            {
                if (resultMembers != value)
                {
                    resultMembers = value;
                    OnPropertyChanged("ResultMembers");
                }
            }
        }


        public ObservableCollection<MemberViewModel> FirstToureMembers
        {
            get { return firstTourMembers; }
            set
            {
                if (firstTourMembers != value)
                {
                    firstTourMembers = value;
                    OnPropertyChanged("FirstToureMembers");
                }
            }
        }

        public ObservableCollection<MemberViewModel> AllMembers
        {
            get { return allMembers; }
            set
            {
                if (allMembers != value)
                {
                    allMembers = value;
                    OnPropertyChanged("AllMembers");
                }
            }
        }

        #endregion

        public CategoryViewModel(float weight, string name)
        {
            weightCategory = new WeightCategory(weight, name);

            allMembers = new ObservableCollection<MemberViewModel>();
            placeMembers = new ObservableCollection<MemberViewModel>();
            resultMembers = new ObservableCollection<MemberViewModel>();

            toures = new ObservableCollection<ToureViewModel>();
            firstTourMembers = new ObservableCollection<MemberViewModel>();
            final = new ToureViewModel();
            semifinal = new ObservableCollection<MemberViewModel>();

            teams = new ObservableCollection<TeamModel>();
            resultTeams = new ObservableCollection<TeamModel>();

            StartToureCommand = new DelegateCommand(StartCommand);
            FirstToureCommand = new DelegateCommand(FirstCommand);
            DrawCommand = new DelegateCommand(RandomDraw);
            FinalCommandA = new DelegateCommand(FinalButtonA);
            FinalCommandB = new DelegateCommand(FinalButtonB);
            SemiFinalCommand = new DelegateCommand(SemiFinalButton);

            toure = new ToureViewModel();
            toure.buttonPressedEvent += ButtonClickToure;
            
        }


        public void SetTeams()
        {
            TeamModel team;
            for (int i = 0; i < allMembers.Count; i++)
            {
                team = new TeamModel(allMembers[i].TeamName);
                if (!CheckTeam(team))
                {
                    teams.Add(team);
                }
            }
        }

        public void RandomDraw(object param)
        {
            MemberViewModel tmp;
            int j;
            for (int i = allMembers.Count - 1; i >= 1; i--)
            {
                j = random.Next(i + 1);
                tmp = allMembers[j];
                allMembers[j] = allMembers[i];
                allMembers[i] = tmp;
            }
        }

        public bool CheckTeam(TeamModel teamToAdd)
        {
            bool isExist = false;
            if(teams.Count > 0)
            {
                foreach (TeamModel team in teams)
                {
                    if (team.Name == teamToAdd.Name)
                    {
                        isExist = true;
                        break;
                    }
                }
            }
            if (!isExist)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public void AddMember(MemberViewModel somemember)
        {
            bool isExist = false;
            foreach (MemberViewModel member in allMembers)
            {
                if (somemember.Member.FullName == member.Member.FullName)
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                allMembers.Add(somemember);
            }

        }

        private void StartCommand(object param)
        {
            if (allMembers.Count == 1)
            {
                allMembers[0].Place = 1;
                placeMembers.Add((MemberViewModel)allMembers[0].Clone());
            }
            else
            {
                int size;
                bool isU = false;
                foreach (MemberViewModel member in allMembers)
                {
                    firstTourMembers.Add((MemberViewModel)member.Clone());
                }
                //если нечетное кол-во участников
                if (firstTourMembers.Count % 2 > 0)
                {
                    size = firstTourMembers.Count - 1;
                    isU = true;
                }
                else
                {
                    size = firstTourMembers.Count;
                }
                for (int i = 0; i < size; i++)
                {
                    toure.Toure.ToureMembers.Add((MemberViewModel)firstTourMembers[i].Clone());
                }
                if (isU)
                {
                    toure.Toure.ToureMembersA.Add((MemberViewModel)firstTourMembers[firstTourMembers.Count - 1].Clone());
                    firstTourMembers.RemoveAt(firstTourMembers.Count - 1);
                }
                toures.Add(toure);
                toure.Name = (toures.Count + 1).ToString();
            }

        }
        private void FirstCommand(object param)
        {
            someMember = new MemberViewModel();
            toure.Toure.ToureMembers.Clear();
            foreach (MemberViewModel member in firstTourMembers)
            {
                toure.Toure.ToureMembers.Add((MemberViewModel)member.Clone());
            }
            //3 участника
            if (firstTourMembers.Count == 2 && toures[0].Toure.ToureMembersA.Count == 1)
            {
                getThreeMembers();
            }
            // 2 участника
            else if (firstTourMembers.Count == 2)
            {
                someMember = (MemberViewModel)toure.Toure.ToureMembers[0].Clone();
                if (someMember.IsWiner)
                {
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)toure.Toure.ToureMembers[1].Clone();
                    someMember.LoseCounter++;
                    final.Toure.ToureMembersA.Add(someMember);
                }
                else
                {
                    someMember = (MemberViewModel)toure.Toure.ToureMembers[1].Clone();
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)toure.Toure.ToureMembers[0].Clone();
                    someMember.LoseCounter++;
                    final.Toure.ToureMembersA.Add(someMember);
                }
            }
            //3+ участника
            else
            {
                toure.SortFirstToure();
                if (toures[toures.Count - 1].Toure.CheckExtraA() || toures[toures.Count - 1].Toure.CheckExtraB())
                {
                    reserveToureToAdd = new ToureViewModel();
                    reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                    ExtraToure(toures[toures.Count - 1], reserveToureToAdd);
                    toures[toures.Count - 1].ifExtra = true;
                    index = toures.Count - 1;
                    toures.Add(reserveToureToAdd);
                    reserveToureToAdd.Name = toures.Count.ToString();
                }
            }


        }

        private void FinalButtonA(object param)
        {
            someMemberFirst = (MemberViewModel)final.Toure.ToureMembersA[0].Clone();
            someMemberSecond = (MemberViewModel)final.Toure.ToureMembersA[1].Clone();
            if(firstTourMembers.Count == 2)
            {
                if (someMemberFirst.IsWiner)
                {
                    someMemberSecond.LoseCounter++;
                    if(someMemberSecond.LoseCounter == 2)
                    {
                        someMemberSecond.Place = 2;
                        placeMembers.Add(someMemberSecond);
                        someMemberFirst.Place = 1;
                        placeMembers.Add(someMemberFirst);
                        
                    }
                    else
                    {
                        someMemberFirst.IsWiner = false;
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberFirst.Clone());
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberSecond.Clone());
                    }
                }
                else
                {
                    someMemberFirst.LoseCounter++;
                    if (someMemberFirst.LoseCounter == 2)
                    {
                        someMemberFirst.Place = 2;
                        placeMembers.Add(someMemberFirst);
                        someMemberSecond.Place = 1;
                        placeMembers.Add(someMemberSecond);

                    }
                    else
                    {
                        someMemberSecond.IsWiner = false;
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberSecond.Clone());
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberFirst.Clone());
                    }
                }
            }
            else
            {
                if (final.Toure.ToureMembersA[0].IsWiner)
                {
                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                }
                else
                {
                    someMemberFirst.IsWiner = false;
                    final.Toure.ToureMembersB.Add(someMemberFirst);
                    someMemberSecond.IsWiner = false;
                    final.Toure.ToureMembersB.Add(someMemberSecond);
                }
            }
            
        }
        private void FinalButtonB(object param)
        {
            someMemberFirst = (MemberViewModel)final.Toure.ToureMembersB[0].Clone();
            someMemberSecond = (MemberViewModel)final.Toure.ToureMembersB[1].Clone();
            if (allMembers.Count == 2)
            {
                if (someMemberFirst.IsWiner)
                {
                    someMemberSecond.Place = 2;
                    placeMembers.Add(someMemberSecond);
                    someMemberFirst.Place = 1;
                    placeMembers.Add(someMemberFirst);
                }
                else
                {
                    someMemberFirst.Place = 2;
                    placeMembers.Add(someMemberFirst);
                    someMemberSecond.Place = 1;
                    placeMembers.Add(someMemberSecond);
                }
            }
            else
            {
                if (final.Toure.ToureMembersB[0].IsWiner)
                {
                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                }
                else
                {
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);
                }
            }
            
        }
        private void SemiFinalButton(object param)
        {
            someMember = new MemberViewModel();
            if (semifinal[0].IsWiner)
            {
                someMember = (MemberViewModel)semifinal[0].Clone();
                someMember.IsWiner = false;
                final.Toure.ToureMembersA.Add(someMember);
                someMember = (MemberViewModel)semifinal[1].Clone();
                someMember.Place = allMembers.Count - placeMembers.Count;
                placeMembers.Add(someMember);
            }
            else
            {
                someMember = (MemberViewModel)semifinal[1].Clone();
                someMember.IsWiner = false;
                final.Toure.ToureMembersA.Add(someMember);
                someMember = (MemberViewModel)semifinal[0].Clone();
                someMember.Place = allMembers.Count - placeMembers.Count;
                placeMembers.Add(someMember);
            }
           
        }

        private void ButtonClickToure(ToureViewModel toure)
        {
            int ind = 0;
            
            //3 участника
            if (toures[0].Toure.ToureMembersA.Count == 2 && toures[0].Toure.ToureMembersB.Count == 0)
            {
                if (toures[0].Toure.ToureMembersA[0].IsWiner)
                {
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();
                    someMemberFirst.IsWiner  = false;
                    someMemberSecond.IsWiner = false;

                    final.Toure.ToureMembersA.Add(someMemberFirst);
                    semifinal.Add(someMemberSecond);
                }
                else if (toures[0].Toure.ToureMembersA[1].IsWiner)
                {
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();
                    someMemberFirst.IsWiner = false;
                    someMemberSecond.IsWiner = false;

                    final.Toure.ToureMembersA.Add(someMemberSecond);
                    semifinal.Add(someMemberFirst);
                }
            }
            //4 участника
            else if (toures[0].Toure.ToureMembersA.Count == 2 && toures[0].Toure.ToureMembersB.Count == 2 && allMembers.Count == 4)
            {
                someMemberFirst = new MemberViewModel();
                someMemberSecond = new MemberViewModel();
                someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();


                if (someMemberFirst.IsWiner)
                {
                    someMemberFirst.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMemberFirst);
                    semifinal.Add(someMemberSecond);


                }
                else if (someMemberSecond.IsWiner)
                {
                    someMemberSecond.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMemberSecond);
                    semifinal.Add(someMemberFirst);
                }

                someMemberFirst = new MemberViewModel();
                someMemberSecond = new MemberViewModel();
                someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersB[0].Clone();
                someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersB[1].Clone();

                if (someMemberFirst.IsWiner)
                {
                    someMemberFirst.IsWiner = false;
                    semifinal.Add(someMemberFirst);
                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);


                }
                else if (someMemberSecond.IsWiner)
                {
                    someMemberSecond.IsWiner = false;
                    semifinal.Add(someMemberSecond);
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                }

            }
            else if (allMembers.Count == 5)
            {
                
                if(toures[0].Toure.ToureMembersA.Count == 2 && toures[0].Toure.ToureMembersB.Count == 2 && !isDone)
                {
                    isDone = true;
                    toureToAdd = toures[toures.Count - 1];
                    toureToAdd.buttonPressedEvent += ButtonClickToure;

                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();


                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        toureToAdd.Toure.ToureMembersA.Add(someMemberFirst);
                        toureToAdd.Toure.ToureMembersB.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        toureToAdd.Toure.ToureMembersA.Add(someMemberSecond);
                        toureToAdd.Toure.ToureMembersB.Add(someMemberFirst);
                    }

                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersB[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersB[1].Clone();

                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        toureToAdd.Toure.ToureMembersB.Add(someMemberFirst);
                        someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        toureToAdd.Toure.ToureMembersB.Add(someMemberSecond);
                        someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberFirst);
                    }
                    int name = toures.Count;
                    toureToAdd.Name = name.ToString();
                }
                else if (!isDonisimo)
                {
                    isDonisimo = true;
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[1].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[1].Toure.ToureMembersA[1].Clone();


                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberFirst);
                        semifinal.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberSecond);
                        semifinal.Add(someMemberFirst);
                    }

                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[1].Toure.ToureMembersB[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[1].Toure.ToureMembersB[1].Clone();

                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        semifinal.Add(someMemberFirst);
                        someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        semifinal.Add(someMemberSecond);
                        someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberFirst);
                    }
                }

            }   
            
            else
            {
                if (toures[0].Toure.ToureMembersA.Count == 2 && toures.Count == 1 && toures[0].Toure.ToureMembersB.Count > 2)
                {
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();
                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberFirst);
                        semifinal.Add(someMemberSecond);

                    }
                    else
                    {
                        someMemberSecond.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberSecond);
                        semifinal.Add(someMemberFirst);
                    }
                }
                if (toures.Count != 1)
                {

                    if (((toures[toures.Count - 2].Toure.ToureMembersA.Count == 2 && toures[toures.Count - 1].Toure.ToureMembersA.Count == 0) || (toures[toures.Count - 1].Toure.ToureMembersA.Count == 2)) && (!isAall))
                    {
                        if (toures[toures.Count - 1].Toure.ToureMembersA.Count == 0)
                        {
                            ind = toures.Count - 2;
                        }
                        else if (toures[toures.Count - 1].Toure.ToureMembersA.Count == 2)
                        {
                            ind = toures.Count - 1;
                        }
                        isAall = true;
                        someMemberFirst = new MemberViewModel();
                        someMemberSecond = new MemberViewModel();
                        someMemberFirst = (MemberViewModel)toures[ind].Toure.ToureMembersA[0].Clone();
                        someMemberSecond = (MemberViewModel)toures[ind].Toure.ToureMembersA[1].Clone();
                        if (someMemberFirst.IsWiner)
                        {
                            someMemberFirst.IsWiner = false;
                            final.Toure.ToureMembersA.Add(someMemberFirst);
                            semifinal.Add(someMemberSecond);

                        }
                        else
                        {
                            someMemberSecond.IsWiner = false;
                            final.Toure.ToureMembersA.Add(someMemberSecond);
                            semifinal.Add(someMemberFirst);
                        }

                    }

                    if ((toures[toures.Count - 2].Toure.ToureMembersB.Count == 2 && toures[toures.Count - 1].Toure.ToureMembersB.Count == 0) || (toures[toures.Count - 1].Toure.ToureMembersB.Count == 2))
                    {
                        if (toures[toures.Count - 1].Toure.ToureMembersB.Count == 0)
                        {
                            ind = toures.Count - 2;
                        }
                        else if (toures[toures.Count - 1].Toure.ToureMembersB.Count == 2)
                        {
                            ind = toures.Count - 1;
                        }
                        isBall = true;
                        someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[0].Clone();
                        if (someMember.IsWiner)
                        {
                            someMember.IsWiner = false;
                            semifinal.Add(someMember);
                            someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[1].Clone();
                            someMember.Place = allMembers.Count - placeMembers.Count;
                            placeMembers.Add(someMember);
                        }
                        else
                        {
                            someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[1].Clone();
                            someMember.IsWiner = false;
                            semifinal.Add(someMember);
                            someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[0].Clone();
                            someMember.Place = allMembers.Count - placeMembers.Count;
                            placeMembers.Add((MemberViewModel)someMember.Clone());

                        }

                    }

                    else if (toures[index].ifExtra)
                    {
                        toures[index].ifExtra = false;
                        SortToures(toures[toures.Count - 1], toures[index], isAall, isBall);
                        if (toures[toures.Count - 1].Toure.CheckExtraA() || toures[toures.Count - 1].Toure.CheckExtraB())
                        {
                            reserveToureToAdd = new ToureViewModel();
                            reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                            ExtraToure(toures[toures.Count - 1], reserveToureToAdd);
                            toures[toures.Count - 1].ifExtra = true;
                            index = toures.Count - 1;
                            toures.Add(reserveToureToAdd);
                            reserveToureToAdd.Name = toures.Count.ToString();
                        }
                    }
                    else
                    {
                        toureToAdd = new ToureViewModel();
                        toureToAdd.buttonPressedEvent += ButtonClickToure;
                        SortToures(toureToAdd, toure, isAall, isBall);
                        if (toureToAdd.Toure.CheckExtraA() || toureToAdd.Toure.CheckExtraB())
                        {
                            reserveToureToAdd = new ToureViewModel();
                            reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                            ExtraToure(toureToAdd, reserveToureToAdd);
                            toureToAdd.ifExtra = true;
                            toures.Add(toureToAdd);
                            index = toures.Count - 1;
                            toureToAdd.Name = toures.Count.ToString();
                            toures.Add(reserveToureToAdd);
                            reserveToureToAdd.Name = toures.Count.ToString();
                        }
                        else
                        {
                            toures.Add(toureToAdd);
                            toureToAdd.Name = toures.Count.ToString();
                        }

                    }

                }
                else if (toures[index].ifExtra)
                {
                    toures[index].ifExtra = false;
                    SortToures(toures[toures.Count - 1], toures[index], isAall, isBall);
                    if (toures[toures.Count - 1].Toure.CheckExtraA() || toures[toures.Count - 1].Toure.CheckExtraB())
                    {
                        reserveToureToAdd = new ToureViewModel();
                        reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                        ExtraToure(toures[toures.Count - 1], reserveToureToAdd);
                        toures[toures.Count - 1].ifExtra = true;
                        index = toures.Count - 1;
                        toures.Add(reserveToureToAdd);
                        reserveToureToAdd.Name = toures.Count.ToString();
                    }
                }
                else
                {
                    toureToAdd = new ToureViewModel();
                    toureToAdd.buttonPressedEvent += ButtonClickToure;
                    SortToures(toureToAdd, toure, isAall, isBall);
                    if (toureToAdd.Toure.CheckExtraA() || toureToAdd.Toure.CheckExtraB())
                    {
                        reserveToureToAdd = new ToureViewModel();
                        reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                        ExtraToure(toureToAdd, reserveToureToAdd);
                        toureToAdd.ifExtra = true;
                        toures.Add(toureToAdd);
                        index = toures.Count - 1;
                        toureToAdd.Name = toures.Count.ToString();
                        toures.Add(reserveToureToAdd);
                        reserveToureToAdd.Name = toures.Count.ToString();
                    }
                    else
                    {
                        toures.Add(toureToAdd);
                        toureToAdd.Name = toures.Count.ToString();
                    }

                }

            }

        }


        private void getThreeMembers()
        {
            someMemberFirst = new MemberViewModel();
            someMemberSecond = new MemberViewModel();
            someMemberFirst = (MemberViewModel)firstTourMembers[0].Clone();
            someMemberSecond = (MemberViewModel)firstTourMembers[1].Clone();
            if (someMemberFirst.IsWiner)
            {
                someMemberFirst.IsWiner = false;
                toures[0].Toure.ToureMembersA.Add(someMemberFirst);
                someMemberSecond.LoseCounter++;
                semifinal.Add(someMemberSecond);

            }
            else
            {
                someMemberSecond.IsWiner = false;
                toures[0].Toure.ToureMembersA.Add(someMemberSecond);
                someMemberFirst.LoseCounter++;
                semifinal.Add(someMemberFirst);

            }
        }
        //сортировка A тура
        private void SortAToure(ToureViewModel toureToAdd, ToureViewModel toure, bool isAall)
        {
            foreach (MemberViewModel member in toure.Toure.ToureMembersA)
            {
                someMember = new MemberViewModel();
                someMember = (MemberViewModel)member.Clone();
                if (someMember.IsWiner && !isAall)
                {
                    someMember.IsWiner = false;
                    toureToAdd.Toure.ToureMembersA.Add(someMember);
                }
                else if(!someMember.IsWiner && !isAall)
                {
                    someMember.LoseCounter++;
                    toureToAdd.Toure.ToureMembersB.Add(someMember);
                }
            }
        }

        //сортировка B тура
        private void SortBToure(ToureViewModel toureToAdd, ToureViewModel toure, bool isBall)
        {
            foreach (MemberViewModel member in toure.Toure.ToureMembersB)
            {
                //someMember = new MemberViewModel();
                someMember = (MemberViewModel)member.Clone();
                if (someMember.IsWiner && !isBall)
                {
                    someMember.IsWiner = false;
                    toureToAdd.Toure.ToureMembersB.Add(someMember);
                }
                else
                {
                    someMember.LoseCounter++;
                    someMember.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMember);
                }
            }
        }

        private void SortToures(ToureViewModel toureToAdd, ToureViewModel toure, bool isAall, bool isBall)
        {
            SortAToure(toureToAdd, toure, isAall);
            SortBToure(toureToAdd, toure, isBall);
        }


        private void ExtraToure(ToureViewModel toureToAdd, ToureViewModel extraToure)
        {
            indexA = toureToAdd.Toure.ToureMembersA.Count - 1;
            indexB = toureToAdd.Toure.ToureMembersB.Count - 1;

            if (toureToAdd.Toure.CheckExtraA())
            {
                extraToure.Toure.ToureMembersA.Add((MemberViewModel)toureToAdd.Toure.ToureMembersA[indexA].Clone());
                toureToAdd.Toure.ToureMembersA.RemoveAt(indexA);
            }
            if (toureToAdd.Toure.CheckExtraB())
            {
                extraToure.Toure.ToureMembersB.Add((MemberViewModel)toureToAdd.Toure.ToureMembersB[indexB].Clone());
                toureToAdd.Toure.ToureMembersB.RemoveAt(indexB);
            }
        }

    }
}