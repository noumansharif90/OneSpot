namespace OneSpot
{
    // ===== LAYER 1 : MODEL =====
    // X ek Platform hai (": Platform"). Naam aur max seconds parent ko bhejte hain: ": base(...)".
    class XPlatform : Platform
    {
        public XPlatform() : base("X", 140)
        {
        }

        // OVERRIDE: parent ka Connect badal kar apna message
        public override string Connect()
        {
            base.Connect();     // parent wala kaam (connected = true) pehle ho jata hai
            return "X account linked (simulation only, no real account used).";
        }
    }
}
