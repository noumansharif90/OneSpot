namespace OneSpot
{
    // ===== LAYER 1 : MODEL =====
    // Facebook ek Platform hai (": Platform"). Naam aur max seconds parent ko bhejte hain: ": base(...)".
    class Facebook : Platform
    {
        public Facebook() : base("Facebook", 14400)
        {
        }

        // OVERRIDE: parent ka Connect badal kar apna message
        public override string Connect()
        {
            base.Connect();     // parent wala kaam (connected = true) pehle ho jata hai
            return "Facebook page linked (simulation only, no real account used).";
        }
    }
}
