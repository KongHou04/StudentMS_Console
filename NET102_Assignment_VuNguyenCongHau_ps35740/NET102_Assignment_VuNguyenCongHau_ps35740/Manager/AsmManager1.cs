using MessagePrinter;
using NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject.TableObj;
using NET102_Assignment_VuNguyenCongHau_ps35740.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VirtualGUIMenu;

namespace NET102_Assignment_VuNguyenCongHau_ps35740.Manager
{
    public partial class AsmManager
    {

        #region Fields
        private string _connStr;
        #endregion



        #region Properties

        public string ConnStr
        {
            set { 
                _connStr = value;
                Conn = new SqlConnection(_connStr);
                Asm_Cs = new AsmDb(Conn, true);
            }
        }
        public SqlConnection Conn;
        public static string DbName = "Asm_C#2";
        public AsmDb Asm_Cs;

        #endregion



        #region Constructors

        /// <summary>
        ///     Create a AsmManager
        /// </summary>
        public AsmManager() { }

        /// <summary>
        ///     Create a AsmManager using custom ConnString
        /// </summary>
        /// <param name="connStr"></param>
        /// <exception cref="Exception"></exception>
        public AsmManager(string connStr)
        {
            if (connStr == null || connStr == string.Empty)
                throw new Exception("The ConnString cannot be null or empty");
            ConnStr = connStr;
            if (Asm_Cs == null)
            {
                Conn = new SqlConnection(connStr);
                Asm_Cs = new AsmDb(Conn, true);
            }
        }

        #endregion



        #region Methods

        /// <summary>
        ///     Function - Adds new Class
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void AddNewClass()
        {

            // Gets number of Classes user wanna add to the Database
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            int n = SystemData.GetNumber("Enter the number of Classes you wanna add", 10, 0);
            Console.WriteLine();

            // Returns if n == 0
            if (n == 0)
            {
                Notification.PrintAsMessage("See youu next time!");
                return;
            };

            // Create new Classes Information
            Console.WriteLine("----------------------------------------------");
            for (int i = 0; i < n; i++)
            {
                Notification.PrintAsMessage($"Input Class {Asm_Cs.Classes.Count() + 1} information");
                Console.WriteLine();

                // Enters new Class information
                Class cls = new Class();
                do
                {
                    cls.NameClass = ClassData.GetNameClass("Enter the Class Name");
                    if (cls.NameClass.Length == 0)
                    {
                        Notification.PrintAsMessage("See you next time :(");
                        return;
                    }    
                    // Checks if the NameClass is unique - not necessary
                    if (Asm_Cs.Classes.Where(c => c.NameClass == cls.NameClass).Count() == 0)
                        break;
                    Notification.PrintAsError("This Class Name already exists");
                }
                while (true);

                // Adds and Saves Changes
                Asm_Cs.Classes.Add(cls);
                try
                {
                    Asm_Cs.SaveChanges();
                    Notification.PrintAsMessage("Add New Class successfully");
                }
                catch (Exception ex)
                {
                    Notification.PrintAsError(ex.Message);
                }
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------");
            }
        }


        /// <summary>
        ///     Print Class List
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PrintClassList()
        {
            // Check if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Classes.Count() == 0)
            {
                Notification.PrintAsMessage("There are no Class at all");
                return;
            }
            TablePrinter.RightAlignPrint(Asm_Cs.Classes.ToList());
            Notification.PrintAsMessage("Press any key to go back");
        }


        /// <summary>
        ///     Search Class using custom Conditions
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void SearchClass()
        {
            // Checks if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Classes.Count() == 0)
                Notification.PrintAsMessage("No Class exists yet, please add Class list first");

            // Creates Search Menu
            DefaultMenu searchMenu = new DefaultMenu();
            searchMenu.SetCutLineIndexList(Menu.SearchClassMenu.cutLineIndexList);
            string option1 = Menu.SearchClassMenu.menu[0];
            string option2 = Menu.SearchClassMenu.menu[1];
            string[] menu = Menu.SearchClassMenu.menu;
            searchMenu.SetFlexibleValues(45, 2);

            // Creates Conditions
            int? idClassCond = null;
            string nameClassCond = string.Empty;
            

            int choice = 1;
            do
            {
                // Reset the Menu Setting
                menu[0] = option1 + ((idClassCond == null) ? "" : $" - {idClassCond}");
                menu[1] = option2 + ((nameClassCond == string.Empty) ? "" : $" - %{nameClassCond}%");
                searchMenu.SetValue(Program.icon, Menu.SearchClassMenu.title, Menu.SearchClassMenu.menu);

                choice = searchMenu.Run(choice);
                Console.WriteLine();

                //Console.WriteLine(oldMaxWidth = searchMenu.MaxWidth);
                if (choice == searchMenu.OptionQuit)
                {
                    Notification.PrintAsMessage("Dont worry about me Y.Y");
                    break;
                }

                #region Sets Conditions
                if (choice == 1)
                {
                    int tempIdClassCond = ClassData.GetIdClass("Enter the IdClass you wanna look for");
                    if (tempIdClassCond == 0 || tempIdClassCond == -1)
                    {
                        Notification.PrintAsMessage("Hic! why you didnt change it");
                        Console.ReadKey();
                        continue;
                    }
                    idClassCond = tempIdClassCond;
                    Notification.PrintAsMessage("Set new IdClass Condition successfully");
                }

                if (choice == 2)
                {
                    string tempNameClassCond = ClassData.GetNameClass("Enter a string of NameClass you wanna look for");
                    if (tempNameClassCond.Length == 0)
                    {
                        Notification.PrintAsMessage("That name was insane !!");
                        Console.ReadKey();
                        continue;
                    }
                    nameClassCond = tempNameClassCond;
                    Notification.PrintAsMessage("Set new NameClass Condition successfully");
                }
                #endregion

                if (choice == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Conditions: ");
                    Console.Write("{0} - ", (idClassCond == null) ? "default" : idClassCond.ToString());
                    Console.Write("{0}\n", (nameClassCond == string.Empty) ? "default" : $"%{nameClassCond}%");
                    Console.ResetColor();
                }

                if (choice == 4)
                {
                    idClassCond = new int?();
                    nameClassCond = string.Empty;
                    Notification.PrintAsMessage("Removed Conditions successfully");
                }

                if (choice == 5)
                {
                    List<Class> clsList = Asm_Cs.Classes.ToList();
                    if (nameClassCond != string.Empty)
                        clsList = clsList.Where(c => c.NameClass.Contains(nameClassCond)).ToList();
                    if (idClassCond != null && clsList.Count() != 0)
                        clsList = clsList.Where(s => s.IdClass == idClassCond).ToList();
                    if (clsList.Count() == 0)
                        Notification.PrintAsMessage("Cannot find any one !!");
                    else
                        TablePrinter.RightAlignPrint(clsList);
                    //foreach (var item in clsList)
                    //    Console.WriteLine($"{item.IdClass} - {item.NameClass}");
                }

                Console.ReadKey();
            }
            while (true);
        }


        /// <summary>
        ///     Searchs And Updates Class
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void SearchAndUpdateClass()
        {
            // Checks if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Classes.Count() == 0)
            {
                Notification.PrintAsMessage("There are no Class at all");
                return;
            }

            // Searches Class
            int idClass = ClassData.GetIdClass("Enter the Id of Class that you want to update");
            if (idClass == 0 || idClass == -1)
            {
                Notification.PrintAsMessage("You should come here next time !");
                return;
            }

            var tempClass = Asm_Cs.Classes.Where(c => c.IdClass == idClass).FirstOrDefault();
            if (tempClass == null)
            {
                Notification.PrintAsMessage("This IdClass does not exist on server");
                return;
            }

            // Make sure the User wants to update the data or not
            Console.WriteLine();
            TablePrinter.RightAlignPrint(new List<Class> { tempClass });
            Console.WriteLine();
            bool willUpdate = SystemData.GetYesNo("Do you want to update this Class Information");
            if (!willUpdate)
            {
                Notification.PrintAsRequest("Okay! See you soon");
                Console.ReadKey(true);
            }

            // Creates Update Menu and setting values
            DefaultMenu updateMenu = new DefaultMenu();
            updateMenu.SetValue(Program.icon, Menu.UpdateClass.title, Menu.UpdateClass.menu);
            updateMenu.SetCutLineIndexList(Menu.UpdateClass.cutLineIndexList);
            int choice = 1;
            bool isSave = true;

            // Creates default data
            string defaultNameClass = tempClass.NameClass;

            // Creates backup data
            string newNameClass = defaultNameClass;

            void PrintNewAndDefClass()
            {
                Console.WriteLine("Default Class Info:");
                TablePrinter.RightAlignPrint(new List<Class>() { new Class { IdClass = tempClass.IdClass, NameClass = defaultNameClass } });
                Console.WriteLine("New Class Info:");
                TablePrinter.RightAlignPrint(new List<Class>() { new Class { IdClass = tempClass.IdClass, NameClass = newNameClass } });
            }

            do
            {
                choice = updateMenu.Run(choice);
                Console.WriteLine();
                //Console.WriteLine($"New Class Info: {tempClass.IdClass} - {newNameClass}");

                // Quit
                if (choice == updateMenu.OptionQuit)
                {
                    if (!isSave)
                    {
                        Notification.PrintAsMessage("Looks like something wasn't saved");
                        isSave = SystemData.GetYesNo("Do you want to save");
                        if (isSave)
                        {
                            tempClass.NameClass = newNameClass;
                            Asm_Cs.SaveChanges();
                            Notification.PrintAsMessage("Saved successfully");
                        }
                        else
                            Notification.PrintAsMessage("All Information will not be saved");
                    }
                    Notification.PrintAsMessage("ByeBye And Update next time plz");
                    break;
                }

                // Sets new NameClass
                if (choice == 1)
                {
                    do
                    {
                        string tempNewNameClass = ClassData.GetNameClass("Enter the new Name Class");
                        if (tempNewNameClass.Length == 0)
                        {
                            Notification.PrintAsMessage("The NameClass will not be changed");
                            break;
                        }
                        newNameClass = tempNewNameClass;
                        // Checks if the NameClass is unique - not necessary
                        if (Asm_Cs.Classes.Count(c => c.NameClass == newNameClass) == 0)
                        {
                            PrintNewAndDefClass();
                            Notification.PrintAsMessage("New NameClass Seted! Dont forgot to Save the data");
                            isSave = false;
                            break;
                        }
                        else
                        {
                            Notification.PrintAsError("This Class Name already exists");
                        } 
                    }
                    while (true);
                }

                if (choice == 2)
                {
                    PrintNewAndDefClass();
                }

                // Resets to default
                if (choice == 3)
                {
                    newNameClass = defaultNameClass;
                    isSave = true;
                    Notification.PrintAsMessage("Reseted all Values to default successfully");
                }

                // Saves Changes
                if (choice == 4)
                {
                    tempClass.NameClass = newNameClass;
                    Asm_Cs.SaveChanges();
                    isSave = true;
                    Notification.PrintAsMessage("Saved successfully");
                }

                Console.ReadKey(true);
            }
            while (true);
        }

        #endregion

    }
}
