using System;
using System.Collections.Generic;
using System.IO;

namespace OneSpot
{
    // ===== LAYER 4 : DATA ACCESS =====
    // users.txt ko parhna aur likhna. File ki har line ek user hai, 6 cheezein "|" se alag:
    //      id|fullName|username|password|email|age
    // misal:   1|Ali Khan|ali|1234|ali@mail.com|20
    class UserDataAccess
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "users.txt");

        // File se saare users parh kar List mein wapas karna
        public List<User> LoadAll()
        {
            List<User> list = new List<User>();

            // file nahi hai (pehli baar) to khali list
            if (!File.Exists(filePath))
                return list;

            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException)
            {
                return list;   // file khul nahi saki
            }

            foreach (string line in lines)
            {
                if (line.Trim() == "")
                    continue;

                string[] parts = line.Split('|');

                // kharab line (6 cheezein nahi) chhor do, program crash nahi hoga
                if (parts.Length != 6)
                    continue;

                int id;
                int age;
                if (!int.TryParse(parts[0], out id)) continue;
                if (!int.TryParse(parts[5], out age)) continue;

                list.Add(new User(id, parts[1], parts[2], parts[3], parts[4], age));
            }

            return list;
        }

        // Poori List file mein likhna. Kamyab ho to true, na ho to false
        public bool SaveAll(List<User> list)
        {
            List<string> lines = new List<string>();

            foreach (User u in list)
            {
                string line = u.Id + "|" + u.FullName + "|" + u.Username + "|" + u.Password + "|" + u.Email + "|" + u.Age;
                lines.Add(line);
            }

            try
            {
                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
