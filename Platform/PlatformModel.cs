namespace OneSpot
{
    // ===== LAYER 1 : MODEL (Base Class) =====
    // Platform woh parent class hai jis se YouTube, TikTok, Instagram, Facebook, XPlatform bante hain (INHERITANCE).
    // Sab ki common cheezein (naam, connected hai ya nahi, max seconds) yahan hain.
    //
    // VIRTUAL methods: bachon ko ijazat hai ke woh inhein apne tareeqe se badal (override) sakein.
    //
    // Yaad rahe: sab kuch SIMULATION hai. Koi asli account connect nahi hota, koi asli video upload nahi hoti.
    class Platform
    {
        private string name;
        private bool connected;
        private int maxSeconds;     // is platform par kitni lambi video (simulation rule)

        public string Name
        {
            get { return name; }
        }

        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        public int MaxSeconds
        {
            get { return maxSeconds; }
        }

        // Constructor: naam aur max seconds. Shuru mein connected = false
        public Platform(string name, int maxSeconds)
        {
            this.name = name;
            this.maxSeconds = maxSeconds;
            this.connected = false;
        }

        // Connect (simulation). Message wapas karta hai
        public virtual string Connect()
        {
            connected = true;
            return name + " connected (simulation only).";
        }

        // Disconnect (simulation)
        public virtual string Disconnect()
        {
            connected = false;
            return name + " disconnected (simulation only).";
        }

        // Is platform ke rules ka text
        public virtual string GetRules()
        {
            return "max " + maxSeconds + " seconds";
        }

        // Status ek line mein
        public virtual string GetStatus()
        {
            string state = connected ? "CONNECTED" : "NOT CONNECTED";
            return name + " : " + state + " (simulated)  [" + GetRules() + "]";
        }

        // Upload ki simulation: true = kamyab, false = nakam
        // Rule: platform connected ho aur video max seconds se lambi na ho
        public virtual bool Upload(Video video)
        {
            if (!connected)
                return false;

            return video.Duration <= maxSeconds;
        }
    }
}
