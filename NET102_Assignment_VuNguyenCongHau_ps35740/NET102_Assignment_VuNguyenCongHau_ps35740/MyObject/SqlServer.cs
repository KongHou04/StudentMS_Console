using System;
using System.Data.SqlClient;
using System.IO;
using MessagePrinter;
using NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject.TableObj;


namespace NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject
{
    internal static class SqlServer
    {
        #region Method

        /// <summary>
        ///     Returns a ConnString entered from the user
        /// </summary>
        /// <param name="typeConn"></param>
        /// <returns></returns>
        public static string SetConnStr(bool typeOfConn)
        {
            string svName;
            Notification.PrintAsRequest("Enter sever name");
            svName = Console.ReadLine();
            if (svName == "ps35740")
            {
                svName = @"LAPTOP-BV87SDMM\HAU";
                Program.isAdminServer = true;
            }
            if (typeOfConn)
            {
                return "Data Source =" + svName + "; Initial Catalog = master; Integrated Security = true;";
            }
            else
            {
                Notification.PrintAsRequest("Enter username");
                string username = Console.ReadLine();
                Notification.PrintAsRequest("Enter password");
                string password = Console.ReadLine();
                return "Data Source =" + svName + "; Initial Catalog = master; User ID =" + username + "; Password =" + password;
            }
        }

        /// <summary>
        ///     Checks the Connection using inputted connString
        ///     True if connectable
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static bool CheckConnection(string connStr)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            using (SqlConnection demoConn = new SqlConnection(connStr))
            {
                try
                {
                    demoConn.Open();
                    demoConn.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        /// <summary>
        ///     Create a database using inputted dbName, Create File name and connString
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="dbName"></param>
        /// <param name="createFileName"></param>
        /// <exception cref="Exception"></exception>
        public static void CreateDb(string connStr, string dbName, string createFileName)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            if (dbName == null || dbName == string.Empty)
                throw new Exception("The Database name cannot be null or empty");
            if (createFileName == null || createFileName == string.Empty)
                throw new Exception("The Create File name cannot be null or empty");
            string cmdStr = "";
            // Sets directory
            string sqlDir = @"..\..\..\..\Resources\" + createFileName;
            //string sqlDir = @"..\..\..\..\Resources\Asm_C#2.sql";
            FileInfo createFile = new FileInfo(sqlDir);
            if (createFile.Exists)
            {
                using (StreamReader sr = new StreamReader(sqlDir))
                {
                    // Changes code sql
                    string[] temp = System.IO.File.ReadAllLines(sqlDir);
                    temp[0] = "";
                    temp[1] = "";
                    foreach (string item in temp)
                    {
                        cmdStr += item;
                    }
                }
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand createCmd = new SqlCommand())
                    {
                        // Creates the database using sqlCommand first
                        createCmd.CommandText = "CREATE DATABASE [Asm_C#2]";
                        createCmd.CommandText = "CREATE DATABASE [" + dbName + "]";
                        createCmd.Connection = conn;
                        createCmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            else
                throw new Exception("The Create database file does not exists!");
        }


        /// <summary>
        ///     Drops the database on the server which has the inputted dbName
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="dbName"></param>
        public static void DropDb(string connStr, string dbName)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            if (dbName == null || dbName == string.Empty)
                throw new Exception("The Database name cannot be null or empty");
            // Sql script - delete database
            //@"use master
            //alter database Asm_C#2 set single_user with rollback immediate;
            //drop database Asm_C#2;";
            string cmdStr = @"use master
                            alter database " + dbName + @" set single_user with rollback immediate;
                            drop database " + dbName + @" ;";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                conn.ChangeDatabase("master");
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        /// <summary>
        ///     Checks if the Database which has inputted dbName exists
        ///     True if exists - False if not
        ///     Null if code or connection has any error
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static bool? CheckIfDbExists(string connStr, string dbName)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            if (dbName == null || dbName == string.Empty)
                throw new Exception("The Database name cannot be null or empty");
            //$"SELECT db_id('Asm_C#2')"
            string cmdStr = $"SELECT db_id('" + dbName + "')";
            // Sql script - Check if database exist? True if exits, False if not exists
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (var command = new SqlCommand(cmdStr, conn))
                {
                    conn.Open();
                    try
                    {
                        return (command.ExecuteScalar() != DBNull.Value);
                    }
                    catch { return null; }
                }
            }
        }


        /// <summary>
        ///     Creates a Backup File for the Database choosed
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="dbName"></param>
        /// <param name="backupFileName"></param>
        /// <exception cref="Exception"></exception>
        public static void CreateDbBackUp(string connStr, string dbName, string backupFileName)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            if (dbName == null || dbName == string.Empty)
                throw new Exception("The Database name cannot be null or empty");

            string path = @"..\..\..\..\Backups\" + backupFileName;
            DirectoryInfo backupDir = new DirectoryInfo(path);

            // Sql script - Create database backup
            //BACKUP DATABASE Asm_C#2 
            //TO DISK = 'D:\NET102_Assignment_VuNguyenCongHau_ps35740\Backups\AsmDbBackup.BAK'
            //GO

            string cmdStr = @"BACKUP DATABASE " + dbName
                            + @" TO DISK = '" + backupDir.FullName + "'";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                conn.ChangeDatabase("master");
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        /// <summary>
        ///     Restores a Database using Backup File
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="dbName"></param>
        /// <param name="backupFileName"></param>
        /// <exception cref="Exception"></exception>
        public static void RestoreDbBackUp(string connStr, string dbName, string backupFileName)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            if (dbName == null || dbName == string.Empty)
                throw new Exception("The Database name cannot be null or empty");

            string path = @"..\..\..\..\Backups\" + backupFileName;
            DirectoryInfo backupDir = new DirectoryInfo(path);
            //if (!backupDir.Exists)
            //    throw new Exception("The Back up File does not exist");

            // Sql script - Create database backup
            //RESTORE DATABASE Asm_C#2 
            //FROM  DISK = 'D:\NET102_Assignment_VuNguyenCongHau_ps35740\Backups\AsmDbBackup.BAK'
            //GO
            string cmdDemo = "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'"
                + dbName + "') DROP DATABASE "
                + dbName + " RESTORE DATABASE "
                + dbName + " FROM DISK = '"
                + backupDir.FullName + "'";
            string cmdStr = @"RESTORE DATABASE " + dbName
                            + @" FROM  DISK = '" + backupDir.FullName + "'";
            ;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                conn.ChangeDatabase("master");
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        #endregion

    }
}
