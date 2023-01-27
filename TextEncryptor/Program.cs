using System.Security.Cryptography;
using System.Text;
namespace TextEncryptor
{
    internal class Program
    {
        //Алфавит
        public static string Alphabet =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#№$%&()*+,-./:;<=>?@[]^_`{|}~АБВ" +
            "ГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿À" +
            "ÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğ" +
            "ĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĲĳĴĵĶķĸĹĺĻļĽľĿŀŁłŃńŅņŇňŉŊŋŌōŎŏŐőŒœŔŕŖŗŘřŚśŜŝŞşŠšŢţŤťŦŧŨũŪūŬŭŮůŰűŲųŴŵŶŷŸŹźŻżŽž" +
            "ſ‴┌┐│─┘└□▪▫◊○◌●◦‖‗―‒‐•❶❷❸❹❺❻❼❽❾❿⓫⓬⓭⓮⓯⓰⓱⓲⓳⓴⓿₠₡₢₣₤₥₦₧₨₩₪₫€₭₮₯₰₱₲₳₴₵₸₹₺₻₼₽₾₿Ѳᴁᴂᴆᴚᴔ‼⁞…";

        public static string Command; //Команды
        public static string text; //Текст
        public static int KEY; //Ключ шифрования
        public static string hexkey;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Random Generating = new();
            Input();
            while (Command != "EXIT")
            {
                string command = Command;
                switch (command)
                {
                    default:
                        Error();
                        break;
                    
                    case "ENCRYPT":
                        Phrases("iutxt");
                        text = Console.ReadLine();
                        KEY = Generating.Next();
                        int[] number = new int[text.Length];
                        Random random = new(KEY);
                        for (int i = 0; i < text.Length; i++)
                            number[i] = Alphabet.IndexOf(text[i]);
                        Phrases("oetxt");
                        for (int i = 0; i < number.Length; i++)
                        {
                            number[i] += random.Next(1, Alphabet.Length + 1);
                            if (number[i] > Alphabet.Length) number[i] %= Alphabet.Length + 1;
                            Out(Alphabet[number[i]]);
                        }
                        Phrases("okey");
                        break;

                    case "DECRYPT":
                        Phrases("ietxt");
                        Program.text = Console.ReadLine();
                        Phrases("ikey");
                        KEY = Convert.ToInt32(Console.ReadLine());
                        random = new(KEY);
                        number = new int[text.Length];

                        for (int i = 0; i < text.Length; i++)
                            number[i] = Alphabet.IndexOf(text[i]);
                        Phrases("odtxt");
                        for (int i = 0; i < text.Length; i++)
                        {
                            number[i] -= random.Next(1, Alphabet.Length + 1);
                            if (number[i] < 0) number[i] += Alphabet.Length + 1;
                            Out(Alphabet[number[i]]);
                        }
                        Console.WriteLine();
                        break;

                    case "HELP":
                        Help();
                        break;

                    case "CLEAR":
                        ClearConsole();
                        break;

                    case "SETTINGS":
                        Settings();
                        break;

                    case "ERRORTYPE":
                        ErrorType();
                        break;
                }
                Input();
            }
            Environment.Exit(0);
        }

        public static void ErrorType()
        {
            Console.WriteLine("\nВ разработке.\n");
        }

        public static string GenKey()
        {
            RandomNumberGenerator val = RandomNumberGenerator.Create();
            byte[] array = new byte[128];
            val.GetBytes(array);
            string text = BitConverter.ToString(array);
            return text.Replace("-", "");
        }

        public static void Phrases(string code)
        {
            switch (code)
            {
                default:
                    Console.WriteLine("\nPhrase does not exist.");
                    break;
                case "iutxt":
                    Console.WriteLine("\nEnter text:");
                    break;
                case "oetxt":
                    Console.WriteLine("\nEncrypted text:");
                    break;
                case "okey":
                    Console.WriteLine("\n" + KEY + " (Decrypt key)\n");
                    break;
                case "ietxt":
                    Console.WriteLine("Enter text:");
                    break;
                case "ikey":
                    Console.Write("\nEnter decrypt key >> ");
                    break;
                case "odtxt":
                    Console.WriteLine("\nDecrypted text:");
                    break;
            }
        }

        public static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("CLEAR                            ОЧИСТКА КОНСОЛИ");
            Console.WriteLine("DECRYPT                          ДЕШИФРОВАНИЕ");
            Console.WriteLine("ENCRYPT                          ШИФРОВАНИЕ");
            Console.WriteLine("ERRORTYPE                        УЗНАТЬ ЗНАЧЕНИЕ ОШИБКИ");
            Console.WriteLine("EXIT                             ЗАКРЫТЬ ПРИЛОЖЕНИЕ");
            Console.WriteLine();
        }

        public static void Settings()
        {
            Console.WriteLine("\nВ разработке. Наверное будет вырезано.\n");
        }

        public static void ClearConsole()
        {
            Console.Clear();
            Console.Write("\nScreen cleaned sucsessfully.\n");
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static void Out(char c)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(c);
            Console.ResetColor();
        }

        public static void Error()
        {
            Console.Write("\nIncorrect. Try again.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        public static void Input()
        {
            Console.Write("Input command (type HELP to see all commands) >> ");
            Command = Console.ReadLine().Replace(" ", "").ToUpper();
        }
    }
}