using System;
using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 3 : UI =====
    // Video ki screens: my videos, edit, delete, search.
    // Sirf input lena aur output dikhana. Rules VideoLogic mein hain.
    class VideoUI
    {
        VideoLogic videoLogic = new VideoLogic();

        // ---------- Chhote helper methods (UploadUI bhi inhein use karti hai) ----------

        // Category ki list dikha kar number se chunwana
        public string AskCategory()
        {
            string[] categories = videoLogic.GetCategories();

            Console.WriteLine("Categories:");
            for (int i = 0; i < categories.Length; i++)
                Console.WriteLine("  " + (i + 1) + ". " + categories[i]);

            while (true)
            {
                int number = InputHelper.ReadInt("Select category number: ");
                string category = videoLogic.GetCategoryByNumber(number);

                if (category != null)
                    return category;

                Console.WriteLine("Invalid category number.");
            }
        }

        // Format chunwana
        public string AskFormat()
        {
            Console.WriteLine("Formats:");
            Console.WriteLine("  1. Short Video (9:16)");
            Console.WriteLine("  2. Long Video (16:9)");

            while (true)
            {
                int number = InputHelper.ReadInt("Select format number: ");
                string format = videoLogic.GetFormatByNumber(number);

                if (format != null)
                    return format;

                Console.WriteLine("Invalid format number.");
            }
        }

        void ShowDetails(Video v)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Video ID    : " + v.Id);
            Console.WriteLine("Title       : " + v.Title);
            Console.WriteLine("Description : " + v.Description);
            Console.WriteLine("Category    : " + v.Category);
            Console.WriteLine("Duration    : " + v.Duration + " seconds");
            Console.WriteLine("Format      : " + v.Format);
            Console.WriteLine("Date added  : " + v.DateAdded);
            Console.WriteLine("----------------------------------");
        }

        // ---------- 5. My Videos ----------
        public void MyVideos(User user)
        {
            Console.Clear();
            Console.WriteLine("========== MY VIDEOS ==========");

            List<Video> list = videoLogic.GetByOwner(user.Id);

            if (list.Count == 0)
            {
                Console.WriteLine("You have no videos yet. Use \"Upload Video\" to add one.");
                InputHelper.Pause();
                return;
            }

            foreach (Video v in list)
                Console.WriteLine(v);   // ToString override chalta hai

            Console.WriteLine();
            int id = InputHelper.ReadInt("Enter a video ID to see full details (0 = back): ");

            if (id == 0)
                return;

            Video chosen = videoLogic.FindOwnedById(id, user.Id);

            if (chosen == null)
                Console.WriteLine("Video not found (or it is not yours).");
            else
                ShowDetails(chosen);

            InputHelper.Pause();
        }

        // ---------- 6. Edit Video ----------
        public void EditVideo(User user)
        {
            Console.Clear();
            Console.WriteLine("========== EDIT VIDEO ==========");

            int id = InputHelper.ReadInt("Enter the video ID to edit (0 = cancel): ");

            if (id == 0)
                return;

            Video v = videoLogic.FindOwnedById(id, user.Id);

            if (v == null)
            {
                Console.WriteLine("Video not found (or it is not yours).");
                InputHelper.Pause();
                return;
            }

            ShowDetails(v);
            Console.WriteLine("1. CHANGE TITLE");
            Console.WriteLine("2. CHANGE DESCRIPTION");
            Console.WriteLine("3. CHANGE CATEGORY");
            Console.WriteLine("4. CHANGE FORMAT");
            Console.WriteLine("5. CANCEL");

            int op = InputHelper.ReadInt("Select an option: ");
            bool done = false;

            if (op == 1)
                done = videoLogic.EditTitle(id, user.Id, InputHelper.ReadText("New title: "));
            else if (op == 2)
                done = videoLogic.EditDescription(id, user.Id, InputHelper.ReadText("New description: "));
            else if (op == 3)
                done = videoLogic.EditCategory(id, user.Id, AskCategory());
            else if (op == 4)
                done = videoLogic.EditFormat(id, user.Id, AskFormat());
            else if (op == 5)
            {
                Console.WriteLine("Cancelled.");
                InputHelper.Pause();
                return;
            }
            else
            {
                Console.WriteLine("Wrong choice!");
                InputHelper.Pause();
                return;
            }

            if (done)
                Console.WriteLine("Video updated.");
            else
                Console.WriteLine("Could not update the video.");

            InputHelper.Pause();
        }

        // ---------- 7. Delete Video ----------
        public void DeleteVideo(User user)
        {
            Console.Clear();
            Console.WriteLine("========== DELETE VIDEO ==========");

            int id = InputHelper.ReadInt("Enter the video ID to delete (0 = cancel): ");

            if (id == 0)
                return;

            Video v = videoLogic.FindOwnedById(id, user.Id);

            if (v == null)
            {
                Console.WriteLine("Video not found (or it is not yours).");
                InputHelper.Pause();
                return;
            }

            Console.WriteLine("You are about to delete: " + v.Title);

            if (!InputHelper.AskYesNo("Are you sure? (y/n): "))
            {
                Console.WriteLine("Nothing was deleted.");
                InputHelper.Pause();
                return;
            }

            if (videoLogic.Delete(id, user.Id))
            {
                Console.WriteLine("Video deleted from OneSpot.");
                Console.WriteLine("(Simulated upload history stays, and shows \"video deleted\". Nothing was removed from any real platform.)");
            }
            else
                Console.WriteLine("Could not delete the video.");

            InputHelper.Pause();
        }

        // ---------- 8. Search Videos ----------
        public void SearchVideos(User user)
        {
            Console.Clear();
            Console.WriteLine("========== SEARCH VIDEOS ==========");
            Console.WriteLine("1. BY TITLE (full or part)");
            Console.WriteLine("2. BY CATEGORY");
            Console.WriteLine("3. BY VIDEO ID");
            Console.WriteLine("4. BACK");

            int op = InputHelper.ReadInt("Select an option: ");
            List<Video> found = new List<Video>();

            if (op == 1)
            {
                string text = InputHelper.ReadText("Enter title text: ");
                found = videoLogic.SearchByTitle(user.Id, text);
            }
            else if (op == 2)
            {
                string category = AskCategory();
                found = videoLogic.SearchByCategory(user.Id, category);
            }
            else if (op == 3)
            {
                int id = InputHelper.ReadInt("Enter video ID: ");
                Video v = videoLogic.FindOwnedById(id, user.Id);
                if (v != null)
                    found.Add(v);
            }
            else if (op == 4)
            {
                return;
            }
            else
            {
                Console.WriteLine("Wrong choice!");
                InputHelper.Pause();
                return;
            }

            Console.WriteLine();
            if (found.Count == 0)
                Console.WriteLine("No matching videos found.");

            foreach (Video v in found)
                Console.WriteLine(v);

            InputHelper.Pause();
        }
    }
}
