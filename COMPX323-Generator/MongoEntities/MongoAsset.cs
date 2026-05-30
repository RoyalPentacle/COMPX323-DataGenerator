using COMPX323_Generator.Relational;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace COMPX323_Generator.MongoEntity
{
    [BsonKnownTypes(typeof(MongoVehicle), typeof(MongoFirearm))]
    public class MongoAsset
    {
        public class MongoIssuance
        {
            [BsonElement("person_id")]
            public int PersonId;
            
            [BsonElement("first_name")]
            public string FirstName;

            [BsonElement("date_issued")]
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime IssueDate;

            [BsonIgnoreIfNull]
            [BsonElement("date_returned")]
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime? ReturnDate;

            public MongoIssuance(int personID, string firstName, DateTime issueDate,  DateTime? returnDate)
            {
                PersonId = personID;
                FirstName = firstName;
                IssueDate = issueDate;
                ReturnDate = returnDate;
            }
        }

        protected string _type;
        protected string _model;

        protected List<MongoIssuance> _issuances;

        [BsonIgnore]
        public static int _newestID = 0;

        [BsonIgnore]
        public static int VehicleIDStart;

        [BsonIgnore]
        public static int FirearmIDStart;

        [BsonElement("id")]
        public int Id
        {
            get; set;
        }

        [BsonElement("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [BsonElement("model")]
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        
        [BsonIgnoreIfNull]
        [BsonElement("issuances")]
        public List<MongoIssuance> Issuances
        {
            get { return _issuances; }
            set { _issuances = value; }
        }

        public MongoAsset(string type)
        {
            Id = _newestID;
            _newestID++;
            _type = type;
            if (type == "EQUIPMENT")
                _model = File.ReadLines(@"Data/equipment.txt").Skip(Form_DataGenerator.GlobalRandom.Next(83)).FirstOrDefault();
            else if (type == "VEHICLE")
                _model = File.ReadLines(@"Data/vehicles.txt").Skip(Form_DataGenerator.GlobalRandom.Next(97)).FirstOrDefault();
            else if (type == "FIREARM")
                _model = File.ReadLines(@"Data/firearms.txt").Skip(Form_DataGenerator.GlobalRandom.Next(94)).FirstOrDefault();
            _model = _model.Replace("\'", "\'\'");
            _issuances = new List<MongoIssuance>();
            int year = Form_DataGenerator.GlobalRandom.Next(1970, 1973);
            int month = Form_DataGenerator.GlobalRandom.Next(1, 13);
            int day = Form_DataGenerator.GlobalRandom.Next(1, 29);
            DateTime dateIssued = new DateTime(year, month, day);
            DateTime? dateReturned = null;
            while(Form_DataGenerator.GlobalRandom.Next(100) > 25)
            {
                year = Form_DataGenerator.GlobalRandom.Next(dateIssued.Year, dateIssued.Year + 3);
                if (year == dateIssued.Year)
                {
                    month = Form_DataGenerator.GlobalRandom.Next(dateIssued.Month, 13);
                    if (month == dateIssued.Month)
                    {
                        day = Form_DataGenerator.GlobalRandom.Next(dateIssued.Day, 29);
                    }
                }
                else
                {
                    month = Form_DataGenerator.GlobalRandom.Next(1, 13);
                    day = Form_DataGenerator.GlobalRandom.Next(1, 29);
                }
                dateReturned = new DateTime(year, month, day);
                MongoPerson person = Form_DataGenerator.MongoPersons[Form_DataGenerator.GlobalRandom.Next(MongoPerson.FirstEmployeeID, Form_DataGenerator.MongoPersons.Count)];
                _issuances.Add(new MongoIssuance(person.Id, person.FirstName, dateIssued, dateReturned));
                person.Issuances.Add(new MongoPerson.MongoPersonIssuance(Id, type, dateIssued, dateReturned));

                year = Form_DataGenerator.GlobalRandom.Next(dateReturned.Value.Year, DateTime.Now.Year);
                if (year == dateReturned.Value.Year)
                {
                    month = Form_DataGenerator.GlobalRandom.Next(dateReturned.Value.Month, 13);
                    if (month == dateReturned.Value.Month)
                    {
                        day = Form_DataGenerator.GlobalRandom.Next(dateReturned.Value.Day, 29);
                        if (day == dateReturned.Value.Day)
                        {
                            month += 1;
                            if (month > 12)
                            {
                                month = 1;
                                year += 1;
                            }
                        }
                    }
                }
                else
                {
                    month = Form_DataGenerator.GlobalRandom.Next(1, 13);
                    day = Form_DataGenerator.GlobalRandom.Next(1, 29);
                }

                dateIssued = new DateTime(year, month, day);
                if (year >= DateTime.Now.Year - 1)
                {
                    person = Form_DataGenerator.MongoPersons[Form_DataGenerator.GlobalRandom.Next(MongoPerson.FirstEmployeeID, Form_DataGenerator.MongoPersons.Count)];
                    dateReturned = null;
                    _issuances.Add(new MongoIssuance(person.Id, person.FirstName, dateIssued, dateReturned));
                    person.Issuances.Add(new MongoPerson.MongoPersonIssuance(Id, type, dateIssued, dateReturned));
                    break;
                }
            }

        }
    }
}
