using COMPX323_Generator.Entity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.MongoEntity
{
    public class MongoFirearm : MongoAsset
    {
        private string _serialNumber;
        private static HashSet<string> _usedSerial = new HashSet<string>();

        [BsonElement("serial_number")]
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public MongoFirearm() : base("FIREARM")
        {
            GenerateSerial(); 
            bool uniqueSerial = false;
            while (!uniqueSerial)
            {
                uniqueSerial = !_usedSerial.Contains(_serialNumber);
                if (!uniqueSerial)
                    GenerateSerial();
            }
            _usedSerial.Add(_serialNumber);

        }
        private void GenerateSerial()
        {
            _serialNumber = $"{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{Form_DataGenerator.GlobalRandom.Next(100000).ToString().PadLeft(5, '0')}";
        }
    }
}
