using COMPX323_Generator.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Entity
{
    public class Firearm : Asset
    {
        private string _serialNumber;
        
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public Firearm() : base("FIREARM")
        {
            GenerateSerial();
            AddDataToTable();
        }

        private void GenerateSerial()
        {
            _serialNumber = $"{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{Form_DataGenerator.GlobalRandom.Next(100000).ToString().PadLeft(5, '0')}";
        }

        private void AddDataToTable()
        {
            // Convert the firearm and underlying asset to an SQL command.
            // Add the asset
            // Get their asset_id.
            // Query the Serial_Number to ensure it's unique.
            // Reroll it if it isn't.
            // Add the firearm.

            if (Form_DataGenerator.OracleDB)
            {
                bool uniqueSerial = false;
                while (!uniqueSerial)
                {
                    uniqueSerial = !Form_DataGenerator.ExecuteOracleDBQuery($"SELECT serial_number FROM A_Firearms WHERE serial_number = '{_serialNumber}';").HasRows;
                    if (!uniqueSerial)
                        GenerateSerial();
                }

                Debug.WriteLine("-----Firearm");
                Debug.WriteLine($"Serial: {_serialNumber}");
                Debug.WriteLine($"Asset ID: {_newestID}");

                string comm = $@"INSERT INTO A_Firearms (serial_number, asset_id) VALUES (
                '{_serialNumber}',
                {_newestID}
                );";
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
