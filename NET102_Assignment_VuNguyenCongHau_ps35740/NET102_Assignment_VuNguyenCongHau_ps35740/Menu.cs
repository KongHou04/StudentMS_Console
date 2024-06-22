using System;
using System.Net.NetworkInformation;

namespace NET102_Assignment_VuNguyenCongHau_ps35740
{
    /// <summary>
    ///     This class stores data from all Menus
    /// </summary>
    /// 

    internal class Menu
    {

        #region Differrent Type Menus
        public static class LoginMenu
        {
            public static string title = "Login Menu";
            public static string[] menu =
            {
                "Windows Authentication",
                "SQL Server Authentication",
                "Exit"
            };
            
        }

        public static class ManagerMenu
        {
            public static string title = "Manager Menu";
            public static string[] menu =
            {
                "Sql Server",
                "Assignment Asm_C#2 ",
                "Log out"
            };
        }

        public static class DbManageMenu
        {
            public static string title = "Database Asm_C#2 Manage Menu";
            public static string[] menu =
            {
                "Create new Database Asm_C#2",
                "Drop Database Asm_C#2 ",
                "Check if Asm_C#2 Database exists",
                "Create Backup",
                "Restore Backup",
                "Go back "
            };
        }

        public static class AsmManageMenu
        {
            public static string title = "Assignment Manage Menu";
            public static string[] menu =
            {
                "Add Student/Class",
                "Print Student/Class List",
                "Print Student List - Advanced",
                "Search Student/Class",
                "Update Student/Class - Using ID",
                "Sort and Print Student List - Advanced",
                "Create Average Mark File",
                "Go Back"
            };
        }

        #endregion



        #region Option 1 Menu
        public static class AsmOption1Menu
        {
            public static string title = "Add Class/Student";
            public static string[] menu =
            {
                "Add new Student",
                "Add new Class",
                "Back"
            };
        }
        #endregion



        #region Option 2 Menu
        public static class AsmOption2Menu
        {
            public static string title = "Print Student/Class List";
            public static string[] menu =
            {
                "Print Student List",
                "Print Class List",
                "Go Back"
            };
        }
        #endregion



        #region Option 3 Menus
        public static class AsmOption3Menu
        {
            public static string title = "Print Student List - Advanced";
            public static string[] menu =
            {
                "Top 5 Students",
                "List of Student in every Class",
                "List of Excellent (>= 9)",
                "List of Above Average (>= 5)",
                "List of Below Average/Weak, Poor (< 5)",
                "Back"
            };
        }
        #endregion



        #region Option 4 Menus

        public static class AsmOption4Menu
        {
            public static string title = "Search Student/Class";
            public static string[] menu =
            {
                "Search Student",
                "Search Class",
                "Back"
            };
        }

        public static class SearchStudentMenu
        {
            public static string title = "Search Student";
            public static string[] menu =
            {
                "Set StId condition",
                "Set Name condition",
                "Set Mark condition",
                "Set Email condition",
                "Set IdClass condition",
                "Show conditions",
                "Remove conditions",
                "Search Student List",
                "Back"
            };
            public static int[] cutLineIndexList = { 4 };
        }

        public static class SearchClassMenu
        {
            public static string title = "Search Class";
            public static string[] menu =
            {
                "Set IdClass condition",
                "Set NameClass condition",
                "Show conditions",
                "Remove conditions",
                "Search Class List",
                "Back",
                //"Remove conditions",
                //"Search Class List",
                //"Back",
                //"Remove conditions",
                //"Search Class List",
                //"Back"
            };
            public static int[] cutLineIndexList = { 1 };
        }

        #endregion



        #region Option 5 Menus

        public static class AsmOption5Menu
        {
            public static string title = "Add Class/Student";
            public static string[] menu =
            {
                "Update Student",
                "Update Class",
                "Back"
            };
        }

        public static class UpdateStudent
        {
            public static string title = "Update Student";
            public static string[] menu =
            {
                "Set new Name",
                "Set new Mark",
                "Set new Email",
                "Set new IdClass",
                "Show Default/New Student Information",
                "Reset to Default",
                "Save Changes",
                "Back"
            };
            public static int[] cutLineIndexList = { 3 };
        }

        public static class UpdateClass
        {
            public static string title = "Update Class";
            public static string[] menu =
            {
                "Set new NameClass",
                "Show Default/New Class Information",
                "Reset to Default",
                "Save Changes",
                "Back"
            };
            public static int[] cutLineIndexList = { 0 };
        }

        #endregion




        public static class SortMenu
        {
            public static string title = "Sort Student List";
            public static string[] propertyNameList =
            {
                "Id",
                "Name",
                "Mark",
                "Email",
                "IdClass"
            };
            public static string[][] valueList =
            {
                new string[] {"A-Z", "Z-A"},
                new string[] {"Off", "A-Z", "Z-A"},
                new string[] {"Off", "A-Z", "Z-A"},
                new string[] {"Off", "A-Z", "Z-A"},
                new string[] {"Off", "A-Z", "Z-A"},
            };
            public static string[] buttonList =
            {
                "Reset Values",
                "Print Student List",
                "Back"
            };
        }


    }
}
