using System;
using System.Collections.Generic;
using System.IO;

namespace OneSpot
{
    // ===== LAYER 4 : DATA ACCESS =====
    // upload_history.txt. Har line ek (simulated) upload koshish, 6 cheezein "|" se alag:
    //      id|videoId|ownerId|platformName|status|dateTime
    // misal:   1|3|1|YouTube|SUCCESS|2026-10-28 14:30
    class UploadDataAccess
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "upload_history.txt");

        public List<UploadRecord> LoadAll()
        {
            List<UploadRecord> list = new List<UploadRecord>();

            if (!File.Exists(filePath))
                return list;

            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException)
            {
                return list;
            }

            foreach (string line in lines)
            {
                if (line.Trim() == "")
                    continue;

                string[] parts = line.Split('|');

                if (parts.Length != 6)
                    continue;

                int id;
                int videoId;
                int ownerId;
                if (!int.TryParse(parts[0], out id)) continue;
                if (!int.TryParse(parts[1], out videoId)) continue;
                if (!int.TryParse(parts[2], out ownerId)) continue;

                list.Add(new UploadRecord(id, videoId, ownerId, parts[3], parts[4], parts[5]));
            }

            return list;
        }

        public bool SaveAll(List<UploadRecord> list)
        {
            List<string> lines = new List<string>();

            foreach (UploadRecord r in list)
            {
                string line = r.Id + "|" + r.VideoId + "|" + r.OwnerId + "|" + r.PlatformName + "|" + r.Status + "|" + r.DateTime;
                lines.Add(line);
            }

            try
            {
                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
