using COMPX323_Generator.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Entity
{
    public class Employee : Person
    {
        private string _irdNumber;
        private string _rank; // If you have an officers rank, you're an officer, if your rank is dispatcher, you're a dispatcher.
        private string _badgeNumber; // unique or null, I'm pretty sure that's a constraint.

        private string[] _officerRanks = { "Constable", "Senior Constable", "Sergeant", "Senior Sergeant"};

        public string IrdNumber
        {
            get { return _irdNumber; }
            set { _irdNumber = value; }
        }

        public string Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public string BadgeNumber
        {
            get { return _badgeNumber; }
            set { _badgeNumber = value; }
        }

        public Employee() : base()
        {
            GenerateBadge();
            _rank = _officerRanks[Form_DataGenerator.GlobalRandom.Next(_officerRanks.Length)];
            GenerateIRD();
            AddDataToTable();
        }

        private void GenerateIRD()
        {
            _irdNumber = $"{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}-{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}-{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}";
        }

        private void GenerateBadge()
        {
            _badgeNumber = $"{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{Form_DataGenerator.GlobalRandom.Next(1000)}";
        }

        private void AddDataToTable()
        {
            // Convert the employee and underlying person to an SQL command.
            // Add the person.
            // Get their person_id.
            // Query the IRD number and Badge Number to ensure they're unique
            // Reroll them if they aren't.
            // Add the employee.

            if (Form_DataGenerator.OracleDB)
            {
                int personID = Form_DataGenerator.ExecuteOracleDBQuery($"SELECT id FROM A_Persons ORDER BY id_column DESC FETCH FIRST 1 ROW ONLY;").GetInt32(0);
                bool uniqueIRD = false;
                bool uniqueBadge = false;
                while (!uniqueIRD)
                {
                    uniqueIRD = !Form_DataGenerator.ExecuteOracleDBQuery($"SELECT ird_number FROM A_Employees WHERE ird_number = '{_irdNumber}';").HasRows;
                    if (!uniqueIRD)
                        GenerateIRD();
                }

                while (!uniqueBadge)
                {
                    uniqueBadge = !Form_DataGenerator.ExecuteOracleDBQuery($"SELECT badge_number FROM A_Employees WHERE badge_number = '{_badgeNumber}';").HasRows;
                    if (!uniqueBadge)
                        GenerateBadge();
                }

                Debug.WriteLine("-----");
                Debug.WriteLine($"IRD: {_irdNumber}");
                Debug.WriteLine($"PersonID: {personID}");
                Debug.WriteLine($"Rank: {_rank}");
                Debug.WriteLine($"Badge: {_badgeNumber}");


                string comm = $@"INSERT INTO A_Employees (ird_number, person_id, rank, badge_number) VALUES (
                    '{_irdNumber}',
                    {personID},
                    '{_rank}',
                    '{_badgeNumber}'
                );";

                Debug.WriteLine(comm);
                Form_DataGenerator.ExecuteDBCommand(comm);
            }
        }

    }
}
