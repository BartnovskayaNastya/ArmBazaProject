using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArmBazaProject.Models;

namespace ArmBazaProject.ViewModels
{
    public delegate void ToureHandler(ToureViewModel toure);

    public class ToureViewModel : ViewModelBase
    {
        public event ToureHandler buttonPressedEvent = null;

        private Toure toure;
        public bool ifExtra = false;
        string name;
        MemberViewModel someMember;

        public IDelegateCommand ToureCommand { protected set; get; }

        public Toure Toure
        {
            get { return toure; }
            set
            {
                if (toure != value)
                {
                    toure = value;
                    OnPropertyChanged("Toure");
                }
            }
        }

        public string Name
        {
            get { return name + " ТУР"; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public ToureViewModel()
        {

            ToureCommand = new DelegateCommand(ExecuteToure);
            toure = new Toure();
        }


        public void ExecuteToure(object param)
        {
           ButtonPressedEvent();
        }

        public void ButtonPressedEvent()
        {
            if (buttonPressedEvent != null)
                buttonPressedEvent.Invoke(this);
        }


        //сортировка 1 тура
        public void SortFirstToure()
        {
            foreach (MemberViewModel member in toure.ToureMembers)
            {
                someMember = new MemberViewModel();
                someMember = (MemberViewModel)member.Clone();
                if (member.IsWiner)
                {
                    someMember.IsWiner = false;
                    toure.ToureMembersA.Add(someMember);

                }
                else
                {
                    someMember.LoseCounter++;
                    toure.ToureMembersB.Add(someMember);
                }
            }
        }

    }
}
