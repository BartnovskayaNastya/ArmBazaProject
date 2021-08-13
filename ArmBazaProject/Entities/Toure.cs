using System.Collections.ObjectModel;
using ArmBazaProject.Entities;

namespace ArmBazaProject.Models
{
    

    public class Toure : NotifyableObject
    {
        private ObservableCollection<MemberViewModel> tourMembers;
        private ObservableCollection<MemberViewModel> tourMembersA;
        private ObservableCollection<MemberViewModel> tourMembersB;


        #region свойства доступа


        public ObservableCollection<MemberViewModel> ToureMembers
        {
            get { return tourMembers; }
            set
            {
                if (tourMembers != value)
                {
                    tourMembers = value;
                    OnPropertyChanged("ToureMembers");
                }
            }
        }

        public ObservableCollection<MemberViewModel> ToureMembersA
        {
            get { return tourMembersA; }
            set
            {
                if (tourMembersA != value)
                {
                    tourMembersA = value;
                    OnPropertyChanged("ToureMembersA");
                }
            }
        }

        public ObservableCollection<MemberViewModel> ToureMembersB
        {
            get { return tourMembersB; }
            set
            {
                if (tourMembersB != value)
                {
                    tourMembersB = value;
                    OnPropertyChanged("ToureMembersB");
                }
            }
        }

        #endregion

        public Toure()
        {
            tourMembersA = new ObservableCollection<MemberViewModel>();
            tourMembersB = new ObservableCollection<MemberViewModel>();
            tourMembers = new ObservableCollection<MemberViewModel>();
        }

        public bool CheckExtraA()
        {
           return CheckExtra(ToureMembersA);
        }

        public bool CheckExtraB()
        {
            return CheckExtra(ToureMembersB);
        }


        private bool CheckExtra(ObservableCollection<MemberViewModel> members)
        {
            bool extra = false;
            if (members.Count % 2 > 0 && members.Count > 1)
            {
                extra = true;
            }

            return extra;
        }


    }
}
