using System;

namespace OneSpot
{
    // ===== LAYER 3 : UI =====
    // Program ka starting point. Sirf welcome menu dikhata hai.
    // Baqi kaam UserUI, DashboardUI aur AdminUI karti hain.
    class Program
    {
        static void Main()
        {
            UserUI userUI = new UserUI();
            DashboardUI dashboardUI = new DashboardUI();
            AdminUI adminUI = new AdminUI();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== ONESPOT : VIDEO SHARING SIMULATOR ==========");
                Console.WriteLine("1. SIGN UP");
                Console.WriteLine("2. SIGN IN");
                Console.WriteLine("3. ABOUT ONESPOT");
                Console.WriteLine("4. EXIT");
                Console.WriteLine("5. ADMINISTRATOR");

                int choice = InputHelper.ReadInt("Select an option: ");

                if (choice == 1)
                {
                    userUI.SignUp();
                }
                else if (choice == 2)
                {
                    User user = userUI.SignIn();

                    // null ka matlab login nakam
                    if (user != null)
                        dashboardUI.Start(user);
                }
                else if (choice == 3)
                {
                    ShowAbout();
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (choice == 5)
                {
                    adminUI.Start();
                }
                else
                {
                    Console.WriteLine("Wrong choice!");
                    InputHelper.Pause();
                }
            }
        }

        static void ShowAbout()
        {
            Console.Clear();
            Console.WriteLine("========== ABOUT ONESPOT ==========");
            Console.WriteLine("OneSpot keeps the details of your videos in one place and");
            Console.WriteLine("SIMULATES sharing them to YouTube, TikTok, Instagram, Facebook and X.");
            Console.WriteLine();
            Console.WriteLine("Important:");
            Console.WriteLine("- Platform connections are simulated. No real account is used.");
            Console.WriteLine("- Uploads are simulated. No real video is published anywhere.");
            Console.WriteLine("- No video file is stored, only its information.");
            Console.WriteLine("- Passwords are saved as plain text in users.txt. This is only a");
            Console.WriteLine("  classroom simplification. A real app must hash passwords.");
            InputHelper.Pause();
        }
    }
}
