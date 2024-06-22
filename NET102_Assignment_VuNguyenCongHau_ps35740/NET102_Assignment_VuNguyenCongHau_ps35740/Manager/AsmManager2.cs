using MessagePrinter;
using NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject.TableObj;
using NET102_Assignment_VuNguyenCongHau_ps35740.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VirtualGUIMenu;

namespace NET102_Assignment_VuNguyenCongHau_ps35740.Manager
{
    public partial class AsmManager
    {

        #region Student Methods

        /// <summary>
        ///     Function - Adds new Student
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void AddNewStudent()
        {
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Classes.Count() == 0)
            {
                Notification.PrintAsMessage("No Class exists yet, please add Class list first");
                return;
            }

            Notification.PrintAsMessage("This is Class List");
            TablePrinter.RightAlignPrint(Asm_Cs.Classes.ToList());

            // Gets number of Students user wanna add to the Database
            int n = SystemData.GetNumber("Enter the number of Students you wanna add", 10, 0);
            Console.WriteLine();

            // Returns if n == 0
            if (n == 0)
            {
                Notification.PrintAsMessage("See you layder! |>_<|");
                return;
            };

            // Create new Students Information
            Console.WriteLine("----------------------------------------------");
            for (int i = 0; i < n; i++)
            {
                Notification.PrintAsMessage($"Input Student {Asm_Cs.Students.Count() + 1} information");
                Console.WriteLine();

                // Enters new Student information
                Student std = new Student();

                // Sets Name
                std.Name = StudentData.GetName("Enter name");
                if (std.Name.Length == 0)
                {
                    Notification.PrintAsMessage("You hate names!!");
                    return;
                };

                // Sets Mark
                std.Mark = StudentData.GetMark("Enter mark");
                if (std.Mark == -1)
                {
                    Notification.PrintAsMessage("Common Man :(");
                    return;
                };

                // Sets Email
                std.Email = StudentData.GetEmail("Enter Email");
                if (std.Email.Length == 0)
                {
                    Notification.PrintAsMessage("Byee !");
                    return;
                };

                // Sets IdClass
                do
                {
                    std.IdClass = StudentData.GetIdClass("Enter Id Class");
                    if (std.IdClass == -1 || std.IdClass == 0)
                    {
                        Notification.PrintAsMessage("Get outtt of here :X");
                        return;
                    }
                    // Checks if the Id Class exists 
                    if (Asm_Cs.Classes.Where(c => c.IdClass == std.IdClass).Count() > 0)
                        break;
                    Notification.PrintAsError("This Class Id does not exists");
                }
                while (true);

                // Adds and Saves Changes
                Asm_Cs.Students.Add(std);
                try
                {
                    Asm_Cs.SaveChanges();
                    Notification.PrintAsMessage("Add New Student successfully");
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
        ///     Print Student List
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PrintStudentList()
        {
            // Check if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
            {
                Notification.PrintAsMessage("There are no Student at all");
                return;
            }
            TablePrinter.RightAlignPrint(Asm_Cs.Students.ToList());
            Console.WriteLine();
            Notification.PrintAsMessage("Press any key to go back...");
        }


        /// <summary>
        ///     Prints the Top 5 Student List
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PrintTop5List()
        {
            // Check if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");

            List<Student> top5List = Asm_Cs.Students.OrderByDescending(s => s.Mark).ThenBy(s => s.StId).ToList();
            if (top5List.Count() <= 5)
                foreach (var item in top5List)
                {
                    Console.WriteLine($"{item.StId} - {item.Name} - {item.Mark} - {item.Email} - {item.IdClass}");
                }
            else
            {
                top5List = top5List.Take(5).ToList();
                TablePrinter.RightAlignPrint(top5List);
                Console.WriteLine();
                Notification.PrintAsMessage("Press eany key to continue...");
            }
        }


        public void PrintStudentInEveryClass()
        {
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Classes.Count() == 0)
                Notification.PrintAsMessage("No Class exists yet, please add Class list first");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");
            var tempClsList = Asm_Cs.Classes.ToList();
            foreach (var item in tempClsList)
            {
                Notification.PrintAsMessage(item.NameClass);
                List<Student> tempList = Asm_Cs.Students.Where(s => s.IdClass == item.IdClass).ToList();
                if (tempList.Count() == 0)
                {
                    Notification.PrintAsMessage("No Student at all\n");
                    continue;
                }
                TablePrinter.RightAlignPrint(tempList);
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to get out of here!!..");
        }




        /// <summary>
        ///     Prints the Excellent Student List
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PrintExcellentList()
        {
            // Check if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");

            List<Student> excellentList = Asm_Cs.Students.Where(s => s.Mark >= 9).ToList();
            if (excellentList.Count() == 0)
                Notification.PrintAsMessage("No excellent Student at all");
            else
            {
                TablePrinter.RightAlignPrint(excellentList);
                Console.WriteLine();
                Notification.PrintAsMessage("Press eany key to go back...");
            }
        }


        /// <summary>
        ///     Prints the Above Agerage Student List
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PrintAboveAvgList()
        {
            // Check if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");

            List<Student> aboveAvgList = Asm_Cs.Students.Where(s => s.Mark >= 5).ToList();
            if (aboveAvgList.Count() == 0)
                Notification.PrintAsMessage("No excellent Student at all");
            else
            {
                TablePrinter.RightAlignPrint(aboveAvgList);
                Console.WriteLine();
                Notification.PrintAsMessage("Press eany key to go back...");
            }

        }


        /// <summary>
        ///     Prints the Below Average Student List
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PrintBelowAvgList()
        {
            // Check if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");

            List<Student> belowAvgList = Asm_Cs.Students.Where(s => s.Mark < 5).ToList();
            if (belowAvgList.Count() == 0)
                Notification.PrintAsMessage("No excellent Student at all");
            else
            {
                TablePrinter.RightAlignPrint(belowAvgList);
                Console.WriteLine();
                Notification.PrintAsMessage("Press any key to continue...");
            }
                
        }


        /// <summary>
        ///     Search Student using custom conditions
        /// </summary>
        public void SearchStudent()
        {
            // Checks if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");

            

            // Creates Search Menu
            DefaultMenu searchMenu = new DefaultMenu();
            searchMenu.SetCutLineIndexList(Menu.SearchStudentMenu.cutLineIndexList);
            string option1 = Menu.SearchStudentMenu.menu[0];
            string option2 = Menu.SearchStudentMenu.menu[1];
            string option3 = Menu.SearchStudentMenu.menu[2];
            string option4 = Menu.SearchStudentMenu.menu[3];
            string option5 = Menu.SearchStudentMenu.menu[4];
            string[] menu = Menu.SearchStudentMenu.menu;
            searchMenu.SetFlexibleValues(43, 2);


            // Creates conditions
            int? stIdCond = new int?();
            string nameCond = string.Empty;
            double? mark1Cond = null;
            double? mark2Cond = null;
            string emailCond = string.Empty;
            int? idClassCond = new int?();

            int choice = 1;
            do
            {
                // Reset the Menu options
                menu[0] = option1 + ((stIdCond == null) ? "" : $" - {stIdCond}");
                menu[1] = option2 + ((nameCond == string.Empty) ? "" : $" - %{nameCond}%");
                menu[2] = option3 + ((mark1Cond == null) ? "" : $" - {mark1Cond} to {mark2Cond}");
                menu[3] = option4 + ((emailCond == string.Empty) ? "" : $" - %{emailCond}%");
                menu[4] = option5 + ((idClassCond == null) ? "" : $" - {idClassCond}");
                searchMenu.SetValue(Program.icon, Menu.SearchStudentMenu.title, menu);

                choice = searchMenu.Run(choice);
                Console.WriteLine();

                // Quit
                if (choice == searchMenu.OptionQuit)
                {
                    Notification.PrintAsMessage("Dont go backkk! O_o");
                    break;
                }

                #region Sets Conditions
                if (choice == 1)
                {
                    int tempStIdCond = StudentData.GetIdClass("Enter the StId you wanna look for");
                    if (tempStIdCond == 0 || tempStIdCond == -1)
                    {
                        Notification.PrintAsMessage("The Condition will not be changed");
                        Console.ReadKey();
                        continue;
                    }
                    stIdCond = tempStIdCond;
                    Notification.PrintAsMessage("Set new StId Condition successfully");
                }

                if (choice == 2)
                {
                    string tempNameCond = StudentData.GetName("Enter a string of Name you wanna look for");
                    if (tempNameCond.Length == 0)
                    {
                        Notification.PrintAsMessage("The Condition will not be changed");
                        Console.ReadKey();
                        continue;
                    }
                    nameCond = tempNameCond;
                    Notification.PrintAsMessage("Set new Name Condition successfully");
                }

                if (choice == 3)
                {
                    try
                    {
                        double tempMark1Cond = StudentData.GetMark("Enter the first mark");
                        if (tempMark1Cond == -1)
                            throw new Exception();
                        double tempMark2Cond = StudentData.GetMark("Enter the second mark");
                        if (tempMark2Cond == -1)
                            throw new Exception();
                        mark1Cond = tempMark1Cond;
                        mark2Cond = tempMark2Cond;
                        Notification.PrintAsMessage("Set new Mark Condition successfully");
                    }
                    catch
                    {
                        Notification.PrintAsMessage("The Condition will not be changed");
                        Console.ReadKey();
                        continue;
                    }
                }

                if (choice == 4)
                {
                    Notification.PrintAsRequest("Enter a string of Email you wanna look for");
                    string tempEmailCond = Console.ReadLine();
                    if (tempEmailCond.Length == 0)
                    {
                        Notification.PrintAsMessage("The Condition will not be changed");
                        Console.ReadKey();
                        continue;
                    }
                    emailCond = tempEmailCond;
                    Notification.PrintAsMessage("Set new Email Condition successfully");
                }

                if (choice == 5)
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
                #endregion

                if (choice == 6)
                {
                    Console.Write("Conditions: ");
                    Console.Write("{0} - ", (stIdCond == null) ? "default" : stIdCond.ToString());
                    Console.Write("{0} - ", (nameCond == string.Empty) ? "default" : $"%{nameCond}%");
                    Console.Write("{0} - ", (mark1Cond == null) ? "default" : $"{mark1Cond} to {mark2Cond}");
                    Console.Write("{0} - ", (emailCond == string.Empty) ? "default" : $"%{emailCond}%");
                    Console.Write("{0}\n", (idClassCond == null) ? "default" : idClassCond.ToString());
                }

                if (choice == 7)
                {
                    stIdCond = new int?();
                    nameCond = string.Empty;
                    mark1Cond = new double?();
                    mark2Cond = new double?();
                    emailCond = string.Empty;
                    idClassCond = new int?();
                    Notification.PrintAsMessage("Removed Conditions successfully");
                }

                if (choice == 8)
                {
                    List<Student> stdList = Asm_Cs.Students.ToList();
                    if (nameCond != string.Empty)
                        stdList = stdList.Where(s => s.Name.Contains(nameCond)).ToList();
                    if (emailCond != string.Empty && stdList.Count() != 0)
                        stdList = stdList.Where(s => s.Email.Contains(emailCond)).ToList();
                    if (mark1Cond != null && stdList.Count() != 0)
                        stdList = stdList.Where(s =>
                        (s.Mark <= mark1Cond && s.Mark >= mark2Cond) || (s.Mark <= mark2Cond && s.Mark >= mark1Cond)).ToList();
                    if (idClassCond != null && stdList.Count() != 0)
                        stdList = stdList.Where(s => s.IdClass == idClassCond).ToList();
                    if (stIdCond != null && stdList.Count() != 0)
                        stdList = stdList.Where(s => s.StId == stIdCond).ToList();
                    if (stdList.Count() == 0)
                        Notification.PrintAsMessage("Cannot find any one !!");
                    else
                    {
                        TablePrinter.RightAlignPrint(stdList);
                        Console.WriteLine();
                        Notification.PrintAsMessage("Press any key to go back !");
                    }
                }

                Console.ReadKey();
            }
            while (true);
        }


        /// <summary>
        ///     Searchs And Updates Student
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void SearchAndUpdateStudent()
        {
            // Checks if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");


            int StId = StudentData.GetStId("Enter the Id of Student that you want to update");
            if (StId == -1 || StId == 0)
            {
                Notification.PrintAsMessage("See you next time");
                return;
            }    

            // Searches Student
            var tempStd = Asm_Cs.Students.Where(s => s.StId == StId).FirstOrDefault();
            if (tempStd == null)
            {
                Notification.PrintAsMessage("This StId does not exist on server");
                return;
            }

            // Makes sure the User wants to update the data or not
            Console.WriteLine(5);
            TablePrinter.RightAlignPrint(new List<Student> (){ tempStd });
            Console.WriteLine();
            bool willUpdate = SystemData.GetYesNo("Do you want to update this Student Information");
            if (!willUpdate)
            {
                Notification.PrintAsRequest("Okay! See you soon");
                Console.ReadKey(true);
            }

            // Creates the Update Menu and setting values
            DefaultMenu updateMenu = new DefaultMenu();
            updateMenu.SetValue(Program.icon, Menu.UpdateStudent.title, Menu.UpdateStudent.menu);
            updateMenu.SetCutLineIndexList(Menu.UpdateStudent.cutLineIndexList);
            int choice = 1;
            bool isSave = true;

            // Creates default data
            string defaultName = tempStd.Name;
            double defaultMark = tempStd.Mark;
            string defaultEmail = tempStd.Email;
            int defaultIdClass = tempStd.IdClass;

            // Creates backup data
            string newName = defaultName;
            double newMark = defaultMark;
            string newEmail = defaultEmail;
            int newIdClass = defaultIdClass;

            void PrintNewAndDefStudent()
            {
                Console.WriteLine("Default Student Info:");
                TablePrinter.RightAlignPrint(new List<Student>() { new Student()
                    {
                        StId = tempStd.StId, Name = defaultName, Mark = defaultMark, Email= defaultEmail, IdClass= defaultIdClass
                    }});
                Console.WriteLine("New Student Info: ");
                TablePrinter.RightAlignPrint(new List<Student>() { new Student()
                    {
                        StId = tempStd.StId, Name = newName, Mark = newMark, Email= newEmail, IdClass= newIdClass
                    }});
            }

            do
            {
                choice = updateMenu.Run(choice);
                Console.WriteLine();
                //Console.WriteLine($"New Class Info: {tempStd.StId} - {newName} - {newMark} - {newEmail} - {newIdClass}");

                // Quit
                if (choice == updateMenu.OptionQuit)
                {
                    if (!isSave)
                    {
                        Notification.PrintAsMessage("Looks like something wasn't saved");
                        isSave = SystemData.GetYesNo("Do you want to save");
                        if (isSave)
                        {
                            tempStd.Name = newName;
                            tempStd.Mark = newMark;
                            tempStd.Email = newEmail;
                            tempStd.IdClass = newIdClass;
                            Asm_Cs.SaveChanges();
                            Notification.PrintAsMessage("Saved successfully");
                        }
                        else
                            Notification.PrintAsMessage("All Information will not be saved");
                    }
                    Notification.PrintAsMessage("ByeBye And Update next time plz");
                    break;
                }

                // Sets new Name
                if (choice == 1)
                {
                    string tempNewName = StudentData.GetName("Enter the new Name");
                    if (tempNewName.Length == 0)
                    {
                        Notification.PrintAsMessage("Looks like you dont wanna change name");
                        Console.ReadKey();
                        continue;
                    }
                    newName = tempNewName;
                    PrintNewAndDefStudent();
                    Notification.PrintAsMessage("New Name Seted! Dont forgot to Save the data");
                    isSave = false;
                }

                // Sets new Mark
                if (choice == 2)
                {
                    double tempNewMark = StudentData.GetMark("Enter the new Mark");
                    if (tempNewMark == -1)
                    {
                        Notification.PrintAsMessage("This mark sus! I will not save it");
                        Console.ReadKey();
                        continue;
                    }
                    newMark = tempNewMark;
                    PrintNewAndDefStudent();
                    Notification.PrintAsMessage("New Mark Seted! Dont forgot to Save the data");
                    isSave = false;
                }

                // Sets new Email
                if (choice == 3)
                {
                    string tempNewEmail = StudentData.GetEmail("Enter the new Email");
                    if (tempNewEmail.Length == 0)
                    {
                        Notification.PrintAsMessage("I'll keep the old Email");
                        Console.ReadKey();
                        continue;
                    }    
                    newEmail = tempNewEmail;
                    PrintNewAndDefStudent();
                    Notification.PrintAsMessage("New Email Seted! Dont forgot to Save the data");
                    isSave = false;
                }

                // Sets New IdClass
                if (choice == 4)
                {
                    do
                    {
                        int tempNewIdClass = StudentData.GetIdClass("Enter the new IdClass");
                        if (tempNewIdClass == 0 || tempNewIdClass == -1)
                        {
                            Notification.PrintAsMessage("The IdClass will not be changed");
                            break;
                        }
                        else
                        {
                            newIdClass = tempNewIdClass;

                            // Checks if the Id Class exists 
                            if (Asm_Cs.Classes.Any(c => c.IdClass == newIdClass))
                            {
                                PrintNewAndDefStudent();
                                Notification.PrintAsMessage("New IdClass Seted! Dont forgot to Save the data");
                                isSave = false;
                                break;
                            }
                            Notification.PrintAsError("This IdClass does not exist");
                        }
                    }
                    while (true);
                    
                }

                if (choice == 5)
                {
                    PrintNewAndDefStudent();
                }

                // Resets values to default
                if (choice == 6)
                {
                    newName = defaultName;
                    newMark = defaultMark;
                    newEmail = defaultEmail;
                    newIdClass = defaultIdClass;
                    tempStd.Name = defaultName;
                    tempStd.Mark = defaultMark;
                    tempStd.Email = defaultEmail;
                    tempStd.IdClass = defaultIdClass;
                    isSave = true;
                    Notification.PrintAsMessage("Reseted all Values to default successfully");
                }

                // Saves Changes
                if (choice == 7)
                {
                    tempStd.Name = newName;
                    tempStd.Mark = newMark;
                    tempStd.Email = newEmail;
                    tempStd.IdClass = newIdClass;
                    Asm_Cs.SaveChanges();
                    isSave = true;
                    Notification.PrintAsMessage("Saved successfully");
                }
                Console.ReadKey();
            }
            while (true);
        }


        /// <summary>
        ///     Sorts and Prints Student List
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void SortAndPrintStudentList()
        {
            // Checks if the Database has data
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Students.Count() == 0)
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");

            // Create Sort Menu and setting values
            SettingMenu sortMenu = new SettingMenu();
            int[] valueSettingList = { 0, 0, 0, 0, 0 };
            sortMenu.SetValue(Program.icon, Menu.SortMenu.title, Menu.SortMenu.propertyNameList,
                Menu.SortMenu.buttonList, valueSettingList, Menu.SortMenu.valueList);
            int choice = 1;
            List<Student> tempList = Asm_Cs.Students.ToList();

            do
            {
                choice = sortMenu.Run(choice);
                Console.WriteLine();

                if (choice == sortMenu.OptionQuit)
                {
                    Notification.PrintAsMessage("Don't let them mess up X<X");
                    break;
                }

                if (choice == 6)
                {
                    sortMenu.ResetValue();
                    Notification.PrintAsMessage("Reseted values successfully");
                }

                if (choice == 7)
                {
                    valueSettingList = sortMenu.ValueSettingList;
                    if (valueSettingList[0] == 1)
                        tempList = tempList.OrderByDescending(s => s.StId).ToList();
                    else
                        tempList = tempList.OrderBy(s => s.StId).ToList();

                    for (int i = 1; i < valueSettingList.Count(); i++)
                    {
                        if (valueSettingList[i] == 0) continue;

                        if (i == 1)
                            tempList = tempList.OrderBy(x => x.Name).ToList();
                        if (i == 2)
                            tempList = tempList.OrderBy(x => x.Mark).ToList();
                        if (i == 3)
                            tempList = tempList.OrderBy(x => x.Email).ToList();
                        if (i == 4)
                            tempList = tempList.OrderBy(x => x.IdClass).ToList();

                        if (valueSettingList[i] == 2)
                            tempList.Reverse();
                    }
                    TablePrinter.RightAlignPrint(tempList);
                    Console.WriteLine();
                    Notification.PrintAsMessage("Press any Key to countinue...");
                }
                Console.ReadKey();
            }
            while (true);


        }


        public void CreateAvgMarkFile()
        {
            if (Asm_Cs == null)
                throw new Exception("The Field Asm_Cs cannot be null");
            if (Asm_Cs.Classes.Count() == 0)
            {
                Notification.PrintAsMessage("No Class exists yet, please add Class list first");
                return;
            }
            if (Asm_Cs.Students.Count() == 0)
            {
                Notification.PrintAsMessage("No Student exists yet, please add Student list first");
                return;
            }

            string path = @"..\..\..\..\AvgMark\AvgMark.txt";
            List<string> content = new List<string>();
            for (int i = 0; i < Asm_Cs.Classes.Count(); i++)
            {
                Class cls = Asm_Cs.Classes.Where(c => c.IdClass == i+1).FirstOrDefault();
                double avgMark = new double();
                var tempStdList = Asm_Cs.Students.Where(c => c.IdClass == cls.IdClass);
                if (tempStdList.Count() > 0)
                    avgMark = tempStdList.Average(c => c.Mark);
                string row = cls.IdClass + "-" + cls.NameClass + "-" + avgMark;
                content.Add(row);
            }
            
            Thread thr = new Thread(x =>
            {
                File.WriteAllLines(path, content);
            });
            thr.Start();
            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine($"{i}s remaining...");
                Thread.Sleep(1000);
            }
            Notification.PrintAsMessage("Created Avg_Mark.txt file successfully");
            
        }

        #endregion

    }
}
