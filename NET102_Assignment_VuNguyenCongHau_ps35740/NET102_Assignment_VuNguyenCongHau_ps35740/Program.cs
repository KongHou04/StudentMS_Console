using NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtualGUIMenu;
using MessagePrinter;
using System.Text;
using NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject.TableObj;
using System.Reflection;
using System.ComponentModel;
using NET102_Assignment_VuNguyenCongHau_ps35740.Manager;
using System.Data.Entity.Core.Metadata.Edm;
using NET102_Assignment_VuNguyenCongHau_ps35740.Input;
using static NET102_Assignment_VuNguyenCongHau_ps35740.Menu;



// C: \Users\CONG HAU\Desktop\FPT Polytechnic\SUMMER_2023\NET102\MyLib\Menu\Menu\bin\Debug\Menu.dll

namespace NET102_Assignment_VuNguyenCongHau_ps35740
{
    internal class Program
    {
        public static string icon = "◄◄◄";
        public static bool isAdminServer = false;

        #region Main Function
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Creates Connections and Managers Class
            DbManager dbManager = new DbManager();
            AsmManager asmManager = new AsmManager();
            TablePrinter.CreateMyStr = TablePrinter.CreateRightAlignString;
            bool isQuit = new bool();
            bool isConnect = new bool();
            bool typeLogin = true;
            string connStr = string.Empty;

            // Creates All the Menus
            DefaultMenu loginMenu = new DefaultMenu();
            DefaultMenu managerMenu = new DefaultMenu();
            DefaultMenu dbManageMenu = new DefaultMenu();
            DefaultMenu asmManageMenu = new DefaultMenu();
            DefaultMenu asmOption1Menu = new DefaultMenu();
            DefaultMenu asmOption2Menu = new DefaultMenu();
            DefaultMenu asmOption3Menu = new DefaultMenu();
            DefaultMenu asmOption4Menu = new DefaultMenu();
            DefaultMenu asmOption5Menu = new DefaultMenu();
            loginMenu.SetValue(icon, Menu.LoginMenu.title, Menu.LoginMenu.menu);
            managerMenu.SetValue(icon, Menu.ManagerMenu.title, Menu.ManagerMenu.menu);
            dbManageMenu.SetValue(icon, Menu.DbManageMenu.title, Menu.DbManageMenu.menu);
            asmManageMenu.SetValue(icon, Menu.AsmManageMenu.title, Menu.AsmManageMenu.menu);
            asmOption1Menu.SetValue(icon, Menu.AsmOption1Menu.title, Menu.AsmOption1Menu.menu);
            asmOption2Menu.SetValue(icon, Menu.AsmOption2Menu.title, Menu.AsmOption2Menu.menu);
            asmOption3Menu.SetValue(icon, Menu.AsmOption3Menu.title, Menu.AsmOption3Menu.menu);
            asmOption4Menu.SetValue(icon, Menu.AsmOption4Menu.title, Menu.AsmOption4Menu.menu);
            asmOption5Menu.SetValue(icon, Menu.AsmOption5Menu.title, Menu.AsmOption5Menu.menu);

            // Creates choice variables
            int loginChoice = 1;
            int choice_1 = 1;
            int choice_2 = 1;
            int choice_3 = 1;
            int choice_3_1 = 1;
            int choice_3_2 = 1;
            int choice_3_3 = 1;
            int choice_3_4 = 1;
            int choice_3_5 = 1;
            

            // Start Login
            do
            {
                isQuit = true;
                loginChoice = loginMenu.Run(loginChoice);
                Console.WriteLine();

                if (loginChoice == 1)
                {
                    typeLogin = true;
                    isQuit = false;
                }

                if (loginChoice == 2)
                {
                    typeLogin = false;
                    isQuit = false;
                }


                // Set connString
                if (!isQuit)
                {
                    connStr = SqlServer.SetConnStr(typeLogin);
                    if (!SqlServer.CheckConnection(connStr))
                    {
                        isConnect = false;
                        Notification.PrintAsError("Cannot connect to server! Please try again or restart the program/computer");
                    }
                    else
                    {
                        Notification.PrintAsMessage("Connect successfully");
                        // Settings for dbManager amd asmManager
                        dbManager.SetConnStrForSelf(connStr);
                        asmManager.ConnStr = connStr.Replace("master", AsmManager.DbName);
                        isConnect = true;
                    }
                    Console.ReadKey();
                }
                else
                {
                    isConnect = false;
                    isQuit = SystemData.GetYesNo("You sure want to exit the program");
                    if (isQuit)
                    {
                        Notification.PrintAsMessage("Bye` Bye' :(");
                        Console.ReadKey();
                        return;
                    }
                }


                // All the Controls are here
                if (isConnect)
                {
                    

                    //asmManager = new AsmManager(connStr.Replace("master", AsmManager.DbName));
                    do
                    {
                        choice_1 = managerMenu.Run(choice_1);
                        Console.WriteLine();

                        if (choice_1 == managerMenu.OptionQuit)
                        {
                            Notification.PrintAsMessage("Stay hereee, Pleasee X_X");
                            ResetSettings(ref choice_1);
                            break;
                        }

                        // Manage the Database Asm_C#2
                        if (choice_1 == 1)
                        {
                            do
                            {
                                choice_2 = dbManageMenu.Run(choice_2);
                                Console.WriteLine();

                                if (choice_2 == dbManageMenu.OptionQuit)
                                {
                                    Notification.PrintAsMessage("Will you miss me, broo Y.Y");
                                    ResetSettings(ref choice_2);
                                    break;
                                }

                                // Creates the Database Asm_C#2
                                if (choice_2 == 1)
                                    dbManager.CreateAsmDatabase();

                                // Drops the Database Asm_C#2
                                if (choice_2 == 2)
                                    dbManager.DropAsmDatabase();

                                // Checks if the Database Asm_C#2 exists
                                if (choice_2 == 3)
                                    dbManager.CheckIfAsmDbExists();

                                // Creates backup File
                                if (choice_2 == 4)
                                    dbManager.CreateAsmDbBackup();

                                // Restore the Database Asm_C#2 using backup File
                                if (choice_2 == 5)
                                    dbManager.RestoreAsmDbBackup();

                                Console.ReadKey(true);
                            }
                            while (true);
                        }

                        // Manage the Assignment
                        if (choice_1 == 2)
                        {
                            if (!(bool)SqlServer.CheckIfDbExists(connStr, "Asm_C#2"))
                            {
                                Notification.PrintAsError("You must Create or Restore the Database Asm_C#2 first");
                                if (!isAdminServer)
                                {
                                    Notification.PrintAsMessage("The connected Server is not CongHau's server. Some function can cause errors");
                                    Notification.PrintAsMessage("You should create the Database in DB Manage Menu");
                                    Notification.PrintAsMessage("Make sure that there is no database named Asm_C#2 on your server");
                                    Notification.PrintAsMessage("If not! Try drop it in  DB Manage Menu");
                                }
                                Console.ReadKey();
                                continue;
                            }
                            do
                            {
                                choice_3 = asmManageMenu.Run(choice_3);
                                Console.WriteLine();

                                if (choice_3 == asmManageMenu.OptionQuit)
                                {
                                    Notification.PrintAsMessage("Back here and you will find out the answer :X");
                                    ResetSettings(ref choice_3);
                                    break;
                                }

                                // Add Student/Class
                                if (choice_3 == 1)
                                {

                                    do
                                    {
                                        choice_3_1 = asmOption1Menu.Run(choice_3_1);
                                        Console.WriteLine();

                                        if (choice_3_1 == asmOption1Menu.OptionQuit)
                                        {
                                            Notification.PrintAsMessage("We need new Students and  new Classes ! hic");
                                            ResetSettings(ref choice_3_1);
                                            break;
                                        }

                                        if (choice_3_1 == 1)
                                            asmManager.AddNewStudent();
                                        if (choice_3_1 == 2)
                                            asmManager.AddNewClass();
                                        Console.ReadKey(true);
                                    }
                                    while (true);
                                }

                                // Print Student/Class List
                                if (choice_3 == 2)
                                {
                                    do
                                    {
                                        choice_3_2 = asmOption2Menu.Run(choice_3_2);
                                        Console.WriteLine();

                                        if (choice_3_2 == asmOption2Menu.OptionQuit)
                                        {
                                            Notification.PrintAsMessage("You will never know who Student on server !!!");
                                            ResetSettings(ref choice_3_2);
                                            break;
                                        }
                                        if (choice_3_2 == 1)
                                            asmManager.PrintStudentList();
                                        if (choice_3_2 == 2)
                                            asmManager.PrintClassList();
                                        Console.ReadKey(true);
                                    }
                                    while (true);
                                }

                                // Prints Student List - Advanced
                                if (choice_3 == 3)
                                {
                                    do
                                    {
                                        choice_3_3 = asmOption3Menu.Run(choice_3_3);
                                        Console.WriteLine();

                                        if (choice_3_3 == asmOption3Menu.OptionQuit)
                                        {
                                            Notification.PrintAsMessage("May be you will see your self in the Excellent Student list @_@");
                                            ResetSettings(ref choice_3_3);
                                            break;
                                        }
                                        if (choice_3_3 == 1)
                                            asmManager.PrintTop5List();
                                        if (choice_3_3 == 2)
                                            asmManager.PrintStudentInEveryClass();
                                        if (choice_3_3 == 3)
                                            asmManager.PrintExcellentList();
                                        if (choice_3_3 == 4)
                                            asmManager.PrintAboveAvgList();
                                        if (choice_3_3 == 5)
                                            asmManager.PrintBelowAvgList();
                                        Console.ReadKey(true);
                                    }
                                    while (true);
                                }

                                // Searches Student
                                if (choice_3 == 4)
                                {
                                    do
                                    {
                                        choice_3_4 = asmOption4Menu.Run(choice_3_4);
                                        Console.WriteLine();

                                        if (choice_3_4 == asmOption4Menu.OptionQuit)
                                        {
                                            Notification.PrintAsMessage("Why do you go back XD");
                                            ResetSettings(ref choice_3_4);
                                            break;
                                        }
                                        if (choice_3_4 == 1)
                                            asmManager.SearchStudent();
                                        if (choice_3_4 == 2)
                                            asmManager.SearchClass();
                                        Console.ReadKey(true);
                                    }
                                    while (true);
                                }

                                // Searches and Updates Student/Class 
                                if (choice_3 == 5)
                                {
                                    do
                                    {
                                        choice_3_5 = asmOption5Menu.Run(choice_3_5);
                                        Console.WriteLine();

                                        if (choice_3_5 == asmOption5Menu.OptionQuit)
                                        {
                                            Notification.PrintAsMessage("Why do you leave me!");
                                            ResetSettings(ref choice_3_5);
                                            break;
                                        }

                                        if (choice_3_5 == 1)
                                            asmManager.SearchAndUpdateStudent();
                                        if (choice_3_5 == 2)
                                            asmManager.SearchAndUpdateClass();
                                        Console.ReadKey(true);
                                    }
                                    while (true);
                                }

                                if (choice_3 == 6)
                                    asmManager.SortAndPrintStudentList();

                                if (choice_3 == 7)
                                    asmManager.CreateAvgMarkFile();

                                Console.ReadKey(true);
                            }
                            while (true);
                        }

                        Console.ReadKey(true);
                    }
                    while (true);
                    Console.ReadKey(true);
                }
            }
            while (true);


            // Reset choice of menu
            void ResetSettings(ref int choiceNum)
            {
                choiceNum = 1;
            }

        }
        #endregion
    }
}
