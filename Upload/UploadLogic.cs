using System;
using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 2 : BUSINESS LOGIC =====
    // Upload ki simulation chalana aur uska record save karna.
    // platform.Upload(video) mein POLYMORPHISM hai: TikTok/Instagram ka apna rule chalta hai.
    class UploadLogic
    {
        UploadDataAccess dataAccess = new UploadDataAccess();

        int NextId(List<UploadRecord> list)
        {
            int max = 0;
            foreach (UploadRecord r in list)
            {
                if (r.Id > max)
                    max = r.Id;
            }
            return max + 1;
        }

        public int Count()
        {
            return dataAccess.LoadAll().Count;
        }

        // Ek video ek platform par "upload" karna (SIMULATION) aur record save karna
        public UploadRecord Upload(Video video, Platform platform)
        {
            bool ok = platform.Upload(video);

            string status;
            if (ok)
                status = "SUCCESS";
            else
                status = "FAILED";

            List<UploadRecord> list = dataAccess.LoadAll();
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            UploadRecord record = new UploadRecord(NextId(list), video.Id, video.OwnerId, platform.Name, status, now);

            list.Add(record);
            dataAccess.SaveAll(list);

            return record;
        }

        // Sirf is user ki history
        public List<UploadRecord> GetByOwner(int ownerId)
        {
            List<UploadRecord> result = new List<UploadRecord>();
            foreach (UploadRecord r in dataAccess.LoadAll())
            {
                if (r.OwnerId == ownerId)
                    result.Add(r);
            }
            return result;
        }
    }
}
