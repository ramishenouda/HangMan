using System;
using System.IO;
using HelpingFunctions;
using System.Collections.Generic;
namespace HangMan
{
    // Represent information and functions about the word file, 
    class Words
    {
        static string line;
        /// <summary> the Path for the words file </summary>
        public static string Path = AppContext.BaseDirectory + "\\words.txt";
        /// <summary> the Path for a temp word file </summary>
        public static string Path2 = AppContext.BaseDirectory + "\\words2.txt";

        /// <summary>
        /// display the words
        /// </summary>
        public static void Display()
        {
            Filter();
            StreamReader SR = new StreamReader(AppContext.BaseDirectory + "\\words.txt");
            line = SR.ReadLine();

            if (line == null)
            {
                Console.Write("You Didn't add any words yet");
            }

            while (line != null)
            {
                Console.Write(line + ", ");
                line = SR.ReadLine();
            }

            SR.Close();
            Console.WriteLine("\nPress any button to back");
            var key = Console.ReadKey();
            HangManMenu.WordsMenu();
        }

        /// <summary>
        /// Adds a word to the word.txt file
        /// </summary>
        public static void Add()
        {
            Filter();

            while (true)
            {
                ClearFunctions.ClearAll();
                StreamWriter SW;
                StreamReader SR;

                SR = new StreamReader(AppContext.BaseDirectory + "\\words.txt");

                line = SR.ReadLine();
                while (line != null)
                {
                    Console.Write(line + ", ");
                    line = SR.ReadLine();
                }

                SR.Close();
                SW = new StreamWriter(AppContext.BaseDirectory + "\\words.txt", true);

                Console.Write("\nEnter a word to be added(Enter (-) to back) : ");

                /*
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    SW.Close();
                    WordsMenu();
                    Console.WriteLine("Error in Addwords Function");//just in case if WordsMenu() didn't execute
                }
                line = key.KeyChar + Console.ReadLine();
                */

                line = Console.ReadLine();
                if (line == "-")
                {
                    SW.Close();
                    HangManMenu.WordsMenu();
                    return;
                }
                line = line.ToUpper();
                SW.WriteLine(line);
                SW.Close();
            }
        }

        /// <summary>
        /// Deletes a word from word.txt file
        /// </summary>
        public static void Delete()
        {
            Filter();
            StreamWriter SW;
            StreamReader SR;

            string Path = AppContext.BaseDirectory + "\\words.txt";
            string Path2 = AppContext.BaseDirectory + "\\words2.txt";

            while (true)
            {
                ClearFunctions.ClearAll();
                var file = File.CreateText(Path2);

                int WordIndex = 0;
                SR = new StreamReader(Path);

                line = SR.ReadLine();
                while (line != null)
                {
                    Console.Write(line + "(" + WordIndex + "), ");
                    line = SR.ReadLine();
                    WordIndex++;
                }

                SR.Close();

                Console.Write("\nEnter word number to be deleted(Enter (-) to back) : ");

                /*var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    file.Close();
                    File.Delete(Path2);
                    FilterWords();
                    WordsMenu();
                }
                string indexChar = key.KeyChar + Console.ReadLine();
                 */

                string indexChar = Console.ReadLine();
                if (indexChar == "-")
                {
                    file.Close();
                    File.Delete(Path2);
                    Filter();
                    HangManMenu.WordsMenu();
                    return;
                }

                try
                {
                    try
                    {
                        Convert.ToInt32(indexChar);
                    }
                    catch
                    {
                        file.Close();
                        File.Delete(Path2);
                        Filter();
                        Delete();
                    }

                    int WordIndexToDelete = Convert.ToInt32(indexChar);
                    WordIndex = 0;
                    SR = new StreamReader(Path);

                    line = SR.ReadLine();

                    while (line != null)
                    {
                        if (WordIndex == WordIndexToDelete)
                            line = SR.ReadLine();

                        file.WriteLine(line);
                        line = SR.ReadLine();
                        WordIndex++;
                    }

                    file.Close();
                    SR.Close();

                    SR = new StreamReader(Path2);
                    SW = new StreamWriter(Path);

                    line = SR.ReadLine();

                    while (line != null)
                    {
                        SW.WriteLine(line);
                        line = SR.ReadLine();
                    }

                    SR.Close();
                    SW.Close();
                    file.Close();
                    Filter();
                }

                catch
                {
                    Console.WriteLine("error 210");
                }

            }
        }

        /// <summary>
        /// Deletes the empty words from words.txt file
        /// </summary>
        public static void Filter()
        {
            StreamWriter SW;
            StreamReader SR;


            if (File.Exists(Path2))
            {
                File.Delete(Path2);
            }
            var file = File.CreateText(Path2);

            SR = new StreamReader(Path);

            //try
            //{

                line = SR.ReadLine();

                while (line != null)
                {
                    if (line == "" || line == " ")
                    {
                        line = SR.ReadLine();
                        continue;
                    }
                    file.WriteLine(line);
                    line = SR.ReadLine();
                }

                SR.Close();
                file.Close();

                SR = new StreamReader(Path2);
                SW = new StreamWriter(Path);

                line = SR.ReadLine();

                while (line != null)
                {
                    SW.WriteLine(line);
                    line = SR.ReadLine();
                }

                SR.Close();
                SW.Close();
                File.Delete(Path2);
            //}

            //catch
            //{
            //    Console.WriteLine("Error Filter");
            //}

        }

        public static string GetWord(int index)
        {
            StreamReader SR = new StreamReader(Path);
            string Word = SR.ReadLine();
            for (int i = 0; i < index; i++)
            {
                Word = SR.ReadLine();
                if (Word == null)
                    break;
            }

            SR.Close();
            return Word;
        }

        public static int NumberOfWords()
        {
            StreamReader SR = new StreamReader(Path);
            string Line = SR.ReadLine();
            int Size = 0;

            while (Line != null)
            {
                Size++;
                Line = SR.ReadLine();
            }
            SR.Close();
            return Size;
        }
    }

    // All the game Menus stored here
    class HangManMenu
    {
        public static void StartMenu()
        {
            Menu.ResetMenu();
            Menu.additem("Play");
            Menu.additem("Game Words");
            Menu.additem("Exit");
            CheckMenuChooseOption(Menu.MenuSystem.StartTheMenuSystem(), 0);
        }

        public static void WordsMenu()
        {
            Menu.ResetMenu();
            Menu.additem("Display Words");
            Menu.additem("Add Words");
            Menu.additem("Delete Words");
            Menu.additem("Back");
            CheckMenuChooseOption(Menu.MenuSystem.StartTheMenuSystem(), 1);
        }
            
        public static void ChooseLevelMenu()
        {
            Menu.ResetMenu();
            Menu.additem("Easy");
            Menu.additem("Normal");
            Menu.additem("Hard");
            Menu.additem("Back");
            CheckMenuChooseOption(Menu.MenuSystem.StartTheMenuSystem(), 2);
        }

        public static void MatchResultMenu()
        {
            Menu.ResetMenu();
            Menu.additem("Play Again");
            Menu.additem("Main Menu");
            Menu.additem("Exit");
            CheckMenuChooseOption(Menu.MenuSystem.StartTheMenuSystem(), 3);
        }

        public static void Exit()
        {
            Write.MiddleC("Exiting....");
            Words.Filter();
            Environment.Exit(0);
        }

        static void CheckMenuChooseOption(int value, int menu)
        {
            if (menu == 0)
            {
                switch (value)
                {
                    case 0:
                        ChooseLevelMenu();
                        break;

                    case 1:
                        WordsMenu();
                        break;

                    case 2:
                        Exit();
                        break;
                }
            }

            else if (menu == 1)
            {
                switch (value)
                {
                    case 0:
                        Words.Display();
                        break;
                    case 1:
                        Words.Add();
                        break;

                    case 2:
                        Words.Delete();
                        break;
                    case 3:
                        StartMenu();
                        break;
                }
            }

           else  if (menu == 2)
            {
                switch (value)
                {
                    case 0:
                        HangManGame.Level = 1.25f;
                        HangManGame.Start();
                        break;
                    case 1:
                        HangManGame.Level = 1.5f;
                        HangManGame.Start();
                        break;

                    case 2:
                        HangManGame.Level = 2f;
                        HangManGame.Start();
                        break;

                    case 3:
                        StartMenu();
                        break;
                }
            }

            else if (menu == 3)
            {
                switch (value)
                {
                    case 0:
                        ChooseLevelMenu();
                        break;
                    case 1:
                        StartMenu();
                        break; ;

                    case 2:
                        Exit();
                        break;
                }
            }
        }



        /// <summary> Starts the menu </summary>
        public static void Awake()
        {
            Words.Filter();
            StartMenu();
        }

    }
  
    // Game Logic
    class HangManGame
    {
        static string Word;
        static int Lives = 3;
        readonly static char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        static char[] Keyboard;
        static string[] playerAnswer;
        static List<string> Board = new List<string>();
        static Random random = new Random();

        public static float Level;

        public static void SetKeyBoard(string Word)
        {
            int Size = (int)(Word.Length * Level);

            if (Size % 2 != 0)
                Size++;

            Keyboard = new char[Size];
            for (int i = 0; i < Size; i++)
                Keyboard[i] = ' ';

            for(int i = 0; i < Word.Length; i++)
            {
                int index = random.Next(0, Size);
                while(Keyboard[index] != ' ')
                    index = random.Next(0, Size);

                Keyboard[index] = Word[i];
            }

            for (int i = 0; i < Size - Word.Length; i++)
            {
                int index = random.Next(0, Size);
                int index2 = random.Next(0, 26);

                while (Keyboard[index] != ' ')
                    index = random.Next(0, Size);

                Keyboard[index] = alphabet[index2];
            }
        }

        static void SetBoard()
        {
            int Size = Keyboard.Length;
            for(int i = 0; i < 2; i++)
            {
                int j = (Size / 2) * i;
                int size = (Size / 2) * (i + 1);
                for (; j < size; j++)
                {
                    Board.Add("|");
                    Board.Add(Keyboard[j].ToString());
                    Board.Add("|");
                }
                Board.Add("\n");
                for (int k = j; k < Size; k++)
                {
                    Board.Add("-");
                    Board.Add("-");
                    Board.Add("-");
                }
                Board.Add("\n");
            }
        }

        public static void DispalyBoard(int DisplayingStatue)
        {
            string StringToDisplay = "";
            ClearFunctions.ClearAll();
            Console.WriteLine("Lives : " + Lives);
            //to move the cursor up and down from the center
            //increasing this variable will move the cursor down, decreasing  will move the cursor up
            int VerticalSpace = 0;

            //to move the cursor left and right from the center
            //positive value will move the cursor to the right, nagtive to the left
            //int HorizontalSpace = Keyboard.Length / 2;

            //this is just a variable to declare where to start count the horizontal space
            //the horizontal space depends on the length of the characters in the line
            //this variable will simply change to the value of i(the for loop counter) when then vertical space increase
            int Index = 0;

            while(Index < Board.Count)
            {
                while (Board[Index] != "\n")
                {
                    StringToDisplay += Board[Index];
                    Index++;
                }

                Write.Middle(StringToDisplay, 0, VerticalSpace);
                StringToDisplay = "";
                VerticalSpace++;
                Index++;
            }

            StringToDisplay = "";

            for (int i = 0; i < playerAnswer.Length; i++)
            {
                StringToDisplay += playerAnswer[i] + " ";
            }

            Write.Middle(StringToDisplay, 0, VerticalSpace++);

            switch(DisplayingStatue)
            {
                case 1:
                    Console.Write("\nEnter only one character");
                    break;

                case 2:
                    Console.Write("\nThe character should be in the keyboard shown in the screen");
                    break;

                case 3:
                    Console.Write("\nCorrect!");
                    break;

                case 4:
                    Console.Write("\nOPS, Wrong answer");
                    break;
            }

            Console.Write("\nEnter A Character : ");
        }

        static bool CheckAnswer(string Character)
        {
            for (int i = 0; i < Word.Length; i++)
                if (Character == Word[i].ToString() && playerAnswer[i] != Word[i].ToString())
                {
                    playerAnswer[i] = Character;
                    return true;
                }

            return false;
        }

        static bool CheckCharacterExist(string Character)
        {
            for (int i = 0; i < Board.Count; i++)
            {
                if (Character == Board[i])
                    return true;
            }

            return false;
        }

        ///<summary> Updating frames </summary>
        public static void Update()
        {
            DispalyBoard(0);
            string Character;
            int AnswerLength = 0;
            while(true)
            {
                Character = Console.ReadLine();
                Character = Character.ToUpper();

                //Checking if the user entered more than one character
                while (Character.Length > 1)
                {
                    DispalyBoard(1);
                    Character = Console.ReadLine();
                    Character = Character.ToUpper();
                }
                //Check if the user entered a character does not exist on the keyboard

                while (!CheckCharacterExist(Character))
                {
                    DispalyBoard(2);
                    Character = Console.ReadLine();
                    Character = Character.ToUpper();

                    if (Character.Length > 1)
                    {
                        break;
                    }
                }

                if (Character.Length > 1)
                {
                    DispalyBoard(1);
                    continue;
                }

                if (CheckAnswer(Character))
                {
                    for (int i = 0; i < Board.Count; i++)
                    {
                        if (Character == Board[i])
                        {
                            Board[i] = " ";
                            break;
                        }
                    }
                    AnswerLength++;
                    DispalyBoard(3);
                }

                else
                {
                    Lives--;
                    DispalyBoard(4);
                }

                if(Lives <= 0)
                {
                    break;
                }

                if(AnswerLength == Word.Length)
                {
                    break;
                }
            }

            ClearFunctions.ClearAll();
            if (Lives <= 0)
            {
                Write.MiddleC("OPS, You Lost :(");
                WaitFunctions.WaitForSeconds(4);
                ClearFunctions.ClearAll();
                WaitFunctions.WaitForSeconds(1);
            }

            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Write.Middle("You Win!");
                    WaitFunctions.WaitForMS(800);
                    ClearFunctions.ClearAll();
                    WaitFunctions.WaitForMS(300);
                }
            }

            HangManMenu.MatchResultMenu();

        }

        public static void Start()
        {
            random = new Random();
            int RandomWordIndex = random.Next(0, Words.NumberOfWords());

            if(Words.NumberOfWords() <= 0)
            {
                Write.MiddleC("You need to add words first");
                WaitFunctions.WaitForSeconds(3);
                HangManMenu.ChooseLevelMenu();
                return;
            }

            Lives = 3;
            Board = new List<string>();
            Word = Words.GetWord(RandomWordIndex);
            playerAnswer = new string[Word.Length];

            for (int i = 0; i < playerAnswer.Length; i++)
            {
                playerAnswer[i] = "...";
            }

            SetKeyBoard(Word);
            SetBoard();

            Update();
        }

        /// <summary> Starts the Game </summary>
        public static void Awake()
        {
            Console.CursorVisible = false;
            
            FileStream file;
            if (!File.Exists(Words.Path))
            {
                file = new FileStream(Words.Path, FileMode.Create);
                file.Close();
            }

            HangManMenu.Awake();
        }
    }
}
