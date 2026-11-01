using System;
using System.Collections.Generic;
using System.IO;

namespace OneSpot
{
    // ===== LAYER 4 : DATA ACCESS =====
    // platforms.txt: har user ki platform connection states. Har line:
    //      userId|platformName|connected
    // misal:   1|YouTube|True
    // Har user ki apni lines hoti hain, is liye ek user ki state doosre se mix nahi hoti.
    class PlatformDataAccess
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "platforms.txt");

        // Paanchon platforms ki List banana. List<Platform> mein bachon ke objects hain (POLYMORPHISM)
        List<Platform> CreateAll()
        {
            List<Platform> list = new List<Platform>();
            list.Add(new YouTube());
            list.Add(new TikTok());
            list.Add(new Instagram());
            list.Add(new Facebook());
            list.Add(new XPlatform());
            return list;
        }

        // Is user ke platforms: file se connected/not connected set karna
        public List<Platform> LoadForUser(int userId)
        {
            List<Platform> list = CreateAll();

            if (!File.Exists(filePath))
                return list;

            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException)
            {
                return list;
            }

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length != 3)
                    continue;

                int id;
                bool connected;
                if (!int.TryParse(parts[0], out id)) continue;
                if (!bool.TryParse(parts[2], out connected)) continue;

                if (id != userId)
                    continue;

                foreach (Platform p in list)
                {
                    if (p.Name == parts[1])
                        p.Connected = connected;
                }
            }

            return list;
        }

        // Is user ki lines badalna, baqi users ki lines wesi hi rehti hain
        public bool SaveForUser(int userId, List<Platform> list)
        {
            List<string> lines = new List<string>();

            // 1. doosre users ki purani lines rakh lo
            if (File.Exists(filePath))
            {
                try
                {
                    foreach (string line in File.ReadAllLines(filePath))
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 3 && parts[0] != userId.ToString())
                            lines.Add(line);
                    }
                }
                catch (IOException)
                {
                    return false;
                }
            }

            // 2. is user ki nayi lines jor do
            foreach (Platform p in list)
                lines.Add(userId + "|" + p.Name + "|" + p.Connected);

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
