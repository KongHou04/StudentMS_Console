using System;
using System.Collections.Generic;
using System.Linq;
using MessagePrinter;
using NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject.TableObj;

namespace NET102_Assignment_VuNguyenCongHau_ps35740
{
    delegate string StringCreater(string s, int n, int space); 
    internal class TablePrinter
    {
        #region Properties
        //├ ┤ │ ─	┌ ┬ ┐ └ ┴ ┘
        public static ConsoleColor EvenCellColor = ConsoleColor.Cyan;
        public static ConsoleColor OddCellColor = ConsoleColor.Yellow;
        public static StringCreater CreateMyStr { get; set; }
        #endregion



        #region Methods

        /// <summary>
        ///     Sets rank for Student List
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        public static string SetRank(double mark)
        {
            return (mark >= 9) ? "Excellent" : (mark >= 7.5) ? "Very Good" : (mark >= 6.5) ? "Good" : (mark >= 5) ? "Avergare" : (mark >= 3) ? "Weak" : "Poor";
        }


        /// <summary>
        ///     Creates left align String
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="leftSpace"></param>
        /// <returns></returns>
        public static string CreateLeftAlignString(string str, int maxLength, int leftSpace)
        {
            string newStr = string.Empty;
            int length1 =  maxLength - str.Length - leftSpace;
            for (int i = 0; i < leftSpace; i++)
                newStr += ' ';
            newStr += str;
            for (int i = 0; i < length1; i++)
                newStr += ' ';
            return newStr;
        }


        /// <summary>
        ///     Creates right align String
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="rightSpace"></param>
        /// <returns></returns>
        public static string CreateRightAlignString(string str, int maxLength, int rightSpace)
        {
            string newStr = string.Empty;
            int length1 = maxLength - str.Length - rightSpace;
            for (int i = 0; i < length1; i++)
                newStr += ' ';
            newStr += str;
            for (int i = 0; i < rightSpace; i++)
                newStr += ' ';
            return newStr;
        }


        /// <summary>
        ///     Creates center align String
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string CreateCenterAlignString(string str, int maxLength, int nullSpace)
        {
            string newStr = string.Empty;
            string spaceStr = string.Empty;
            double space = (double)(maxLength - str.Length) / 2;
            if (space == (int)space)
            {
                for (int i = 0; i < space; i++)
                {
                    spaceStr += ' ';
                }
                return newStr = spaceStr + str + spaceStr;
            }
            else
            {
                for (int i = 0; i < space - 0.5; i++)
                {
                    spaceStr += ' ';
                }
                return newStr = ' ' + spaceStr + str + spaceStr ;
            }
        }


        /// <summary>
        ///     Right Align Print - Student Table
        /// </summary>
        /// <param name="stdList"></param>
        public static void RightAlignPrint(List<Student> stdList)
        {
            #region Setting Values
            string column1Name = "StId";
            string column2Name = "Name";
            string column3Name = "Mark";
            string column4Name = "Email";
            string column5Name = "IdClass";
            string column6Name = "Rank";
            string starLine = string.Empty;
            string cutLine = string.Empty;
            string endLine = string.Empty;

            List<string> rankList = stdList.Select(s => SetRank(s.Mark)).ToList();
            int optionLength = stdList.Count();
            // Changes space here
            int maxL_StId = Math.Max(stdList.Max(s => s.StId.ToString().Length), column1Name.Length) + 3;
            int maxL_Name = Math.Max(stdList.Max(s => s.Name.Length), column2Name.Length) + 4;
            int maxL_Mark = Math.Max(stdList.Max(s => s.Mark.ToString().Length), column3Name.Length) + 3;
            int maxL_Email = Math.Max(stdList.Max(s => s.Email.Length), column4Name.Length) + 4;
            int maxL_IdClass = Math.Max(stdList.Max(s => s.IdClass.ToString().Length), column5Name.Length) + 4;
            int maxL_Rank = Math.Max(rankList.Max(r => r.Length), column6Name.Length) + 4;

            string column1Cell = CreateMyStr(column1Name, maxL_StId, new int());
            string column2Cell = CreateMyStr(column2Name, maxL_Name, new int());
            string column3Cell = CreateMyStr(column3Name, maxL_Mark, new int());
            string column4Cell = CreateMyStr(column4Name, maxL_Email, new int());
            string column5Cell = CreateMyStr(column5Name, maxL_IdClass, new int());
            string column6Cell = CreateMyStr(column6Name, maxL_Rank, new int());
            #endregion

            #region StartLine
            starLine += '┌';
            for (int i = 0; i < maxL_StId; i++)
                starLine += '─';
            starLine += '┬';
            for (int i = 0; i < maxL_Name; i++)
                starLine += '─';
            starLine += '┬';
            for (int i = 0; i < maxL_Mark; i++)
                starLine += '─';
            starLine += '┬';
            for (int i = 0; i < maxL_Email; i++)
                starLine += '─';
            starLine += '┬';
            for (int i = 0; i < maxL_IdClass; i++)
                starLine += '─';
            starLine += '┬';
            for (int i = 0; i < maxL_Rank; i++)
                starLine += '─';
            starLine += '┐';
            #endregion

            #region CutLine
            cutLine += '│';
            for (int i = 0; i < maxL_StId; i++)
                cutLine += '─';
            cutLine += '│';
            for (int i = 0; i < maxL_Name; i++)
                cutLine += '─';
            cutLine += '│';
            for (int i = 0; i < maxL_Mark; i++)
                cutLine += '─';
            cutLine += '│';
            for (int i = 0; i < maxL_Email; i++)
                cutLine += '─';
            cutLine += '│';
            for (int i = 0; i < maxL_IdClass; i++)
                cutLine += '─';
            cutLine += '│';
            for (int i = 0; i < maxL_Rank; i++)
                cutLine += '─';
            cutLine += '│';
            #endregion

            #region EndLine
            endLine += '└';
            for (int i = 0; i < maxL_StId; i++)
                endLine += '─';
            endLine += '┴';
            for (int i = 0; i < maxL_Name; i++)
                endLine += '─';
            endLine += '┴';
            for (int i = 0; i < maxL_Mark; i++)
                endLine += '─';
            endLine += '┴';
            for (int i = 0; i < maxL_Email; i++)
                endLine += '─';
            endLine += '┴';
            for (int i = 0; i < maxL_IdClass; i++)
                endLine += '─';
            endLine += '┴';
            for (int i = 0; i < maxL_Rank; i++)
                endLine += '─';
            endLine += '┘';
            #endregion

            #region Prints Table
            //Console.Clear();
            //Console.WriteLine("\x1b[3J");
            //Console.SetCursorPosition(0, 0);
            Console.WriteLine(starLine);
            Console.ForegroundColor = OddCellColor;
            Console.WriteLine('│' + column1Cell + '│' + column2Cell + '│' + column3Cell + '│' + column4Cell + '│' + column5Cell + '│' + column6Cell + '│');
            Console.ResetColor();
            Console.WriteLine(cutLine);
            for (int i = 0; i < optionLength; i++)
            {
                string stIdCell = CreateRightAlignString(stdList[i].StId.ToString(), maxL_StId, 1);
                string nameCell = CreateRightAlignString(stdList[i].Name, maxL_Name, 1);
                string markCell = CreateRightAlignString(stdList[i].Mark.ToString(), maxL_Mark, 1);
                string emailCell = CreateRightAlignString(stdList[i].Email, maxL_Email, 1);
                string idClassCell = CreateRightAlignString(stdList[i].IdClass.ToString(), maxL_IdClass, 1);
                string rankCell = CreateRightAlignString(rankList[i], maxL_Rank, 1);

                if (i%2== 0)
                    Console.ForegroundColor = EvenCellColor;
                else 
                    Console.ForegroundColor = OddCellColor;
  
                Console.WriteLine('│' + stIdCell + '│' + nameCell + '│' + markCell + '│' + emailCell + '│' + idClassCell + '│' + rankCell + '│');
                Console.ResetColor();
                
                if (i != optionLength - 1)
                Console.WriteLine(cutLine);
            }
            Console.WriteLine(endLine);

            #endregion
        }


        /// <summary>
        ///     Right Align Print - Class Table
        /// </summary>
        /// <param name="clsList"></param>
        public static void RightAlignPrint(List <Class> clsList)
        {
            #region Setting Values
            string column1Name = "IdClass";
            string column2Name = "NameClass";
            string starLine = string.Empty;
            string cutLine = string.Empty;
            string endLine = string.Empty;

            int optionLength = clsList.Count();
            // Changes space here
            int maxL_IdClass = Math.Max(clsList.Max(s => s.IdClass.ToString().Length), column1Name.Length) + 3;
            int maxL_NameClass = Math.Max(clsList.Max(s => s.NameClass.Length), column2Name.Length) + 4;

            string column1Cell = CreateCenterAlignString(column1Name, maxL_IdClass, new int());
            string column2Cell = CreateCenterAlignString(column2Name, maxL_NameClass, new int());
            #endregion

            #region StartLine
            starLine += '┌';
            for (int i = 0; i < maxL_IdClass; i++)
                starLine += '─';
            starLine += '┬';
            for (int i = 0; i < maxL_NameClass; i++)
                starLine += '─';
            starLine += '┐';
            #endregion

            #region CutLine
            cutLine += '│';
            for (int i = 0; i < maxL_IdClass; i++)
                cutLine += '─';
            cutLine += '│';
            for (int i = 0; i < maxL_NameClass; i++)
                cutLine += '─';
            cutLine += '│';
            #endregion

            #region EndLine
            endLine += '└';
            for (int i = 0; i < maxL_IdClass; i++)
                endLine += '─';
            endLine += '┴';
            for (int i = 0; i < maxL_NameClass; i++)
                endLine += '─';
            endLine += '┘';
            #endregion

            #region Prints Table
            //Console.Clear();
            //Console.WriteLine("\x1b[3J");
            //Console.SetCursorPosition(0, 0);
            Console.WriteLine(starLine);
            Console.ForegroundColor = OddCellColor;
            Console.WriteLine('│' + column1Cell + '│' + column2Cell + '│');
            Console.ResetColor();
            Console.WriteLine(cutLine);
            for (int i = 0; i < optionLength; i++)
            {
                string idClassCell = CreateMyStr(clsList[i].IdClass.ToString(), maxL_IdClass, 1);
                string nameClassCell = CreateMyStr(clsList[i].NameClass, maxL_NameClass, 1);

                if (i % 2 == 0)
                    Console.ForegroundColor = EvenCellColor;
                else
                    Console.ForegroundColor = OddCellColor;
                Console.WriteLine('│' + idClassCell + '│' + nameClassCell + '│');
                Console.ResetColor();

                if (i != optionLength - 1)
                    Console.WriteLine(cutLine);
            }
            Console.WriteLine(endLine);
            Console.WriteLine();
            #endregion
        }

        #endregion

    }
}
