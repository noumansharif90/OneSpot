using System;
using System.Collections.Generic;

namespace OneSpot
{
    // ===== LAYER 3 : UI =====
    // Upload Video aur Upload History ki screens.
    // YAAD RAHE: yeh simulation hai. Koi asli video kisi asli platform par publish nahi hoti.
    class UploadUI
    {
        VideoLogic videoLogic = new VideoLogic();
        VideoUI videoUI = new VideoUI();
        PlatformLogic platformLogic = new PlatformLogic();
        UploadLogic uploadLogic = new UploadLogic();

        // ---------- 4. Upload Video ----------
        public void UploadVideo(User user)
        {
            Console.Clear();
            Console.WriteLine("========== UPLOAD VIDEO (SIMULATION) ==========");

            // 1. video ki details
            string title = InputHelper.ReadText("Video title: ");
            string description = InputHelper.ReadText("Description: ");
            string category = videoUI.AskCategory();

            int duration = InputHelper.ReadInt("Duration in seconds: ");
            while (!videoLogic.IsValidDuration(duration))
            {
                Console.WriteLine("Duration must be between 1 and 43200 seconds.");
                duration = InputHelper.ReadInt("Duration in seconds: ");
            }

            string format = videoUI.AskFormat();

            // 2. platforms chunna
            List<Platform> platforms = platformLogic.GetPlatforms(user.Id);
            List<Platform> chosen = ChoosePlatforms(platforms);

            // 3. confirm
            Console.WriteLine();
            if (chosen.Count == 0)
            {
                Console.WriteLine("No platform selected (or none is connected).");

                if (!InputHelper.AskYesNo("Save the video in OneSpot without uploading? (y/n): "))
                {
                    Console.WriteLine("Cancelled. Nothing was saved.");
                    InputHelper.Pause();
                    return;
                }
            }
            else
            {
                if (!InputHelper.AskYesNo("Save the video and simulate upload to " + chosen.Count + " platform(s)? (y/n): "))
                {
                    Console.WriteLine("Cancelled. Nothing was saved.");
                    InputHelper.Pause();
                    return;
                }
            }

            // 4. video ek hi baar save hoti hai
            Video video = videoLogic.Add(user.Id, title, description, category, duration, format);

            if (video == null)
            {
                Console.WriteLine("Could not save the video.");
                InputHelper.Pause();
                return;
            }

            Console.WriteLine("Video saved with ID " + video.Id + ".");

            // 5. har chune hue platform par simulation
            foreach (Platform p in chosen)
            {
                UploadRecord record = uploadLogic.Upload(video, p);

                if (record.Status == "SUCCESS")
                    Console.WriteLine(p.Name + ": SUCCESS (simulated)");
                else
                    Console.WriteLine(p.Name + ": FAILED (simulated). Platform rules: " + p.GetRules());
            }

            if (chosen.Count > 0)
                Console.WriteLine("Note: nothing was published to a real platform.");

            InputHelper.Pause();
        }

        // Platforms dikha kar user se chunwana. Connected walay hi chun sakte hain
        List<Platform> ChoosePlatforms(List<Platform> platforms)
        {
            List<Platform> chosen = new List<Platform>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Platforms:");
                for (int i = 0; i < platforms.Count; i++)
                    Console.WriteLine("  " + (i + 1) + ". " + platforms[i].GetStatus());

                int number = InputHelper.ReadInt("Platform number to add (0 = done): ");

                if (number == 0)
                    break;

                if (number < 1 || number > platforms.Count)
                {
                    Console.WriteLine("Invalid platform number.");
                    continue;
                }

                Platform p = platforms[number - 1];

                if (!p.Connected)
                {
                    Console.WriteLine(p.Name + " is not connected. Connect it first from \"Connect Platforms\".");
                    continue;
                }

                if (chosen.Contains(p))
                {
                    Console.WriteLine(p.Name + " is already selected.");
                    continue;
                }

                chosen.Add(p);
                Console.WriteLine(p.Name + " selected.");
            }

            return chosen;
        }

        // ---------- 9. Upload History ----------
        public void ShowHistory(User user)
        {
            Console.Clear();
            Console.WriteLine("========== UPLOAD HISTORY (SIMULATED) ==========");
            Console.WriteLine("These are practice attempts. Nothing was published anywhere.");
            Console.WriteLine();

            List<UploadRecord> list = uploadLogic.GetByOwner(user.Id);

            if (list.Count == 0)
            {
                Console.WriteLine("No upload attempts yet.");
                InputHelper.Pause();
                return;
            }

            foreach (UploadRecord r in list)
            {
                Video v = videoLogic.FindById(r.VideoId);

                string title;
                if (v == null)
                    title = "(video deleted)";
                else
                    title = v.Title;

                Console.WriteLine("Record " + r.Id + " | Video " + r.VideoId + " \"" + title + "\" | " + r.PlatformName + " | " + r.Status + " (simulated) | " + r.DateTime);
            }

            InputHelper.Pause();
        }
    }
}
