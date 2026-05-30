using COMPX323_Generator.Entity;
using COMPX323_Generator.Relational;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.MongoEntity
{
    public class MongoIncident
    {
        public class MongoInvolved
        {
            [BsonElement("person_id")]
            public int PersonID;
            [BsonElement("first_name")]
            public string FirstName;

            [BsonElement("role")]
            public string Role;

            [BsonElement("description")]
            public string Description;

            public MongoInvolved(int personID, string firstName, string role, string description)
            {
                PersonID = personID;
                FirstName = firstName;
                Role = role;
                Description = description;
            }
        }

        private DateTime _timestamp;
        private string _type;
        private string _address;
        private string _description;
        private static string[] _addressStreetTypes = { " Road, ", " Street, ", " Avenue, ", " Crescent, ", " Place, ", " Boulevard, " };

        private static int _newestID;

        private List<MongoInvolved> _involved;

        [BsonElement("id")]
        public int Id
        {
            get;
            set;
        }

        [BsonElement("involved")]
        public List<MongoInvolved> Involved
        {
            get { return _involved; }
            set { _involved = value; }
        }


        
        [BsonElement("timestamp")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        [BsonElement("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [BsonElement("address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [BsonElement("description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public MongoIncident()
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

            int numCiv = Form_DataGenerator.GlobalRandom.Next(1,4);
            int numCop = Form_DataGenerator.GlobalRandom.Next(1,6);

            Id = _newestID;
            _newestID++;
            _involved = new List<MongoInvolved>();
            switch (Form_DataGenerator.GlobalRandom.Next(4))
            {
                case 0: 
                    _type = "Robbery";
                    _description = $"There was a robbery at {_address} at about {_timestamp.TimeOfDay} on {_timestamp.ToShortDateString()}. {numCiv} people broke into the building and attempted to steal things but all {numCop} cops showed up early and stopped them.";
                    break;
                case 1: 
                    _type = "Homicide";
                    _description = $"There was a homicide at {_address} at about {_timestamp.TimeOfDay} on {_timestamp.ToShortDateString()}. {numCiv} people all killed each other, it was crazy. {numCop} officers responded.";
                    break;
                case 2: 
                    _type = "Domestic";
                    _description = $"There was a domestic dispute at {_address} at about {_timestamp.TimeOfDay} on {_timestamp.ToShortDateString()}. {numCiv} people would not stop shouting. {numCop} officers subdued them.";
                    break;
                case 3: 
                    _type = "Traffic Stop";
                    _description = $"There was a traffic stop near {_address} at about {_timestamp.TimeOfDay} on {_timestamp.ToShortDateString()}. {numCiv} people are all boy racers. {numCop} officers impounded their vehicles.";
                    break;
            }

            HashSet<int> people = new HashSet<int>();
            for (int i = 0; i < numCiv; i++)
            {
                MongoPerson p = Form_DataGenerator.MongoPersons[Form_DataGenerator.GlobalRandom.Next(0, MongoPerson.FirstEmployeeID)];
                while (people.Contains(p.Id))
                {
                    p = Form_DataGenerator.MongoPersons[Form_DataGenerator.GlobalRandom.Next(0, MongoPerson.FirstEmployeeID)];
                }
                people.Add(p.Id);
                string role = "Witness";
                string desc = "YOU SHOULD NEVER SEE THIS STRING";
                switch (Form_DataGenerator.GlobalRandom.Next(3))
                {
                    case 0:
                        role = "Witness";
                        desc = "This guy saw EVERYTHING. Should really interview him.";
                        break;
                    case 1:
                        role = "Suspect";
                        desc = "This guy may have done the deed.";
                        break;
                    case 2:
                        role = "Victim";
                        desc = "This guy got it rough, what a shame.";
                        break;
                }
                _involved.Add(new MongoInvolved(p.Id, p.FirstName, role, desc));
                p.Involvement.Add(new MongoPerson.MongoPersonInvolvement(Id, _timestamp, _address));
            }
            for (int i = 0; i < numCop; i++)
            {
                MongoPerson p  = Form_DataGenerator.MongoPersons[Form_DataGenerator.GlobalRandom.Next(MongoPerson.FirstEmployeeID, Form_DataGenerator.MongoPersons.Count)];
                while (people.Contains(p.Id))
                {
                    p = Form_DataGenerator.MongoPersons[Form_DataGenerator.GlobalRandom.Next(MongoPerson.FirstEmployeeID, Form_DataGenerator.MongoPersons.Count)];
                }
                people.Add(p.Id);
                _involved.Add(new MongoInvolved(p.Id, p.FirstName, "Officer", "This guy showed up and saved the day, truly heroic"));
                p.Involvement.Add(new MongoPerson.MongoPersonInvolvement(Id, _timestamp, _address));
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
                if (Form_DataGenerator.SQLWriter != null)
                {
                    Form_DataGenerator.SQLWriter.Write(comm);
                    Form_DataGenerator.SQLWriter.WriteLine(";");
                }
                Form_DataGenerator.ExecuteOracleCommand(comm);
            }
            else
            {
                //MongoDB
            }
        }
    }
}
