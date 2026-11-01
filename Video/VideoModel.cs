namespace OneSpot
{
    // ===== LAYER 1 : MODEL =====
    // Video ka data (sirf information, asli video file save nahi hoti).
    // Har video ka ek owner hota hai: ownerId (jis user ne banayi).
    class Video
    {
        private int id;
        private int ownerId;
        private string title;
        private string description;
        private string category;
        private int duration;       // seconds mein
        private string format;      // "Short Video (9:16)" ya "Long Video (16:9)"
        private string dateAdded;

        public int Id
        {
            get { return id; }
        }

        public int OwnerId
        {
            get { return ownerId; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public int Duration
        {
            get { return duration; }
        }

        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        public string DateAdded
        {
            get { return dateAdded; }
        }

        // Constructor
        public Video(int id, int ownerId, string title, string description, string category, int duration, string format, string dateAdded)
        {
            this.id = id;
            this.ownerId = ownerId;
            this.title = title;
            this.description = description;
            this.category = category;
            this.duration = duration;
            this.format = format;
            this.dateAdded = dateAdded;
        }

        // OVERRIDE: video ko print karo to ek line mein khulasa nazar aata hai
        public override string ToString()
        {
            return "ID: " + id + " | " + title + " | " + category + " | " + duration + " sec | " + format + " | " + dateAdded;
        }
    }
}
