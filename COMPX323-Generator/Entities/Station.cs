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

namespace COMPX323_Generator.Entity
{
    public class Station
    {
        private string _address;
        private static string[] _addressStreetTypes = { " Road Station, ", " Street Station, ", " Avenue Station, ", " Crescent Station, ", " Place Station, " , " Boulevard Station, " };

        private static HashSet<string> _usedAddress = new HashSet<string>();

        public static HashSet<string> StationAddresses
        {
            get { return _usedAddress; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public Station()
        {
            // Randomize the address.
            // Query the address to check if it already exists
            // Reroll if it already exists
            // Construct sql command to add station to table.

            GenerateAddress();
            AddDataToTable();
        }

        private void GenerateAddress()
        {
            _address = File.ReadLines(@"Data/lname.txt").Skip(Form_DataGenerator.GlobalRandom.Next(1000)).FirstOrDefault();
            _address += _addressStreetTypes[Form_DataGenerator.GlobalRandom.Next(_addressStreetTypes.Length)];
            _address += File.ReadLines(@"Data/cities.txt").Skip(Form_DataGenerator.GlobalRandom.Next(96)).FirstOrDefault();
        }

        private void AddDataToTable()
        {
            bool uniqueAddress = false;
            while (!uniqueAddress)
            {
                uniqueAddress = !_usedAddress.Contains(_address);
                if (!uniqueAddress)
                    GenerateAddress();
            }

            if (Form_DataGenerator.OracleDB)
            {
                Debug.WriteLine("-----Station");
                Debug.WriteLine($"Address: {_address}");

                string comm = $@"INSERT INTO A_Stations (address) VALUES (
                '{_address}'
                )";
                Debug.WriteLine(comm);
                if (Form_DataGenerator.SQLWriter != null)
                {
                    Form_DataGenerator.SQLWriter.Write(comm);
                    Form_DataGenerator.SQLWriter.WriteLine(";");
                }
                Form_DataGenerator.ExecuteOracleCommand(comm);
            }
            _usedAddress.Add(_address);
        }
    }
}
