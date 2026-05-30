using COMPX323_Generator.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Relational
{
    public class Employment
    {
        private DateTime _startDate;
        private DateTime? _endDate;

        // Possibly build own Date struct in minimum possible bytes, so it's more reasonable to hold.
        private struct Record
        {
            public DateTime Start;
            public DateTime? End;
            public string Address;

            public Record(DateTime startDate, DateTime? endDate, string address)
            {
                Start = startDate;
                End = endDate;
                Address = address;
            }
        }

        private List<Record> _employmentRecords = new List<Record>();

        public DateTime StartDate
        {
            get { return _startDate; } 
            set { _startDate = value; }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public Employment(Employee e)
        {
            int employmentCount = 1;
            
            while (Form_DataGenerator.GlobalRandom.Next(100) > 50)
            {
                employmentCount++;
            }
            employmentCount = Math.Min(5, employmentCount);
            _startDate = new DateTime(Form_DataGenerator.GlobalRandom.Next(e.DateOfBirth.Year + 18, DateTime.Now.Year), Form_DataGenerator.GlobalRandom.Next(1, 13), Form_DataGenerator.GlobalRandom.Next(1, 29));
            while (employmentCount > 0)
            {
                if (_startDate.Year >= DateTime.Now.Year - 1)
                {
                    employmentCount = 0;
                    _endDate = null;
                }
                if (employmentCount > 1)
                {
                    _endDate = new DateTime(Form_DataGenerator.GlobalRandom.Next(_startDate.Year + 1, DateTime.Now.Year), Form_DataGenerator.GlobalRandom.Next(1, 13), Form_DataGenerator.GlobalRandom.Next(1, 29));
                }
                else
                    _endDate = null;
                string stationAddr = Station.StationAddresses.ElementAt(Form_DataGenerator.GlobalRandom.Next(Station.StationAddresses.Count));
                _employmentRecords.Add(new Record(_startDate, _endDate, stationAddr));
                if (employmentCount > 0 && _endDate != null)
                {
                    _startDate = new DateTime(Form_DataGenerator.GlobalRandom.Next(_endDate.Value.Year, DateTime.Now.Year + 1), Form_DataGenerator.GlobalRandom.Next(1, DateTime.Now.Month + 1), Form_DataGenerator.GlobalRandom.Next(1, 29));
                }
                employmentCount--;
            }
            
            // This time, not done in another function, so we can use the employee that called this.
            Debug.WriteLine("-----Employment");
            for(int i = 0; i < _employmentRecords.Count; i++)
            {
                Debug.WriteLine($"---Entry {i}");
                Debug.WriteLine($"IRD: {e.IrdNumber}");
                Debug.WriteLine($"Address: {_employmentRecords[i].Address}");
                Debug.WriteLine($"Start: {_employmentRecords[i].Start}");
                Debug.WriteLine($"End: {_employmentRecords[i].End}");
                if (Form_DataGenerator.OracleDB)
                {
                    string comm = $@"INSERT INTO A_Employment (ird_number, station_address, start_date, end_date) VALUES (
                '{e.IrdNumber}',
                '{_employmentRecords[i].Address}',
                DATE '{_employmentRecords[i].Start.Year}-{_employmentRecords[i].Start.Month}-{_employmentRecords[i].Start.Day}',
                {((_employmentRecords[i].End != null) ? $"DATE '{_employmentRecords[i].End.Value.Year}-{_employmentRecords[i].End.Value.Month}-{_employmentRecords[i].End.Value.Day}'" : "null")}
                )";
                    Debug.WriteLine(comm);
                    Form_DataGenerator.ExecuteOracleCommand(comm);
                }
                else
                {
                    //MongoDB
                }
            }
        }
    }
}
