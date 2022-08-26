using kbbs_test.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace kbbs_test
{
    public static class Helper
    {
        public static Random _rnd = new Random();

        public static string[] _TitleWords = new string[]
        {
            "Godly", "Wool", "Agreement", "Foamy", "Dream", "Jazzy", "Substantial", "Haircut", "Sloppy", "Pickle", "Copper", "Chief", "Relieved", "Distance", "Jaded", "Staking", "Meaty", "Abusive", "Sheep", "Deafening", "Refuse", "Subdued", "Dirty", "Blushing", "Look", "Oval", "Decide", "Quixotic", "Concern", "Wry", "Rabid", "Materialistic", "Sin", "Obtainable", "Birthday", "Eggs", "Slimy", "Signal", "Erect", "Momentous", "Frame", "Onerous", "Soggy", "Blue", "Condemned", "Borrow", "Hobbies", "River", "Itchy", "Pink", "Type", "Even", "Placid", "Bitter", "Touch", "Pot", "Loaf", "Longing", "Reflective", "Zipper", "Acidic", "Telling", "Color", "Scared", "Caring", "Play", "Real", "Spotless", "Pathetic", "Health", "March", "Terrify", "Zippy", "Chop", "Entertain", "Low", "Economic", "Tearful", "Grumpy", "Word", "Round", "Belligerent", "Battle", "Walk", "Thank", "Foot", "Enchanted", "Force", "Party", "Cheerful", "Ink", "Brush", "Oranges", "Tiny", "Remarkable", "Lip", "Snake", "Chemical", "Corn", "Scientific"
        };

        public static string[] _FirstNames = new string[]
        {
            "Keshaun","Jayla","Regina","Martha","Kate","Jude","Mitchell","Shira","Iman","Louie","Marcela","Elaina","Marley","Whitley","Graham","Ulysses","Julieta","Kaylin","Hallie","Kristal","Tony","Yajaira","Shay","Lacey","Patrick","Layton","Monet","Miguel","Leslie","Anastasia","Unique","Albert","Jadon","Kenton","Janette","Kianna","Nathanial","Naomi","Augustus","Janelle","Adrianna","Maribel","Lorraine","Acacia","Nasir","Walker","Bobbie","Marcella","Maura","Sommer"
        };

        public static string[] _LastNames = new string[]
        {
            "Parkinson","Vieira","Patton","Gaffney","Satterfield","Cota","Yoder","Thrasher","Craig","Duggan","Pereira","Qualls","Ryan","Rapp","Herring","Sturm","Custer","Delgadillo","Schrader","Pickens","Bean","Michel","Prince","Bagley","Munn","McGovern","McConnell","Cloud","Muir","Riggins","Peters","Weiss","Estes","Mathew","Martin","Keys","Conner","Neeley","Cohn","Tully","Kelley","Hamel","Mchenry","Faber","Fox","Escalante","Bland","Putnam","Hiatt","Harlow"
        };

        public static string[] _Genres = new string[]
        {
            "Fantasy", "Adventure", "Self-help", "Travel", "Horror", "Science Fiction", "Thriller", "Guide / How-to", "Motivational", "Romance", "Cookbook", "Mystery"
        };

        /// <summary>
        /// Takes between one and three random words and generates a book title
        /// </summary>
        /// <returns></returns>
        public static string GenerateTitle()
        {
            int wordCount = _rnd.Next(3) + 1;

            string result = "";

            for(int i = 0; i < wordCount; i++)
            {
                result += $"{(i > 0 ? " " : "")}{_TitleWords[_rnd.Next(_TitleWords.Length - 1)]}";
            }

            return result;
        }

        /// <summary>
        /// Generates a string of digits of the specified length
        /// </summary>
        /// <param name="sectionLength"></param>
        /// <returns></returns>

        public static string GenerateISBNSection(int sectionLength)
        {
            string result = "";

            for(int i = 0; i < sectionLength; i++)
            {
                result += _rnd.Next(10).ToString();
            }

            return result;
        }

        /// <summary>
        /// Generates a crude 13-digit ISBN string
        /// </summary>
        /// <returns></returns>
        public static string GenerateISBN()
        {
            string[] prefix = new string[] { "978", "979" };

            return $"{prefix[_rnd.Next(2)]}-{GenerateISBNSection(1)}-{GenerateISBNSection(5)}-{GenerateISBNSection(3)}-{GenerateISBNSection(1)}";
        }

        /// <summary>
        /// Returns a list of Book objects, with the specified length
        /// </summary>
        /// <returns></returns>
        public static List<Book> GenerateBooks(int count)
        {
            List<Book> result = new List<Book>();

            for(int i = 0; i < count; i++)
            {
                int yearPublished = DateTime.Now.AddYears(-(_rnd.Next(200))).Year,
                    pageCount = 1000 - _rnd.Next(700);

                string author = $"{_FirstNames[_rnd.Next(_FirstNames.Length)]} {_LastNames[_rnd.Next(_LastNames.Length)]}";

                result.Add(new Book(GenerateTitle(),
                                    author,
                                    GenerateISBN(),
                                    _Genres[_rnd.Next(_Genres.Length)],
                                    yearPublished,
                                    pageCount));
            }

            return result;
        }

        /// <summary>
        /// Writes exception messages to errorLog.txt, with timestamps
        /// </summary>
        /// <param name="ex"></param>
        public static void LogException(Exception ex)
        {
            using (StreamWriter streamWriter = new StreamWriter($"{Environment.CurrentDirectory}/errorLog.txt", true))
            {
                streamWriter.WriteLine("-------------------------------");

                streamWriter.WriteLine(DateTime.Now.ToString());

                streamWriter.WriteLine();

                streamWriter.WriteLine(ex.Message);

                streamWriter.WriteLine("-------------------------------");

                streamWriter.WriteLine();
            }
        }
    }
}
