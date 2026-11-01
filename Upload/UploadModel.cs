namespace OneSpot
{
    // ===== LAYER 1 : MODEL =====
    // Ek upload koshish ka record (SIMULATED). Video ka record alag rehta hai,
    // is liye ek video kai platforms par jaye to video dobara save nahi hoti, sirf yeh records barhte hain.
    class UploadRecord
    {
        private int id;
        private int videoId;
        private int ownerId;
        private string platformName;
        private string status;          // "SUCCESS" ya "FAILED"
        private string dateTime;

        public int Id
        {
            get { return id; }
        }

        public int VideoId
        {
            get { return videoId; }
        }

        public int OwnerId
        {
            get { return ownerId; }
        }

        public string PlatformName
        {
            get { return platformName; }
        }

        public string Status
        {
            get { return status; }
        }

        public string DateTime
        {
            get { return dateTime; }
        }

        public UploadRecord(int id, int videoId, int ownerId, string platformName, string status, string dateTime)
        {
            this.id = id;
            this.videoId = videoId;
            this.ownerId = ownerId;
            this.platformName = platformName;
            this.status = status;
            this.dateTime = dateTime;
        }
    }
}
