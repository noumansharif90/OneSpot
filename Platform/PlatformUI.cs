using System;
using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 3 : UI =====
    // Platforms ki screens: connect menu aur status.
    class PlatformUI
    {
        PlatformLogic platformLogic = new PlatformLogic();

        // ---------- 2. Connect Platforms (menu) ----------
        public void ConnectMenu(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== CONNECT PLATFORMS (SIMULATION) ==========");
                Console.WriteLine("1. YOUTUBE");
                Console.WriteLine("2. TIKTOK");
                Console.WriteLine("3. INSTAGRAM");
                Console.WriteLine("4. FACEBOOK");
                Console.WriteLine("5. X");
                Console.WriteLine("6. DISCONNECT A PLATFORM");
                Console.WriteLine("7. VIEW ALL PLATFORM STATUS");
                Console.WriteLine("8. BACK");

                int op = InputHelper.ReadInt("Select an option: ");

                if (op >= 1 && op <= 5)
                {
                    // option 1 = index 0, option 2 = index 1, ...
                    Console.WriteLine(platformLogic.Connect(user.Id, op - 1));
                    InputHelper.Pause();
                }
                else if (op == 6) Disconnect(user);
                else if (op == 7) ViewStatus(user);
                else if (op == 8) break;
                else Console.WriteLine("Wrong choice!");
            }
        }

        void Disconnect(User user)
        {
            Console.WriteLine("Which platform to disconnect?");
            Console.WriteLine("1 YouTube, 2 TikTok, 3 Instagram, 4 Facebook, 5 X");

            int number = InputHelper.ReadInt("Platform number (0 = cancel): ");

            if (number == 0)
                return;

            Console.WriteLine(platformLogic.Disconnect(user.Id, number - 1));
            InputHelper.Pause();
        }

        // ---------- 3. View Platform Status ----------
        public void ViewStatus(User user)
        {
            Console.Clear();
            Console.WriteLine("========== PLATFORM STATUS ==========");
            Console.WriteLine("(Simulation only: no real account is connected.)");
            Console.WriteLine();

            List<Platform> list = platformLogic.GetPlatforms(user.Id);

            foreach (Platform p in list)
                Console.WriteLine(p.GetStatus());   // har platform ka apna GetStatus (override ho sakta hai)

            InputHelper.Pause();
        }
    }
}
