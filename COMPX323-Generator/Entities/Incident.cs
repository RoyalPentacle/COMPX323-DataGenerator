using COMPX323_Generator.Relational;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Entity
{
    public class Incident
    {
        private DateTime _timestamp;
        private string _type;
        private string _address;
        private string _description;
        private static string[] _addressStreetTypes = { " Road, ", " Street, ", " Avenue, ", " Crescent, ", " Place, ", " Boulevard, " };

        private static int _newestID;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Incident()
        {
            // randomize timestamp, address
            // description template will list number of officers, etc, involved
            // query specific rows from those tables to get specific people.
            // IMPORTANT, RANDOM AN INTEGER, AND GET THAT ROW. We need to make sure that it respects the seed provided, not whatever the db decides.
            // construct description from template
            // construct sql command for adding incident to table
            // construct involved objects for each person involved
            // randomize the involved components, but ensure it matches with the incident.
            // construct sql commands for those involved, add them to involved table.

            _timestamp = new DateTime(Form_DataGenerator.GlobalRandom.Next(1970, DateTime.Now.Year), Form_DataGenerator.GlobalRandom.Next(1, 13), Form_DataGenerator.GlobalRandom.Next(1, 29), Form_DataGenerator.GlobalRandom.Next(24), 0, 0);
            
            _address = Form_DataGenerator.GlobalRandom.Next(1, 200).ToString() + " ";
            _address += File.ReadLines(@"Data/lname.txt").Skip(Form_DataGenerator.GlobalRandom.Next(1000)).FirstOrDefault();
            _address += _addressStreetTypes[Form_DataGenerator.GlobalRandom.Next(_addressStreetTypes.Length)];
            _address += File.ReadLines(@"Data/cities.txt").Skip(Form_DataGenerator.GlobalRandom.Next(96)).FirstOrDefault();

            int numCiv = Form_DataGenerator.GlobalRandom.Next(3);
            int numCop = Form_DataGenerator.GlobalRandom.Next(5);

            switch (Form_DataGenerator.GlobalRandom.Next(4))
            {
                case 0: 
                    _type = "Robbery";
                    _description = $"There was a robbery at {_address} at about {_timestamp.TimeOfDay} on {_timestamp.Date}. {numCiv} people broke into the building and attempted to steal things but all {numCop} cops showed up early and stopped them.";
                    break;
                case 1: 
                    _type = "Homicide";
                    _description = $"There was a homicide at {_address} at about {_timestamp.TimeOfDay} on {_timestamp.Date}. {numCiv} people all killed each other, it was crazy. {numCop} officers responded.";
                    break;
                case 2: 
                    _type = "Domestic";
                    _description = $"There was a domestic dispute at {_address} at about {_timestamp.TimeOfDay} on {_timestamp.Date}. {numCiv} people would not stop shouting. {numCop} officers subdued them.";
                    break;
                case 3: 
                    _type = "Traffic Stop";
                    _description = $"There was a traffic stop near {_address} at about {_timestamp.TimeOfDay} on {_timestamp.Date}. {numCiv} people are all boy racers. {numCop} officers impounded their vehicles.";
                    break;
            }

            Debug.WriteLine("-----Incident");
            Debug.WriteLine($"Timestamp: {_timestamp.ToString("g")}");
            Debug.WriteLine($"Address: {_address}");
            Debug.WriteLine($"Type: {_type}");
            Debug.WriteLine($"Description: {_description}");

            AddDataToTable();
            _newestID++;
            HashSet<int> people = new HashSet<int>();
            for (int i = 0; i < numCiv; i++)
            {
                int newID = Form_DataGenerator.GlobalRandom.Next(1, Person.FirstEmployeeID);
                while (people.Contains(newID))
                {
                    newID = Form_DataGenerator.GlobalRandom.Next(1, Person.FirstEmployeeID);
                }
                people.Add(newID);
                Involved inc = new Involved(newID, _newestID, false);
            }
            for (int i = 0; i < numCop; i++)
            {
                int newID = Form_DataGenerator.GlobalRandom.Next(Person.FirstEmployeeID, Person._newestID + 1);
                while (people.Contains(newID))
                {
                    newID = Form_DataGenerator.GlobalRandom.Next(Person.FirstEmployeeID, Person._newestID + 1);
                }
                people.Add(newID);
                Involved inc = new Involved(newID, _newestID, true);
            }
        }

        public virtual void AddDataToTable()
        {
            // convert the data to a SQL command, add it.
            // ID auto increments, so don't worry about it being unique.
            if (Form_DataGenerator.OracleDB)
            {
                string comm = $@"INSERT INTO A_Incidents (timestamp, type, address, description) VALUES (
                TO_DATE('{_timestamp.Year}-{_timestamp.Month}-{_timestamp.Day} {_timestamp.Hour}:00:00', 'YYYY-MM-DD HH24:MI:SS'),
                '{_type}',
                '{_address}',
                '{_description}'
                )";
                Debug.WriteLine(comm);
                Form_DataGenerator.ExecuteDBCommand(comm);
            }
            else
            {
                //MongoDB
            }
        }
    }
}
