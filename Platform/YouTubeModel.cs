namespace OneSpot
{
    // ===== LAYER 1 : MODEL =====
    // YouTube ek Platform hai (": Platform"). Naam aur max seconds parent ko bhejte hain: ": base(...)".
    class YouTube : Platform
    {
        public YouTube() : base("YouTube", 43200)
        {
        }

        // OVERRIDE: parent ka Connect badal kar apna message
        public override string Connect()
        {
            base.Connect();     // parent wala kaam (connected = true) pehle ho jata hai
            return "YouTube channel linked (simulation only, no real account used).";
        }
    }
}
