using MessagePrinter;
using NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject;
using System;
using System.Xml.Linq;


namespace NET102_Assignment_VuNguyenCongHau_ps35740.Manager
{
    internal class DbManager
    {

        #region Properties
        public string ConnStr;
        public const string DB_NAME = "Asm_C#2";
        #endregion



        #region Constructors

        /// <summary>
        ///     Creates a dbManager
        /// </summary>
        public DbManager()
        {
        }


        /// <summary>
        ///     Create a dbManager with custom connString
        /// </summary>
        /// <param name="connStr"></param>
        public DbManager(string connStr)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            ConnStr = connStr;
        }

        #endregion



        #region Methods

        /// <summary>
        ///     Sets the connString for self
        /// </summary>
        /// <param name="connStr"></param>
        public void SetConnStrForSelf(string connStr)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            ConnStr = connStr;
        }


        /// <summary>
        ///     Create the Database Asm_C#2 on the server
        /// </summary>
        public void CreateAsmDatabase()
        {
            if (SqlServer.CheckIfDbExists(ConnStr, DB_NAME) == null)
            {
                Notification.PrintAsError("Somethings went wrong:(");
                return;
            }
            if ((bool)SqlServer.CheckIfDbExists(ConnStr, DB_NAME))
            {
                Notification.PrintAsError("Database already exist");
                return;
            }
            try 
            { 
                SqlServer.CreateDb(ConnStr, DB_NAME, "Asm_C#2.sql");
                Notification.PrintAsMessage("Create Database Asm_C#2 successfully! Yayhh");
            }
            catch (Exception ex)
            {
                Notification.PrintAsError("Cannot create the Database Asm_C#2");
                Notification.PrintAsError(ex.Message);
                Notification.PrintAsError("If you waited too long! You should check the Sql server connection or restart program/computer");
            }

        }


        /// <summary>
        ///     Drops the database Asm_C#2 on server
        /// </summary>
        public void DropAsmDatabase()
        {
            if (!(bool)SqlServer.CheckIfDbExists(ConnStr, DB_NAME))
            {
                Notification.PrintAsError("The Database Name does not exist");
                return;
            }
            try
            {
                SqlServer.DropDb(ConnStr, DB_NAME);
                Notification.PrintAsMessage($"Drop Database Asm_C#2 successfully");
            }
            catch (Exception ex)
            {
                Notification.PrintAsError($"Cannot drop Database Asm_C#2");
                Notification.PrintAsError(ex.Message);
                Notification.PrintAsError("If you waited too long! You should check the Sql server connection or restart program/computer");
            }
        }


        /// <summary>
        ///     Shows the result of Check if the Database Asm_C#2 exists
        /// </summary>
        public void CheckIfAsmDbExists()
        {
            if ((bool)SqlServer.CheckIfDbExists(ConnStr, DB_NAME))
                Notification.PrintAsMessage("Database Asm_C#2 already exists");
            else if (!(bool)SqlServer.CheckIfDbExists(ConnStr, "Asm_C#2"))
                Notification.PrintAsMessage("Da8tabase Asm_C#2 does not exist");
            else
                Notification.PrintAsError("If you waited too long! You should check the Sql server connection or restart program/computer");
        }


        public void CreateAsmDbBackup()
        {
            if (!(bool)SqlServer.CheckIfDbExists(ConnStr, DB_NAME))
            {
                Notification.PrintAsError("The Database does not exist");
                return;
            }
            try
            {
                SqlServer.CreateDbBackUp(ConnStr, DB_NAME, "AsmDbBackup.BAK");
                Notification.PrintAsMessage("Created Database Asm_C#2 Back up successfully");
            }
            catch(Exception ex)
            {
                Notification.PrintAsError (ex.Message);
            }
        }


        public void RestoreAsmDbBackup()
        {
            if ((bool)SqlServer.CheckIfDbExists(ConnStr, DB_NAME))
            {
                Notification.PrintAsError("The Database already exists");
                return;
            }

            try
            {
                SqlServer.RestoreDbBackUp(ConnStr, DB_NAME, "AsmDbBackup.BAK");
                Notification.PrintAsMessage("Restore Database Asm_C#2 from Back up file successfully");
            }
            catch (Exception ex)
            {
                Notification.PrintAsError(ex.Message);
            }
        }

        #endregion

    }
}
