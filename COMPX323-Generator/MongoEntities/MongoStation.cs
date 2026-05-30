using MongoDB.Bson;
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
    public class MongoStation
    {
        public class MongoStationEmployee
        {

            [BsonElement("person_id")]
            public int PersonID;

            [BsonElement("first_name")]
            public string FirstName;

            [BsonElement("start_date")]
            [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
            public DateTime StartDate;

            [BsonIgnoreIfNull]
            [BsonElement("end_date")]
            [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
            public DateTime? EndDate;

            public MongoStationEmployee(int personID, string firstName, DateTime startDate, DateTime? endDate)
            {
                PersonID = personID;
                FirstName = firstName;
                StartDate = startDate;
                EndDate = endDate;
            }
        }

        private string _address;
        private static string[] _addressStreetTypes = { " Road Station, ", " Street Station, ", " Avenue Station, ", " Crescent Station, ", " Place Station, ", " Boulevard Station, " };

        private static HashSet<string> _usedAddress = new HashSet<string>();

        [BsonIgnore]
        public static HashSet<string> StationAddresses
        {
            get { return _usedAddress; }
        }

        [BsonId]
        [BsonElement("address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private List<MongoStationEmployee> _stationEmployment;

        [BsonIgnoreIfNull]
        [BsonElement("employment")]
        public List<MongoStationEmployee> StationEmployment
        {
            get { return _stationEmployment; }
            set { _stationEmployment = value; }
        }

        public MongoStation()
        {
            // Randomize the address.
            // Query the address to check if it already exists
            // Reroll if it already exists
            // Construct sql command to add station to table.

            GenerateAddress();
            bool uniqueAddress = false;
            while (!uniqueAddress)
            {
                uniqueAddress = !_usedAddress.Contains(_address);
                if (!uniqueAddress)
                    GenerateAddress();
            }
            _usedAddress.Add(_address);
            _stationEmployment = new List<MongoStationEmployee>();
        }

        private void GenerateAddress()
        {
            _address = File.ReadLines(@"Data/lname.txt").Skip(Form_DataGenerator.GlobalRandom.Next(1000)).FirstOrDefault();
            _address += _addressStreetTypes[Form_DataGenerator.GlobalRandom.Next(_addressStreetTypes.Length)];
            _address += File.ReadLines(@"Data/cities.txt").Skip(Form_DataGenerator.GlobalRandom.Next(96)).FirstOrDefault();
        }
    }
}
