using System;
using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 2 : BUSINESS LOGIC =====
    // Video ke rules: add, edit, delete, search, aur check ke video is user ki apni hai.
    // Is mein Console.WriteLine nahi hota. File ka kaam VideoDataAccess karti hai.
    class VideoLogic
    {
        VideoDataAccess dataAccess = new VideoDataAccess();

        // Categories ek array mein (array ka use)
        string[] categories = { "Gaming", "Education", "Comedy", "Sports", "Technology", "Islamic", "Entertainment", "Other" };

        public string[] GetCategories()
        {
            return categories;
        }

        // Number (1 se shuru) se category ka naam. Galat number ho to null
        public string GetCategoryByNumber(int number)
        {
            if (number >= 1 && number <= categories.Length)
                return categories[number - 1];
            return null;
        }

        // Number se format ka naam. Galat number ho to null
        public string GetFormatByNumber(int number)
        {
            if (number == 1) return "Short Video (9:16)";
            if (number == 2) return "Long Video (16:9)";
            return null;
        }

        // Duration 1 second se 12 ghante (43200 sec) tak theek hai
        public bool IsValidDuration(int seconds)
        {
            return seconds >= 1 && seconds <= 43200;
        }

        int NextId(List<Video> list)
        {
            int max = 0;
            foreach (Video v in list)
            {
                if (v.Id > max)
                    max = v.Id;
            }
            return max + 1;
        }

        public int Count()
        {
            return dataAccess.LoadAll().Count;
        }

        // Sirf is owner ki videos
        public List<Video> GetByOwner(int ownerId)
        {
            List<Video> result = new List<Video>();
            foreach (Video v in dataAccess.LoadAll())
            {
                if (v.OwnerId == ownerId)
                    result.Add(v);
            }
            return result;
        }

        // Id se video dhoondna (kisi ki bhi). History mein title dikhane ke liye
        public Video FindById(int id)
        {
            foreach (Video v in dataAccess.LoadAll())
            {
                if (v.Id == id)
                    return v;
            }
            return null;
        }

        // Id se video, lekin sirf agar owner wohi ho. Dusre user ki video ho to null
        // Yeh "kisi aur ki video na chhoo sako" wala rule hai
        public Video FindOwnedById(int id, int ownerId)
        {
            Video v = FindById(id);
            if (v != null && v.OwnerId == ownerId)
                return v;
            return null;
        }

        // Nayi video add karna. Save na ho saka to null
        public Video Add(int ownerId, string title, string description, string category, int duration, string format)
        {
            List<Video> list = dataAccess.LoadAll();

            string today = DateTime.Now.ToString("yyyy-MM-dd");
            Video v = new Video(NextId(list), ownerId, title, description, category, duration, format, today);

            list.Add(v);

            if (!dataAccess.SaveAll(list))
                return null;

            return v;
        }

        // ---------- Edit (har ek mein owner check hota hai) ----------
        public bool EditTitle(int id, int ownerId, string newTitle)
        {
            List<Video> list = dataAccess.LoadAll();
            foreach (Video v in list)
            {
                if (v.Id == id && v.OwnerId == ownerId)
                {
                    v.Title = newTitle;
                    return dataAccess.SaveAll(list);
                }
            }
            return false;
        }

        public bool EditDescription(int id, int ownerId, string newDescription)
        {
            List<Video> list = dataAccess.LoadAll();
            foreach (Video v in list)
            {
                if (v.Id == id && v.OwnerId == ownerId)
                {
                    v.Description = newDescription;
                    return dataAccess.SaveAll(list);
                }
            }
            return false;
        }

        public bool EditCategory(int id, int ownerId, string newCategory)
        {
            List<Video> list = dataAccess.LoadAll();
            foreach (Video v in list)
            {
                if (v.Id == id && v.OwnerId == ownerId)
                {
                    v.Category = newCategory;
                    return dataAccess.SaveAll(list);
                }
            }
            return false;
        }

        public bool EditFormat(int id, int ownerId, string newFormat)
        {
            List<Video> list = dataAccess.LoadAll();
            foreach (Video v in list)
            {
                if (v.Id == id && v.OwnerId == ownerId)
                {
                    v.Format = newFormat;
                    return dataAccess.SaveAll(list);
                }
            }
            return false;
        }

        // ---------- Delete ----------
        // Sirf owner delete kar sakta hai. Doosre users ki videos wesi hi rehti hain
        public bool Delete(int id, int ownerId)
        {
            List<Video> list = dataAccess.LoadAll();
            foreach (Video v in list)
            {
                if (v.Id == id && v.OwnerId == ownerId)
                {
                    list.Remove(v);
                    return dataAccess.SaveAll(list);   // remove ke fauran baad return (foreach ka rule)
                }
            }
            return false;
        }

        // ---------- Search (chhote bare harf ka farq nahi) ----------
        public List<Video> SearchByTitle(int ownerId, string text)
        {
            List<Video> result = new List<Video>();
            foreach (Video v in GetByOwner(ownerId))
            {
                if (v.Title.ToLower().Contains(text.Trim().ToLower()))
                    result.Add(v);
            }
            return result;
        }

        public List<Video> SearchByCategory(int ownerId, string category)
        {
            List<Video> result = new List<Video>();
            foreach (Video v in GetByOwner(ownerId))
            {
                if (v.Category.ToLower() == category.Trim().ToLower())
                    result.Add(v);
            }
            return result;
        }
    }
}
