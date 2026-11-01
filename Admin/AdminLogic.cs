namespace OneSpot
{
    // ===== LAYER 2 : BUSINESS LOGIC =====
    // Admin ka login check.
    // TEST ACCOUNT (sirf class project ke liye): username "admin", password "admin123".
    // Asli application mein password code mein nahi likhte aur hash karke rakhte hain.
    class AdminLogic
    {
        string adminUsername = "admin";
        string adminPassword = "admin123";

        public bool Login(string username, string password)
        {
            return username == adminUsername && password == adminPassword;
        }
    }
}
