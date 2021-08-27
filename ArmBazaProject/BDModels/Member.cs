using System;
using ArmBazaProject.Models;
using ArmBazaProject.Entities;

namespace ArmBazaProject
{
    [Serializable]
    public class Member : NotifyableObject, ICloneable
    {
        private string fullName;
        public int? QualificationId { get; set; }
        public virtual Qualification Qualification { get; set; }
        private string dateOfBirth;
        private float weight;
        private string gender;


        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int Id { get; set; }
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }


        public string DateOfBirth
        {

            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }


        public float Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public object Clone()
        {
            return new Member()
            {
                Gender = Gender,
                FullName = FullName,
                Weight = Weight,
                DateOfBirth = DateOfBirth,

            };
        }

        public Member()
        {

        }


    }
}
