using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePrinter;
using Microsoft.SqlServer.Server;

namespace NET102_Assignment_VuNguyenCongHau_ps35740.Input
{
    internal static class StudentData
    {

        #region Field

        // Invalid characters in email
        private static char[] _invalidchars = { '!', '#', '$','%', '^', '&', '*', '(', ')', '-', '_'
                                            , '=', '+', '{', '[',']', '}', '|', '\\', ';', ':', '\'', '"'
                                            , ',', '<', '>', '/', '?'};
        // Ivalid numbers in name
        private static char[] _invalidnums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        #endregion



        #region Method

        /// <summary>
        ///     Gets a Id entered by user
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        ///     Int - a Student Id
        /// </returns>
        public static int GetStId(string req)
        {
            string id = string.Empty;
            int result = new int();
            do
            {
                Notification.PrintAsRequest(req);
                try
                {
                    id = Console.ReadLine();
                    result = Convert.ToInt16(id);
                    if (result < -1)
                        throw new Exception();
                    return result;
                }
                catch 
                {
                    Notification.PrintAsError("This Student Id is illegal");
                    continue;
                }
                
            }
            while (true);
        }


        /// <summary>
        ///     Gets a Name entered by the user
        /// </summary>
        /// <returns>
        ///     String - a Student Name
        /// </returns>
        public static string GetName(string req)
        {
            string input;
            do
            {
                Notification.PrintAsRequest(req);
                input = Console.ReadLine();
                if (input.Replace(" ", string.Empty).Length == 0) input = null;
                else if (input.Length > 50) input = null;
                else if (input.IndexOfAny(_invalidchars) >= 0) input = null;
                else if (input.Contains('@')) input = null;
                else if (input.IndexOfAny(_invalidnums) >= 0) input = null;
                else if (input == null)
                    Notification.PrintAsError("This Student Name is illegal");
            }
            while (input == null);
            return input;
        }


        /// <summary>
        ///     Gets a Mark (0 < mark < 10) entered bt the user
        /// </summary>
        /// <returns>
        ///     Double - a Student Mark
        /// </returns>
        public static float GetMark(string req)
        {
            float input;
            do
            {
                Notification.PrintAsRequest(req);
                // Checks Mark
                try
                {
                    input = float.Parse(Console.ReadLine());
                    if (input < -1 || input > 10)
                        throw new Exception();
                    return input;
                }
                catch
                {
                    Notification.PrintAsError("This Student Mark is illegal");
                    continue;
                }
            }
            while (true);
        }


        /// <summary>
        ///     Gets a Email address (follow rules of google) entered by the user
        /// </summary>
        /// <returns>
        ///     String - a Student Email
        /// </returns>
        public static string GetEmail(string req)
        {
            string input;
            do
            {
                Notification.PrintAsRequest(req);
                input = Console.ReadLine();
                int length = input.Length;
                int indexOfAt = input.IndexOf('@');
                int indexofDot = input.IndexOf('.');
                // Checks Email string
                if (length == 0) return input;
                if (input.Length > 100) input = null;
                else if (input.Contains(' ') == true) input = null;
                else if (input.ToCharArray().Count(c => c == '@') > 1) input = null;
                else if (indexOfAt == -1 || indexOfAt < 5 || indexOfAt > length - 5) input = null;
                else if (indexofDot == -1 || indexofDot == 1 || indexofDot == length - 1) input = null;
                else if (input.IndexOfAny(_invalidchars) != -1) input = null;
                else if (input.IndexOf("..") != -1) input = null;
                else if (input.IndexOf(".@") != -1 || input.IndexOf("@.") != -1) input = null;
                else if (input.Substring(input.IndexOf('@')).IndexOf('.') == 1) input = null;
                else if (input.Substring(input.IndexOf('@')).ToArray().Count(x => x == '.') < 1) input = null;
                if (input == null)
                    Notification.PrintAsError("This Student Mail is illegal");
            }
            while (input == null);
            return input;
        }


        /// <summary>
        ///     Gets a Rank entered by the user
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        ///     String - a Student Rank
        /// </returns>
        public static string GetRank(string req)
        {
            string[] ranks = { "excellent", "verygood", "good", "average", "weak", "poor" };
            string input;
            do
            {
                Notification.PrintAsRequest(req);
                input = Console.ReadLine();
                if (input.Length == 0) return input;
                if (ranks.Contains(input.ToLower()))
                    return input;
                Notification.PrintAsError("That Student Rank is illegal");
            }
            while (true);
        }


        /// <summary>
        ///     Gets a Id Class entered by user
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        ///     Int - a Id Class
        /// </returns>
        public static int GetIdClass(string req)
        {
            return ClassData.GetIdClass(req);
        }

        #endregion

    }
}
