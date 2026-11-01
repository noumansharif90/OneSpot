using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 2 : BUSINESS LOGIC =====
    // User ke rules: register, login, profile update, password change.
    // Is mein Console.WriteLine nahi hota. File ka kaam UserDataAccess karti hai.
    // Jo methods string wapas karte hain: "OK" ka matlab kaam ho gaya, warna error ka message.
    class UserLogic
    {
        UserDataAccess dataAccess = new UserDataAccess();

        public List<User> GetAll()
        {
            return dataAccess.LoadAll();
        }

        // Naya id: sab se bara id + 1
        int NextId(List<User> list)
        {
            int max = 0;
            foreach (User u in list)
            {
                if (u.Id > max)
                    max = u.Id;
            }
            return max + 1;
        }

        // Username pehle se maujood hai? (chhote bare harf ka farq nahi)
        public bool UsernameExists(string username)
        {
            return FindByUsername(username) != null;
        }

        // Username se user dhoondna (na mile to null)
        public User FindByUsername(string username)
        {
            foreach (User u in dataAccess.LoadAll())
            {
                if (u.Username.ToLower() == username.Trim().ToLower())
                    return u;
            }
            return null;
        }

        // Email theek hai? (@ ho, @ ke baad dot ho, space na ho)
        bool IsValidEmail(string email)
        {
            int at = email.IndexOf('@');
            if (at < 1) return false;
            if (email.Contains(" ")) return false;

            int dot = email.IndexOf('.', at);
            return dot > at + 1 && dot < email.Length - 1;
        }

        // Naya user register karna
        public string Register(string fullName, string username, string email, int age, string password, string confirm)
        {
            if (fullName.Trim() == "" || username.Trim() == "" || email.Trim() == "" || password == "")
                return "All fields are required.";

            if (UsernameExists(username))
                return "This username is already taken.";

            if (!IsValidEmail(email))
                return "The email address is not valid.";

            if (age < 13 || age > 100)
                return "Age must be between 13 and 100.";

            if (password.Length < 4)
                return "Password must have at least 4 characters.";

            if (password != confirm)
                return "Passwords do not match.";

            List<User> list = dataAccess.LoadAll();
            User u = new User(NextId(list), fullName.Trim(), username.Trim(), password, email.Trim(), age);
            list.Add(u);

            if (!dataAccess.SaveAll(list))
                return "Could not save users.txt.";

            return "OK";
        }

        // Login: sahi ho to User wapas, galat ho to null
        public User Login(string username, string password)
        {
            foreach (User u in dataAccess.LoadAll())
            {
                if (u.Username.ToLower() == username.Trim().ToLower() && u.Password == password)
                    return u;
            }
            return null;
        }

        // Profile update (naam, email, age). Username aur password yahan nahi badalte
        public string UpdateProfile(User user, string fullName, string email, int age)
        {
            if (fullName.Trim() == "" || email.Trim() == "")
                return "Name and email are required.";

            if (!IsValidEmail(email))
                return "The email address is not valid.";

            if (age < 13 || age > 100)
                return "Age must be between 13 and 100.";

            List<User> list = dataAccess.LoadAll();
            foreach (User u in list)
            {
                if (u.Id == user.Id)
                {
                    u.FullName = fullName.Trim();
                    u.Email = email.Trim();
                    u.Age = age;

                    if (!dataAccess.SaveAll(list))
                        return "Could not save users.txt.";

                    // session wale user ko bhi update karo
                    user.FullName = u.FullName;
                    user.Email = u.Email;
                    user.Age = u.Age;
                    return "OK";
                }
            }
            return "User not found.";
        }

        // Password badalna (pehle purana password check hota hai)
        public string ChangePassword(User user, string current, string newPassword, string confirm)
        {
            if (current != user.Password)
                return "Current password is wrong.";

            if (newPassword.Length < 4)
                return "New password must have at least 4 characters.";

            if (newPassword != confirm)
                return "New passwords do not match.";

            List<User> list = dataAccess.LoadAll();
            foreach (User u in list)
            {
                if (u.Id == user.Id)
                {
                    u.Password = newPassword;

                    if (!dataAccess.SaveAll(list))
                        return "Could not save users.txt.";

                    user.Password = newPassword;
                    return "OK";
                }
            }
            return "User not found.";
        }
    }
}
