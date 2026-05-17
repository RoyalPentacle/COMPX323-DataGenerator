using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMPX323_Generator.Entity;
using COMPX323_Generator.Relational;
using Oracle.ManagedDataAccess.Client;

namespace COMPX323_Generator
{
    public partial class Form_DataGenerator : Form
    {
        public Form_DataGenerator()
        {
            InitializeComponent();
        }

        public static Form_DataGenerator MasterForm;
        // The random to be used for all random number generation, so that the output is the same for any given seed.
        public static Random GlobalRandom;

        private static OracleConnection conn;

        // Default to small dataset size.
        private static int _numPersons = 20000;
        private static int _numAssets = 15000;
        private static int _numEmployees = 10000;
        private static int _numStations = 1000;
        private static int _numFirearms = 7500;
        private static int _numVehicles = 5000;
        private static int _numIncidents = 750000;

        private static bool _oracleDB = true;

        public static int GeneratorProgress = 0;
        public static int GeneratorMaxProgress;

        private static Thread _generatorThread = new Thread(CreateDataset);

        public static bool OracleDB
        {
            get {  return _oracleDB; }
        }

        private void DisableControls()
        {
            textBox_DataSource.Enabled = false;
            textBox_Username.Enabled = false;
            textBox_Password.Enabled = false;
            textBox_RandomSeed.Enabled = false;
            radioButton_LargeDataset.Enabled = false;
            radioButton_SmallDataset.Enabled = false;
            radioButton_Oracle.Enabled = false;
            radioButton_MongoDB.Enabled = false;
            button_Generate.Enabled = false;
        }

        private void EnableControls()
        {
            textBox_DataSource.Enabled = true;
            textBox_Username.Enabled = true;
            textBox_Password.Enabled = true;
            textBox_RandomSeed.Enabled = true;
            radioButton_LargeDataset.Enabled = true;
            radioButton_SmallDataset.Enabled = true;
            radioButton_Oracle.Enabled = true;
            radioButton_MongoDB.Enabled = true;
            button_Generate.Enabled = true;
        }

        private void button_Generate_Click(object sender, EventArgs e)
        {
            MasterForm = this;
            GeneratorProgress = 0;
            GeneratorMaxProgress = _numPersons + _numAssets + _numAssets + _numEmployees + _numStations + _numFirearms + _numFirearms + _numVehicles + _numVehicles + _numIncidents;

            if (textBox_RandomSeed.Text.Length > 0)
                GlobalRandom = new Random(textBox_RandomSeed.Text.GetHashCode());
            else
                GlobalRandom = new Random();
            
            
            if (EstablishDBConnection())
            {
                DropExistingTables();
                CreateTables();
                SetDatasetSize(radioButton_LargeDataset.Checked);
                GeneratorMaxProgress = _numPersons + _numAssets + _numAssets + _numEmployees + _numStations + _numFirearms + _numFirearms + _numVehicles + _numVehicles + _numIncidents;
                DisableControls();
                _generatorThread.Start();
                timer_progressUpdate.Start();
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
                comm = @"DROP TABLE IF EXISTS A_Persons CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Employees CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Stations CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Employment CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Incidents CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Involved CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Assets CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Vehicles CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Firearms CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                comm = @"DROP TABLE IF EXISTS A_Issued CASCADE CONSTRAINTS";
                ExecuteDBCommand(comm);
                
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
                )";

                // Constrain ird_number to the format of an IRD number (***-***-***)
                string employeeComm = @"CREATE TABLE IF NOT EXISTS A_Employees (
                ird_number VARCHAR(11) PRIMARY KEY,
                person_id INTEGER UNIQUE REFERENCES A_Persons(id),
                rank VARCHAR(20) NOT NULL,
                badge_number VARCHAR(6) UNIQUE
                )";

                string stationComm = @"CREATE TABLE IF NOT EXISTS A_Stations (
                address VARCHAR(50) PRIMARY KEY
                )";

                // Consider a constraint to ensure someone doesn't have a new employment while they have an entry
                // with a null end date.
                string employmentComm = @"CREATE TABLE IF NOT EXISTS A_Employment (
                ird_number VARCHAR(11) REFERENCES A_Employees(ird_number),
                station_address VARCHAR(50) REFERENCES A_Stations(address),
                start_date DATE NOT NULL,
                end_date DATE,
                PRIMARY KEY(ird_number, start_date)
                )";


                // Constrain type to specific inputs? Domestic, Robbery, Homicide, etc?
                string incidentComm = @"CREATE TABLE IF NOT EXISTS A_Incidents (
                id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                timestamp DATE NOT NULL,
                type VARCHAR(20) NOT NULL,
                address VARCHAR(50),
                description VARCHAR(400)
                )";

                // Constraints on role to act as an enum? Officer, suspect, dispatcher, etc?
                // Constrain that employees involved had active employment at the time of incident.
                string involvedComm = @"CREATE TABLE IF NOT EXISTS A_Involved (
                person_id INTEGER REFERENCES A_Persons(id),
                incident_id INTEGER REFERENCES A_Incidents(id),
                role VARCHAR(10) NOT NULL,
                description VARCHAR(200),
                PRIMARY KEY(person_id, incident_id)
                )";

                // Constrain type to 'VEHICLE', 'EQUIPMENT', 'FIREARM'
                string assetComm = @"CREATE TABLE IF NOT EXISTS A_Assets (
                id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                type VARCHAR(10) NOT NULL,
                model VARCHAR(50) NOT NULL
                )";

                // Add constraint ensuring matching asset_id has type 'VEHICLE', additional vehicle info.
                string vehicleComm = @"CREATE TABLE IF NOT EXISTS A_Vehicles (
                registration_number VARCHAR(6) PRIMARY KEY,
                asset_id INTEGER UNIQUE REFERENCES A_Assets(id)
                )";

                // Add constraint ensuring matching asset_id has type 'FIREARM', or pistol, rifle, etc. idk
                // Add additional firearm info?
                string firearmComm = @"CREATE TABLE IF NOT EXISTS A_Firearms (
                serial_number VARCHAR(8) PRIMARY KEY,
                asset_id INTEGER UNIQUE REFERENCES A_Assets(id)
                )";

                // Constrain issued and returned date to align with employee employment dates.
                string issuedComm = @"CREATE TABLE IF NOT EXISTS A_Issued (
                ird_number VARCHAR(11) REFERENCES A_Employees(ird_number),
                asset_id INTEGER REFERENCES A_Assets(id),
                date_issued DATE NOT NULL,
                date_returned DATE,
                PRIMARY KEY(asset_id, date_issued)
                )";

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

        private static void CreateDataset()
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
                UpdateLabel($"Generating Persons {i + 1}/{_numPersons} :: {GeneratorProgress}/{GeneratorMaxProgress}");
                Person p = new Person();
                GeneratorProgress++;
                UpdateProgressBar();
                // All we have to do is initialize a person and they're generated and added.
            }

            Person.FirstEmployeeID = Person._newestID + 1;

            // Stations
            for (int i = 0; i < _numStations; i++)
            {
                UpdateLabel($"Generating Stations {i + 1}/{_numStations} :: {GeneratorProgress}/{GeneratorMaxProgress}");
                Station s = new Station();
                GeneratorProgress++;
                UpdateProgressBar();
            }

            // Employees
            for (int i = 0; i < _numEmployees; i++)
            {
                UpdateLabel($"Generating Employees {i + 1}/{_numEmployees} :: {GeneratorProgress}/{GeneratorMaxProgress}");
                Employee e = new Employee();
                GeneratorProgress++;
                UpdateProgressBar();
            }

            // Equipment
            for (int i = 0; i < _numAssets; i++)
            {
                UpdateLabel($"Generating Assets {i + 1}/{_numAssets} :: {GeneratorProgress}/{GeneratorMaxProgress}");
                Asset a = new Asset("EQUIPMENT");
                GeneratorProgress++;
                UpdateProgressBar();
            }

            Asset.VehicleIDStart = Asset._newestID + 1;

            // Vehicle
            for (int i = 0; i < _numVehicles; i++)
            {
                UpdateLabel($"Generating Vehicles {i + 1}/{_numVehicles} :: {GeneratorProgress}/{GeneratorMaxProgress}");
                Vehicle v = new Vehicle();
                GeneratorProgress++;
                UpdateProgressBar();
            }

            Asset.FirearmIDStart = Asset._newestID + 1;

            // Firearm
            for (int i = 0; i < _numFirearms; i++)
            {
                UpdateLabel($"Generating Firearms {i + 1}/{_numFirearms} :: {GeneratorProgress}/{GeneratorMaxProgress}");
                Firearm f = new Firearm();
                GeneratorProgress++;
                UpdateProgressBar();
            }

            // Refactor this to be part of asset generation, like employment is for employee.
            Issued iss = new Issued();

            for (int i = 0; i < _numIncidents; i++)
            {
                UpdateLabel($"Generating Incidents {i + 1}/{_numIncidents} :: {GeneratorProgress}/{GeneratorMaxProgress}");
                Incident f = new Incident();
                GeneratorProgress++;
                UpdateProgressBar();
            }
        }
        private string _progressText;

        public string ProgressText
        {
            get { return _progressText; }
            set
            {
                _progressText = value;
            }
        }
        // temp until issued is refactored.
        public static void UpdateLabel(string text)
        {
            //MasterForm.label_CurrentStep.Text = text;
            MasterForm.ProgressText = text;
        }

        private int _progressValue;

        public int ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; }
        }
        public static void UpdateProgressBar()
        {
            //MasterForm.progressBar_Generation.Value = (int)(((float)GeneratorProgress / (float)GeneratorMaxProgress) * 100f);
            MasterForm.ProgressValue = (int)(((float)GeneratorProgress / (float)GeneratorMaxProgress) * 100f);
        }

        private void timerProgressUpdate_Tick(object sender, EventArgs e)
        {
            progressBar_Generation.Value = _progressValue;
            label_CurrentStep.Text = _progressText;
            if (_generatorThread.ThreadState == System.Threading.ThreadState.Stopped)
            {
                EnableControls();
                timer_progressUpdate.Stop();
                label_CurrentStep.Text = "Generation Complete!";
                progressBar_Generation.Value = 100;
            }
        }
    }
}
