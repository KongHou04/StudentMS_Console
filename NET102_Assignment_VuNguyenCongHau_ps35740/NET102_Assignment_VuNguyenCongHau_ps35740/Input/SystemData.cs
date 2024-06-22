using MessagePrinter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace NET102_Assignment_VuNguyenCongHau_ps35740.Input
{
    internal class SystemData
    {
        #region Method
        /// <summary>
        ///     Input a string
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        ///     A string from user
        /// </returns>
        public static string GetString(string req)
        {
            Notification.PrintAsRequest(req);
            string str = Console.ReadLine();
            return str;
        }

        /// <summary>
        ///     Input Yes or No
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        ///     True if Yes, False if No
        /// </returns>
        public static bool GetYesNo(string req)
        {
            req = req + "? (Yes/No)";
            Notification.PrintAsRequest(req);
            switch (Console.ReadLine().ToLower())
            {
                case "0":
                case "nerver":
                case "n":
                case "N":
                case "no":
                case "nope":
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        ///     Input a number
        /// </summary>
        /// <param name="req"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns>
        ///     Int - available number
        /// </returns>
        public static int GetNumber(string req, int max, int min)
        {
            int? input = new int?();
            do
            {
                Notification.PrintAsRequest(req);
                try
                {
                    input = Convert.ToInt16(Console.ReadLine());
                    if (input < min || input > max) input = null;
                }
                catch
                {
                    input = null;
                }
                if (input == null)
                    Notification.PrintAsError("Please input correctly numbers");
            }
            while (input == null);
            return (int)input;
        }

        #endregion
    }
}
