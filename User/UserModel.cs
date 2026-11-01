namespace OneSpot
{
    // ===== LAYER 1 : MODEL =====
    // User ka data: id, naam, username, password, email, age.
    // ENCAPSULATION: saare fields private hain, bahar se properties ke zariye milte hain.
    class User
    {
        private int id;
        private string fullName;
        private string username;
        private string password;
        private string email;
        private int age;

        // Id sirf get hai (set nahi): id ek baar ban jaye to koi badal nahi sakta
        public int Id
        {
            get { return id; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string Username
        {
            get { return username; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        // Constructor: user banate waqt saari values set hoti hain
        public User(int id, string fullName, string username, string password, string email, int age)
        {
            this.id = id;
            this.fullName = fullName;
            this.username = username;
            this.password = password;
            this.email = email;
            this.age = age;
        }

        // OVERRIDE: user ko print karo to password nazar nahi aata
        public override string ToString()
        {
            return "ID: " + id + " | " + fullName + " | @" + username + " | " + email + " | Age " + age;
        }
    }
}
