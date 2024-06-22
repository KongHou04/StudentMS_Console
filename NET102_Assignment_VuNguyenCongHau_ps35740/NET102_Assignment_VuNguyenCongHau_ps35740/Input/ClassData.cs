using MessagePrinter;
using System;

namespace NET102_Assignment_VuNguyenCongHau_ps35740.Input
{
    public static class ClassData
    {
        /// <summary>
        ///     Gets a Id Class entered by user
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        ///     Int - a Class Id
        /// </returns>
        public static int GetIdClass(string req)
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
                    Notification.PrintAsError("This Class Id is illegal");
                    continue;
                }
            }
            while (true);
        }


        /// <summary>
        ///     Gets a Class Name entered by user
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        ///     String - a Class Name
        /// </returns>
        public static string GetNameClass(string req)
        {
            string input;
            do
            {
                Notification.PrintAsRequest(req);
                input = Console.ReadLine();
                try
                {
                    if (input.Replace(" ", string.Empty).Length == 0)
                        throw new Exception();
                    if (input.Length < 50)
                        return input;
                }
                catch 
                {
                    Notification.PrintAsError("This Class Name is illegal");
                }
               
            }
            while (true);
        }

    }
}
