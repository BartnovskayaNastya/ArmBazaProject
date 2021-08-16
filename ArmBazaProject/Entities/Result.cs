using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmBazaProject.Entities
{
    class Result : NotifyableObject
    {
        private ObservableCollection<MemberViewModel> allMembers;
        private ObservableCollection<MemberViewModel> placeMembersLeft;
        private ObservableCollection<MemberViewModel> placeMembersRight;

        private ObservableCollection<MemberViewModel> placeMembersByTwoHands;


        public ObservableCollection<MemberViewModel> PlaceMembersByTwoHands
        {
            get { return placeMembersByTwoHands; }
            set
            {
                if (placeMembersByTwoHands != value)
                {
                    placeMembersByTwoHands = value;
                    OnPropertyChanged("PlaceMembersByTwoHands");
                }
            }
        }

        public ObservableCollection<MemberViewModel> PlaceMembersRight
        {
            get { return placeMembersRight; }
            set
            {
                if (placeMembersRight != value)
                {
                    placeMembersRight = value;
                    OnPropertyChanged("PlaceMembersRight");
                }
            }
        }

        public ObservableCollection<MemberViewModel> PlaceMembersLeft
        {
            get { return placeMembersLeft; }
            set
            {
                if (placeMembersLeft != value)
                {
                    placeMembersLeft = value;
                    OnPropertyChanged("PlaceMembersLeft");
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

    }
}
