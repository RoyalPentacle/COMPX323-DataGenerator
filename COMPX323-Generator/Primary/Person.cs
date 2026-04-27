using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Primary
{
    public class Person
    {
        protected string _firstName;
        protected string _lastName;
        protected string _address;
        protected string _phoneNumber;
        protected DateTime date_of_birth;


        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public DateTime DateOfBirth
        {
            get { return date_of_birth; }
            set { date_of_birth = value; }
        }

        public Person()
        {
            // randomly pick the whole thing.
        }

        public virtual void AddDataToTable()
        {
            // convert the data to a SQL command, add it.
            // ID auto increments, so don't worry about it being unique.
        }

    }
}
