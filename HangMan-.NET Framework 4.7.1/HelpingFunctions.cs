using System;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// the namespace contains some functions that i made to help me while creating games, still working on it 
/// </summary>
namespace HelpingFunctions
{
    public class Write
    {
        public static void Middle(string s, int x = 0, int y = 0)
        {
            Console.SetCursorPosition(Math.Abs((Console.WindowWidth - s.Length) / 2 + x), Math.Abs((Console.WindowHeight / 2) + y));
            Console.Write(s);
        }
                                                        
        public static void MiddleC(string s, int x = 0, int y = 0)
        {
            ClearFunctions.ClearAll();
            Console.SetCursorPosition(Math.Abs((Console.WindowWidth - s.Length) / 2 + x), Math.Abs((Console.WindowHeight / 2) + y));
            Console.Write(s);
        }
    }

    public class ClearFunctions
    {
        public static void ClearAll()
        {
            Console.Clear();
        }

        public static void ClearLine(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(x, y);
        }
    }

    public class WaitFunctions
    {
        public static void WaitForSeconds(int time)
        {
            time *= 1000;
            Thread.Sleep(time);
        }

        public static void WaitForMinutes(int time)
        {
            time *= (1000 * 60);
            Thread.Sleep(time);
        }

        public static void WaitForMS(int time)
        {
            Thread.Sleep(time);
        }
    }
    public class Menu
    {
        static List<String> items = new List<String>();

        static bool isChoosing = true;

        public static int HorizontalSpace = 0;
        public static int VerticalSpace = 1;
        public static int CurrentHoverItemIndex = 0;
        static int NOT = 0; //Number Of Items

        public static void additem(string s)
        {
            items.Insert(NOT, s);
            NOT++;
        }

        public static void ResetMenu()
        {
            isChoosing = true;
            items.Clear();
            NOT = 0;
            CurrentHoverItemIndex = 0;
    }

        public class MenuSystem
        {

            static void CheckForMenuInput()
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                {
                    if (CurrentHoverItemIndex != 0)
                        CurrentHoverItemIndex--;
                }

                if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                {
                    if (CurrentHoverItemIndex != items.Count - 1)
                        CurrentHoverItemIndex++;
                }

                if (key == ConsoleKey.Enter)
                {
                    isChoosing = false;
                }
            }

            /// <summary>
            /// Starts the menu system
            /// the function returns an integer when the user clicks on the (Enter key)
            /// the integer value is 0 for the first item, 1 for the second item, n for the nth item
            /// </summary>
            public static int StartTheMenuSystem()
            {
                ClearFunctions.ClearAll();
                while (isChoosing)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i == CurrentHoverItemIndex)
                        {
                            //Choose Hover Item Color
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write.Middle(items[i], 0, i);
                            //Normal Item Color
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                        Write.Middle(items[i], i * HorizontalSpace, i * VerticalSpace);
                    }

                    CheckForMenuInput();
                    ClearFunctions.ClearAll();
                }

                return CurrentHoverItemIndex;
            }
        }
    }

    public class Cursor
    {
        public static void SetCursorPosMiddle(int x = 0, int y = 0)
        {
            Console.SetCursorPosition((Console.LargestWindowWidth / 2) + x, (Console.LargestWindowHeight / 2) + y);
        }

        public static void SetCursorPos(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}
