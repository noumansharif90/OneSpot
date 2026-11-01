namespace OneSpot
{
    // ===== LAYER 1 : MODEL =====
    // TikTok ek Platform hai (": Platform"). Yeh sirf Short Video (9:16) leta hai (simulation rule).
    class TikTok : Platform
    {
        public TikTok() : base("TikTok", 600)
        {
        }

        // OVERRIDE: Connect ka apna message
        public override string Connect()
        {
            base.Connect();
            return "TikTok account linked (simulation only, no real account used).";
        }

        // OVERRIDE: Upload mein ek extra rule: sirf Short Video
        public override bool Upload(Video video)
        {
            if (!video.Format.StartsWith("Short"))
                return false;

            return base.Upload(video);      // baqi rules parent ke (connected + max seconds)
        }

        // OVERRIDE: rules ke text mein extra baat jorna
        public override string GetRules()
        {
            return base.GetRules() + ", Short Video (9:16) only";
        }
    }
}
