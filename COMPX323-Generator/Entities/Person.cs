using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Entity
{
    public class Person
    {
        protected string _firstName;
        protected string _lastName;
        protected string _address;
        protected string _phoneNumber;
        protected DateTime _dateOfBirth;

        private static string[] _phonePrefix = { "020", "021", "022", "027", "028" };
        private static string[] _addressStreetTypes = { " Road, ", " Street, ", " Avenue, ", " Crescent, ", " Place, ", " Boulevard, " };
        

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
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public Person()
        {
            _firstName = File.ReadLines(@"Data/fname.txt").Skip(Form_DataGenerator.GlobalRandom.Next(1000)).FirstOrDefault();
            _lastName = File.ReadLines(@"Data/lname.txt").Skip(Form_DataGenerator.GlobalRandom.Next(1000)).FirstOrDefault();
            _phoneNumber = _phonePrefix[Form_DataGenerator.GlobalRandom.Next(_phonePrefix.Length)];
            _phoneNumber += Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0');
            _phoneNumber += Form_DataGenerator.GlobalRandom.Next(10000).ToString().PadLeft(4, '0');
            _address = Form_DataGenerator.GlobalRandom.Next(1, 200).ToString() + " ";
            _address += File.ReadLines(@"Data/lname.txt").Skip(Form_DataGenerator.GlobalRandom.Next(1000)).FirstOrDefault();
            _address += _addressStreetTypes[Form_DataGenerator.GlobalRandom.Next(_addressStreetTypes.Length)];
            _address += File.ReadLines(@"Data/cities.txt").Skip(Form_DataGenerator.GlobalRandom.Next(96)).FirstOrDefault();
            _dateOfBirth = new DateTime(Form_DataGenerator.GlobalRandom.Next(1950, 2009), Form_DataGenerator.GlobalRandom.Next(1, 13), Form_DataGenerator.GlobalRandom.Next(1, 29));
            AddDataToTable();
        }

        public void AddDataToTable()
        {
            Debug.WriteLine("-----");
            Debug.WriteLine($"Name: {_firstName} {_lastName}");
            Debug.WriteLine($"DoB: {_dateOfBirth.ToShortDateString()}");
            Debug.WriteLine($"Address: {_address}");
            Debug.WriteLine($"Phone: {_phoneNumber}");

            // convert the data to an SQL command, add it.
            // ID auto increments, so don't worry about it being unique.
            if (Form_DataGenerator.OracleDB)
            {
                string comm = $@"INSERT INTO A_Persons (first_name, last_name, phone_number, address, date_of_birth) VALUES (
                    '{_firstName}',
                    '{_lastName}',
                    '{_phoneNumber}',
                    '{_address}',
                    DATE '{_dateOfBirth.Year}-{_dateOfBirth.Month}-{_dateOfBirth.Day}'
                );";

                Debug.WriteLine(comm);
                Form_DataGenerator.ExecuteDBCommand(comm);
            }
            else
            {
                // MongoDB
            }
        }

    }
}
