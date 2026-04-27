using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace COMPX323_Generator
{
    public partial class Form_DataGenerator : Form
    {
        public Form_DataGenerator()
        {
            InitializeComponent();
        }


        // The random to be used for all random number generation, so that the output is the same for any given seed.
        public static Random GlobalRandom;

        // SQLite specific stuff?
        private static string _databasePath = "Data Source=policeDB323.db";
        public static SQLiteConnection conn;

        // Default to small dataset size.
        private static int _numPersons = 20;
        private static int _numAssets = 10;
        private static int _numEmployees = 10;
        private static int _numStations = 5;
        private static int _numFirearms = 5;
        private static int _numVehicles = 5;
        private static int _numIncidents = 15;



        private void button_Generate_Click(object sender, EventArgs e)
        {
            if (textBox_RandomSeed.Text.Length > 0)
                GlobalRandom = new Random(textBox_RandomSeed.Text.GetHashCode());
            else
                GlobalRandom = new Random();

            EstablishDBConnection();
            DropExistingTables();
            CreateTables();
            SetDatasetSize(radioButton_LargeDataset.Checked);
            CreateDataset();
        }

        private void EstablishDBConnection()
        {
            conn = new SQLiteConnection(_databasePath);
            conn.Open();
        }

        private void ExecuteDBCommand(string command)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = command;
                cmd.ExecuteNonQuery();
            }
        }

        private void DropExistingTables()
        {
            string comm = @"PRAGMA foreign_keys = OFF;
                DROP TABLE IF EXISTS Persons;
                DROP TABLE IF EXISTS Employees;
                DROP TABLE IF EXISTS Stations;
                DROP TABLE IF EXISTS Employment;
                DROP TABLE IF EXISTS Incidents;
                DROP TABLE IF EXISTS Involved;
                DROP TABLE IF EXISTS Assets;
                DROP TABLE IF EXISTS Vehicles;
                DROP TABLE IF EXISTS Firearms;
                DROP TABLE IF EXISTS Issued;
                PRAGMA foreign_keys = ON;
            ";

            ExecuteDBCommand(comm);
        }

        private void CreateTables()
        {
            string personComm = @"CREATE TABLE IF NOT EXISTS Persons (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                first_name VARCHAR NOT NULL,
                last_name VARCHAR NOT NULL,
                phone_number VARCHAR,
                address VARCHAR,
                date_of_birth DATE
            );";

            // Constrain ird_number to just numerical inputs, but as a VARCHAR.
            string employeeComm = @"CREATE TABLE IF NOT EXISTS Employees (
                ird_number VARCHAR PRIMARY KEY,
                person_id INTEGER UNIQUE REFERENCES Persons(id),
                rank VARCHAR NOT NULL,
                badge_number INTEGER UNIQUE
            );";

            string stationComm = @"CREATE TABLE IF NOT EXISTS Stations (
                address VARCHAR PRIMARY KEY
            );";

            // Consider a constraint to ensure someone doesn't have a new employment while they have an entry
            // with a null end date.
            // Constrain type to specific inputs? Domestic, Robbery, Homicide, etc?
            string employmentComm = @"CREATE TABLE IF NOT EXISTS Employment (
                ird_number INTEGER REFERENCES Employees(ird_number),
                station_address VARCHAR REFERENCES Stations(address),
                start_date DATE NOT NULL,
                end_date DATE,
                PRIMARY KEY(ird_number, station_address, start_date)
            );";

            string incidentComm = @"CREATE TABLE IF NOT EXISTS Incidents (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                timestamp DATETIME NOT NULL,
                type VARCHAR NOT NULL,
                address VARCHAR,
                description VARCHAR
            );";

            // Constraints on role to act as an enum? Officer, suspect, dispatcher, etc?
            string involvedComm = @"CREATE TABLE IF NOT EXISTS Involved (
                person_id INTEGER REFERENCES Persons(id),
                incident_id INTEGER REFERENCES Incidents(id),
                role VARCHAR NOT NULL,
                description VARCHAR,
                PRIMARY KEY(person_id, incident_id)
            );";

            string assetComm = @"CREATE TABLE IF NOT EXISTS Assets (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                type VARCHAR NOT NULL,
                model VARCHAR NOT NULL
            );";

            // Add constraint ensuring matching asset_id has type 'VEHICLE', additional vehicle info.
            string vehicleComm = @"CREATE TABLE IF NOT EXISTS Vehicles (
                registration_number INTEGER PRIMARY KEY,
                asset_id INTEGER UNIQUE REFERENCES Assets(id)
            );";

            // Add constraint ensuring matching asset_id has type 'FIREARM', or pistol, rifle, etc. idk
            // Add additional firearm info?
            string firearmComm = @"CREATE TABLE IF NOT EXISTS Firearms (
                serial_number INTEGER PRIMARY KEY,
                asset_id INTEGER UNIQUE REFERENCES Assets(id)
            );";

            string issuedComm = @"CREATE TABLE IF NOT EXISTS Issued (
                ird_number INTEGER REFERENCES Employees(ird_number),
                asset_id INTEGER REFERENCES Assets(id),
                date_issued DATETIME NOT NULL,
                date_returned DATETIME,
                PRIMARY KEY(ird_number, asset_id, date_issued)
            );";

            ExecuteDBCommand(personComm);
            ExecuteDBCommand(employeeComm);
            ExecuteDBCommand(stationComm);
            ExecuteDBCommand(employmentComm);
            ExecuteDBCommand(incidentComm);
            ExecuteDBCommand(involvedComm);
            ExecuteDBCommand(assetComm);
            ExecuteDBCommand(vehicleComm);
            ExecuteDBCommand(firearmComm);
            ExecuteDBCommand(issuedComm);
        }

        private void SetDatasetSize(bool largeDataset)
        {
            if (largeDataset)
            {
                // set the size variables to big.
                // we auto determine the number of entries for the relational tables.
            }
        }

        private void CreateDataset()
        {
            // Randomly generate the appropriate number of entries in each table, based on the dataset size variables.
            // Ensure we generate the relational entries to match.
            // Also make sure we don't hold too many entries in memory at once if we can help it.
            // Generate a thing, add it, then forget about it.
            // Utilize queries to pull information we need for relations, etc.
            // Generalized entities should handle the creation of their inherited base.
        }
    }
}
