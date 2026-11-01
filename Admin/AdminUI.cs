using System;
using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 3 : UI =====
    // Admin ki screen: login aur menu. Admin sirf users dekh sakta hai, kisi ka password nahi.
    class AdminUI
    {
        AdminLogic adminLogic = new AdminLogic();
        UserLogic userLogic = new UserLogic();
        VideoLogic videoLogic = new VideoLogic();
        UploadLogic uploadLogic = new UploadLogic();

        public void Start()
        {
            if (!Login())
            {
                Console.WriteLine("Access denied.");
                InputHelper.Pause();
                return;
            }

            Menu();
        }

        // 3 koshishen
        bool Login()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("========== ADMIN LOGIN (attempt " + (i + 1) + " of 3) ==========");
                Console.WriteLine("(Test account for this class project: admin / admin123)");

                string username = InputHelper.ReadText("Username: ");
                string password = InputHelper.ReadText("Password: ");

                if (adminLogic.Login(username, password))
                    return true;

                Console.WriteLine("Wrong credentials.");
                InputHelper.Pause();
            }

            return false;
        }

        void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== ADMIN MENU ==========");
                Console.WriteLine("1. VIEW REGISTERED USERS");
                Console.WriteLine("2. SEARCH USER BY USERNAME");
                Console.WriteLine("3. SYSTEM INFORMATION");
                Console.WriteLine("4. BACK TO MAIN MENU");

                int op = InputHelper.ReadInt("Select an option: ");

                if (op == 1) ViewUsers();
                else if (op == 2) SearchUser();
                else if (op == 3) SystemInfo();
                else if (op == 4) break;
                else Console.WriteLine("Wrong choice!");
            }
        }

        void ViewUsers()
        {
            Console.Clear();
            Console.WriteLine("========== REGISTERED USERS ==========");

            List<User> list = userLogic.GetAll();

            if (list.Count == 0)
                Console.WriteLine("No users registered yet.");

            foreach (User u in list)
                Console.WriteLine(u);       // ToString override: password nazar nahi aata

            InputHelper.Pause();
        }

        void SearchUser()
        {
            string username = InputHelper.ReadText("Enter username: ");
            User u = userLogic.FindByUsername(username);

            if (u == null)
                Console.WriteLine("No such user.");
            else
                Console.WriteLine(u);

            InputHelper.Pause();
        }

        void SystemInfo()
        {
            Console.Clear();
            Console.WriteLine("========== SYSTEM INFORMATION ==========");
            Console.WriteLine("Registered users        : " + userLogic.GetAll().Count);
            Console.WriteLine("Saved videos            : " + videoLogic.Count());
            Console.WriteLine("Simulated upload records: " + uploadLogic.Count());
            InputHelper.Pause();
        }
    }
}
