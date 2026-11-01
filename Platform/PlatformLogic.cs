using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 2 : BUSINESS LOGIC =====
    // Platform connect / disconnect ke rules. Har kaam: file se load, badlao, wapas save.
    // p.Connect() mein POLYMORPHISM hota hai: jo platform hai (YouTube, TikTok...) uska apna Connect chalta hai.
    class PlatformLogic
    {
        PlatformDataAccess dataAccess = new PlatformDataAccess();

        public List<Platform> GetPlatforms(int userId)
        {
            return dataAccess.LoadForUser(userId);
        }

        // index 0 se 4 tak (YouTube = 0 ... X = 4). Message wapas karta hai
        public string Connect(int userId, int index)
        {
            List<Platform> list = dataAccess.LoadForUser(userId);

            if (index < 0 || index >= list.Count)
                return "Invalid platform.";

            Platform p = list[index];

            if (p.Connected)
                return p.Name + " is already connected (simulation).";

            string message = p.Connect();

            if (!dataAccess.SaveForUser(userId, list))
                return "Could not save platforms.txt.";

            return message;
        }

        public string Disconnect(int userId, int index)
        {
            List<Platform> list = dataAccess.LoadForUser(userId);

            if (index < 0 || index >= list.Count)
                return "Invalid platform.";

            Platform p = list[index];

            if (!p.Connected)
                return p.Name + " is not connected.";

            string message = p.Disconnect();

            if (!dataAccess.SaveForUser(userId, list))
                return "Could not save platforms.txt.";

            return message;
        }
    }
}
