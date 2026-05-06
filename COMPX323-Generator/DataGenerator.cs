using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMPX323_Generator.Entity;
using Oracle.ManagedDataAccess.Client;

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

        private static OracleConnection conn;

        // Default to small dataset size.
        private static int _numPersons = 20;
        private static int _numAssets = 10;
        private static int _numEmployees = 10;
        private static int _numStations = 5;
        private static int _numFirearms = 5;
        private static int _numVehicles = 5;
        private static int _numIncidents = 15;

        private static bool _oracleDB = true;

        public static bool OracleDB
        {
            get {  return _oracleDB; }
        }



        private void button_Generate_Click(object sender, EventArgs e)
        {
            if (textBox_RandomSeed.Text.Length > 0)
                GlobalRandom = new Random(textBox_RandomSeed.Text.GetHashCode());
            else
                GlobalRandom = new Random();
            
            
            if (EstablishDBConnection())
            {
                DropExistingTables();
                CreateTables();
                SetDatasetSize(radioButton_LargeDataset.Checked);
                CreateDataset();
            }
        }

        private bool EstablishDBConnection()
        {
            
            if (textBox_DataSource.Text.Length == 0 || textBox_Username.Text.Length == 0 || textBox_Username.Text.Length == 0)
            {
                MessageBox.Show("Please enter database credentials.");
                return false;
            }

            if (radioButton_Oracle.Checked)
            {
                _oracleDB = true;
                string connString = $"User Id={textBox_Username.Text};Password={textBox_Password.Text};Data Source={textBox_DataSource.Text};";

                try
                {
                    conn = new OracleConnection(connString);
                    conn.Open(); // Why isn't this a TryOpen that does a true/false with an out for an error message instead of an exception? Billion dollar company btw.
                }
                catch (Exception ex) // If the connection fails to open, tell them why.
                {
                    MessageBox.Show($"Failed to connect to database.\n{ex.Message}");
                    return false;
                }
            }
            else
            {
                // MongoDB stuff.
                _oracleDB = false;
                MessageBox.Show("MongoDB is currently unsupported.");
                return false;
            }
            return true;
        }

        public static void ExecuteDBCommand(string command)
        {
            if (_oracleDB)
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                //MongoDB
            }
        }

        public static OracleDataReader ExecuteOracleDBQuery(string query)
        {
            using (OracleCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                return cmd.ExecuteReader();
            }
        }


        private void DropExistingTables()
        {
            string comm = "";
            if (_oracleDB)
            {
                comm = @"DROP TABLE IF EXISTS A_Persons CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Employees CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Stations CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Employment CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Incidents CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Involved CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Assets CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Vehicles CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Firearms CASCADE CONSTRAINTS;
                DROP TABLE IF EXISTS A_Issued CASCADE CONSTRAINTS;
                ";
            }
            else
            {
                //MongoDB
            }
            ExecuteDBCommand(comm);
        }

        private void CreateTables()
        {
            if (_oracleDB)
            {
                string personComm = @"CREATE TABLE IF NOT EXISTS A_Persons (
                id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                first_name VARCHAR(50) NOT NULL,
                last_name VARCHAR(50) NOT NULL,
                phone_number VARCHAR(20),
                address VARCHAR(50),
                date_of_birth DATE
                );";

                // Constrain ird_number to just numerical inputs, but as a VARCHAR.
                string employeeComm = @"CREATE TABLE IF NOT EXISTS A_Employees (
                ird_number VARCHAR PRIMARY KEY,
                person_id INTEGER UNIQUE REFERENCES A_Persons(id),
                rank VARCHAR NOT NULL,
                badge_number VARCHAR(6) UNIQUE
                );";

                string stationComm = @"CREATE TABLE IF NOT EXISTS A_Stations (
                address VARCHAR PRIMARY KEY
                );";

                // Consider a constraint to ensure someone doesn't have a new employment while they have an entry
                // with a null end date.
                // Constrain type to specific inputs? Domestic, Robbery, Homicide, etc?
                string employmentComm = @"CREATE TABLE IF NOT EXISTS A_Employment (
                ird_number INTEGER REFERENCES A_Employees(ird_number),
                station_address VARCHAR REFERENCES A_Stations(address),
                start_date DATE NOT NULL,
                end_date DATE,
                PRIMARY KEY(ird_number, station_address, start_date)
                );";

                string incidentComm = @"CREATE TABLE IF NOT EXISTS A_Incidents (
                id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                timestamp DATETIME NOT NULL,
                type VARCHAR NOT NULL,
                address VARCHAR,
                description VARCHAR
                );";

                // Constraints on role to act as an enum? Officer, suspect, dispatcher, etc?
                string involvedComm = @"CREATE TABLE IF NOT EXISTS A_Involved (
                person_id INTEGER REFERENCES A_Persons(id),
                incident_id INTEGER REFERENCES A_Incidents(id),
                role VARCHAR NOT NULL,
                description VARCHAR,
                PRIMARY KEY(person_id, incident_id)
                );";

                string assetComm = @"CREATE TABLE IF NOT EXISTS A_Assets (
                id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                type VARCHAR NOT NULL,
                model VARCHAR NOT NULL
                );";

                // Add constraint ensuring matching asset_id has type 'VEHICLE', additional vehicle info.
                string vehicleComm = @"CREATE TABLE IF NOT EXISTS A_Vehicles (
                registration_number VARCHAR PRIMARY KEY,
                asset_id INTEGER UNIQUE REFERENCES A_Assets(id)
                );";

                // Add constraint ensuring matching asset_id has type 'FIREARM', or pistol, rifle, etc. idk
                // Add additional firearm info?
                string firearmComm = @"CREATE TABLE IF NOT EXISTS A_Firearms (
                serial_number VARCHAR PRIMARY KEY,
                asset_id INTEGER UNIQUE REFERENCES A_Assets(id)
                );";

                string issuedComm = @"CREATE TABLE IF NOT EXISTS A_Issued (
                ird_number INTEGER REFERENCES A_Employees(ird_number),
                asset_id INTEGER REFERENCES A_Assets(id),
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
            else
            {
                //MongoDB
            }
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

            // Persons
            for (int i = 0; i < _numPersons; i++)
            {
                Person p = new Person();
                // All we have to do is initialize a person and they're generated and added.
            }

            // Employees
            for (int i = 0; i < _numEmployees; i++)
            {
                Employee e = new Employee();
            }

            // Stations
            for (int i = 0; i < _numStations; i++)
            {
                Station s = new Station();
            }

            // Equipment
            for (int i = 0; i < _numAssets; i++)
            {
                Asset a = new Asset("EQUIPMENT");
            }

            // Vehicle
            for (int i = 0; i < _numVehicles; i++)
            {
                Vehicle v = new Vehicle();
            }

            // Firearm
            for (int i = 0; i < _numFirearms; i++)
            {
                Firearm f = new Firearm();
            }
        }
    }
}
