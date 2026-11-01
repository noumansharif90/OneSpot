using System;

namespace OneSpot
{
    // ===== HELPER (UI layer ke saath use hota hai) =====
    // Yeh "static" class hai: iska object nahi banta, seedha InputHelper.ReadInt(...) likhte hain.
    // Kaam: user se safe input lena, taake galat input par program crash ya infinite loop na ho.
    static class InputHelper
    {
        // Ek line parhna. Agar input khatam ho jaye (null) to program band kar do (infinite loop se bachne ke liye)
        static string ReadLineSafe()
        {
            string line = Console.ReadLine();

            if (line == null)
                Environment.Exit(0);

            return line.Trim();
        }

        // Number lena. Harf likhe to dobara pochta hai (int.TryParse crash nahi hota)
        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string text = ReadLineSafe();

                int number;
                if (int.TryParse(text, out number))
                    return number;

                Console.WriteLine("Please type a number.");
            }
        }

        // Text lena. Khali nahi hona chahiye aur "|" nahi hona chahiye (kyun ke file mein "|" alag karne ke liye use hota hai)
        public static string ReadText(string message)
        {
            while (true)
            {
                Console.Write(message);
                string text = ReadLineSafe();

                if (text == "")
                    Console.WriteLine("This field cannot be empty.");
                else if (text.Contains("|"))
                    Console.WriteLine("The | character is not allowed.");
                else
                    return text;
            }
        }

        // Yes / No sawal. y likho to true, n likho to false
        public static bool AskYesNo(string message)
        {
            while (true)
            {
                Console.Write(message);
                string text = ReadLineSafe().ToLower();

                if (text == "y" || text == "yes") return true;
                if (text == "n" || text == "no") return false;

                Console.WriteLine("Please type y or n.");
            }
        }

        // Screen ruk jaye jab tak koi key na dabaye
        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
