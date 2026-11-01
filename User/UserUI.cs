using System;

namespace OneSpot
{
    // ===== LAYER 3 : UI =====
    // User ki screens: sign up, sign in, profile, password change.
    // Sirf input lena aur output dikhana. Rules UserLogic mein hain.
    class UserUI
    {
        UserLogic userLogic = new UserLogic();

        // ---------- Sign Up ----------
        public void SignUp()
        {
            Console.Clear();
            Console.WriteLine("========== SIGN UP ==========");

            string fullName = InputHelper.ReadText("Full name: ");
            string username = InputHelper.ReadText("Username: ");

            // username pehle hi check kar lo taake baqi cheezein bekar na bharni parein
            if (userLogic.UsernameExists(username))
            {
                Console.WriteLine("This username is already taken.");
                InputHelper.Pause();
                return;
            }

            string email = InputHelper.ReadText("Email: ");
            int age = InputHelper.ReadInt("Age: ");
            string password = InputHelper.ReadText("Password: ");
            string confirm = InputHelper.ReadText("Confirm password: ");

            string result = userLogic.Register(fullName, username, email, age, password, confirm);

            if (result == "OK")
                Console.WriteLine("Registration successful. You can sign in now.");
            else
                Console.WriteLine("Registration failed: " + result);

            InputHelper.Pause();
        }

        // ---------- Sign In (3 koshishen) ----------
        // Kamyab ho to User wapas, nakam ho to null
        public User SignIn()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("========== SIGN IN (attempt " + (i + 1) + " of 3) ==========");

                string username = InputHelper.ReadText("Username: ");
                string password = InputHelper.ReadText("Password: ");

                User user = userLogic.Login(username, password);

                if (user != null)
                {
                    Console.WriteLine("Welcome, " + user.FullName + "!");
                    InputHelper.Pause();
                    return user;
                }

                Console.WriteLine("Wrong username or password.");
                InputHelper.Pause();
            }

            Console.WriteLine("Too many failed attempts. Returning to the main menu.");
            InputHelper.Pause();
            return null;
        }

        // ---------- My Profile ----------
        public void Profile(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== MY PROFILE ==========");
                Console.WriteLine("User ID  : " + user.Id);
                Console.WriteLine("Name     : " + user.FullName);
                Console.WriteLine("Username : " + user.Username);
                Console.WriteLine("Email    : " + user.Email);
                Console.WriteLine("Age      : " + user.Age);
                Console.WriteLine();
                Console.WriteLine("1. EDIT PROFILE");
                Console.WriteLine("2. BACK");

                int op = InputHelper.ReadInt("Select an option: ");

                if (op == 1) EditProfile(user);
                else if (op == 2) break;
                else Console.WriteLine("Wrong choice!");
            }
        }

        void EditProfile(User user)
        {
            string fullName = InputHelper.ReadText("New full name: ");
            string email = InputHelper.ReadText("New email: ");
            int age = InputHelper.ReadInt("New age: ");

            string result = userLogic.UpdateProfile(user, fullName, email, age);

            if (result == "OK")
                Console.WriteLine("Profile updated.");
            else
                Console.WriteLine("Could not update: " + result);

            InputHelper.Pause();
        }

        // ---------- Change Password ----------
        public void ChangePassword(User user)
        {
            Console.Clear();
            Console.WriteLine("========== CHANGE PASSWORD ==========");

            string current = InputHelper.ReadText("Current password: ");
            string newPassword = InputHelper.ReadText("New password: ");
            string confirm = InputHelper.ReadText("Confirm new password: ");

            string result = userLogic.ChangePassword(user, current, newPassword, confirm);

            if (result == "OK")
                Console.WriteLine("Password changed.");
            else
                Console.WriteLine("Password not changed: " + result);

            InputHelper.Pause();
        }
    }
}
