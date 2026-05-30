using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace COMPX323_Generator.MongoEntity
{
    [BsonKnownTypes(typeof(MongoEmployee))]
    public class MongoPerson
    {
        public class MongoPersonInvolvement
        {
            [BsonElement("incident_id")]
            public int IncidentID;

            [BsonElement("timestamp")]
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime Timestamp;

            [BsonElement("address")]
            public string Address;

            public MongoPersonInvolvement(int incidentID, DateTime timestamp, string address)
            {
                IncidentID = incidentID;
                Timestamp = timestamp;
                Address = address;
            }
        }

        public class MongoPersonIssuance
        {
            [BsonElement("asset_id")]
            public int AssetId;
            [BsonElement("type")]
            public string Type;
            

            [BsonElement("date_issued")]
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime IssueTime;

            [BsonIgnoreIfNull]
            [BsonElement("date_returned")]
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime? ReturnTime;

            public MongoPersonIssuance(int assetID, string type, DateTime issueTime, DateTime? returnTime)
            {
                AssetId = assetID;
                Type = type;
                IssueTime = issueTime;
                ReturnTime = returnTime;
            }
        }

        protected string _firstName;
        protected string _lastName;
        protected string _address;
        protected string _phoneNumber;
        protected DateTime _dateOfBirth;

        private static string[] _phonePrefix = { "020", "021", "022", "027", "028" };
        private static string[] _addressStreetTypes = { " Road, ", " Street, ", " Avenue, ", " Crescent, ", " Place, ", " Boulevard, " };

        [BsonIgnore]
        public static int _newestID = 0;

        [BsonIgnore]
        public static int FirstEmployeeID;

        private List<MongoPersonInvolvement> _involvement;
        private List<MongoPersonIssuance> _issuance;


        [BsonElement("id")]
        public int Id
        {
            get; set;
        }

        [BsonElement("first_name")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [BsonElement("last_name")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [BsonElement("address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [BsonElement("phone_number")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        [BsonElement("date_of_birth")]
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        [BsonIgnoreIfNull]
        [BsonElement("involvement")]
        public List<MongoPersonInvolvement> Involvement
        {
            get { return _involvement; }
            set { _involvement = value; }
        }

        [BsonIgnoreIfNull]
        [BsonElement("issuances")]
        public List<MongoPersonIssuance> Issuances
        {
            get { return _issuance; }
            set { _issuance = value; }
        }

        public MongoPerson()
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
            Id = _newestID;
            _newestID++;
            _involvement = new List<MongoPersonInvolvement>();
            _issuance = new List<MongoPersonIssuance>();
        }

    }
}
