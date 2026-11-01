using System;
using System.Collections.Generic;
using System.IO;

namespace OneSpot
{
    // ===== LAYER 4 : DATA ACCESS =====
    // videos.txt ko parhna aur likhna. Har line ek video, 8 cheezein "|" se alag:
    //      id|ownerId|title|description|category|duration|format|dateAdded
    // misal:   1|1|My Match|Best goals|Sports|120|Short Video (9:16)|2026-10-28
    class VideoDataAccess
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "videos.txt");

        public List<Video> LoadAll()
        {
            List<Video> list = new List<Video>();

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

                // kharab line chhor do
                if (parts.Length != 8)
                    continue;

                int id;
                int ownerId;
                int duration;
                if (!int.TryParse(parts[0], out id)) continue;
                if (!int.TryParse(parts[1], out ownerId)) continue;
                if (!int.TryParse(parts[5], out duration)) continue;

                list.Add(new Video(id, ownerId, parts[2], parts[3], parts[4], duration, parts[6], parts[7]));
            }

            return list;
        }

        public bool SaveAll(List<Video> list)
        {
            List<string> lines = new List<string>();

            foreach (Video v in list)
            {
                string line = v.Id + "|" + v.OwnerId + "|" + v.Title + "|" + v.Description + "|" + v.Category + "|" + v.Duration + "|" + v.Format + "|" + v.DateAdded;
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
