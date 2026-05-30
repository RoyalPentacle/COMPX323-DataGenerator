using COMPX323_Generator.Entity;
using COMPX323_Generator.Relational;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPX323_Generator.MongoEntity
{
    public class MongoEmployee : MongoPerson
    {
        public class MongoEmployment
        {
            [BsonElement("station_address")]
            public string StationAddress;
            
            [BsonElement("start_date")]
            [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
            public DateTime StartDate;

            [BsonIgnoreIfNull]
            [BsonElement("end_date")]
            [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
            public DateTime? EndDate;

            public MongoEmployment(string address, DateTime startDate, DateTime? endDate)
            {
                StationAddress = address;
                StartDate = startDate;
                EndDate = endDate;
            }

        }

        private string _irdNumber;
        private string _rank; // If you have an officers rank, you're an officer, if your rank is dispatcher, you're a dispatcher.
        private string _badgeNumber; // unique or null, I'm pretty sure that's a constraint.

        private string[] _officerRanks = { "Constable", "Senior Constable", "Sergeant", "Senior Sergeant"};

        private static HashSet<string> _usedIRD = new HashSet<string>();
        private static HashSet<string> _usedBadge = new HashSet<string>();

        private List<MongoEmployment> _employment;

        [BsonIgnore]
        public static HashSet<string> UsedIRDs
        {
            get { return _usedIRD; }
        }

        [BsonElement("ird_number")]
        public string IrdNumber
        {
            get { return _irdNumber; }
            set { _irdNumber = value; }
        }

        [BsonElement("rank")]
        public string Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        [BsonElement("badge_number")]
        public string BadgeNumber
        {
            get { return _badgeNumber; }
            set { _badgeNumber = value; }
        }

        [BsonElement("employment")]
        public List<MongoEmployment> Employment
        {
            get { return _employment; }
            set { _employment = value; }
        }

        public MongoEmployee() : base()
        {
            GenerateBadge();
            _rank = _officerRanks[Form_DataGenerator.GlobalRandom.Next(_officerRanks.Length)];
            GenerateIRD();

            bool uniqueIRD = false;
            bool uniqueBadge = false;
            while (!uniqueIRD)
            {
                uniqueIRD = !_usedIRD.Contains(_irdNumber);
                if (!uniqueIRD)
                    GenerateIRD();
            }

            while (!uniqueBadge)
            {
                uniqueBadge = !_usedBadge.Contains(_badgeNumber);
                if (!uniqueBadge)
                    GenerateBadge();
            }
            _usedIRD.Add(_irdNumber);
            _usedBadge.Add(_badgeNumber);

            int employmentCount = 1;
            while (Form_DataGenerator.GlobalRandom.Next(100) > 50)
            {
                employmentCount++;
            }
            _employment = new List<MongoEmployment>();
            employmentCount = Math.Min(5, employmentCount);
            DateTime startDate = new DateTime(Form_DataGenerator.GlobalRandom.Next(DateOfBirth.Year + 18, DateTime.Now.Year), Form_DataGenerator.GlobalRandom.Next(1, 13), Form_DataGenerator.GlobalRandom.Next(1, 29));
            DateTime? endDate;
            while (employmentCount > 0)
            {
                if (startDate.Year >= DateTime.Now.Year - 1)
                {
                    employmentCount = 0;
                    endDate = null;
                }
                if (employmentCount > 1)
                {
                    endDate = new DateTime(Form_DataGenerator.GlobalRandom.Next(startDate.Year + 1, DateTime.Now.Year), Form_DataGenerator.GlobalRandom.Next(1, 13), Form_DataGenerator.GlobalRandom.Next(1, 29));
                }
                else
                    endDate = null;
                MongoStation station = Form_DataGenerator.MongoStations[Form_DataGenerator.GlobalRandom.Next(Form_DataGenerator.MongoStations.Count)];
                _employment.Add(new MongoEmployment(station.Address, startDate, endDate));
                station.StationEmployment.Add(new MongoStation.MongoStationEmployee(Id, FirstName, startDate, endDate));

                if (employmentCount > 0 && endDate != null)
                {
                    startDate = new DateTime(Form_DataGenerator.GlobalRandom.Next(endDate.Value.Year, DateTime.Now.Year + 1), Form_DataGenerator.GlobalRandom.Next(1, DateTime.Now.Month + 1), Form_DataGenerator.GlobalRandom.Next(1, 29));
                }
                employmentCount--;
            }
        }

        private void GenerateIRD()
        {
            _irdNumber = $"{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}-{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}-{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}";
        }

        private void GenerateBadge()
        {
            _badgeNumber = $"{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}";
        }
    }
}
