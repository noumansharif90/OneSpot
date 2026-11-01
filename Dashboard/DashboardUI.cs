using System;

namespace OneSpot
{
    // ===== LAYER 3 : UI =====
    // Login ke baad ka user dashboard. Sirf menu dikhata hai aur sahi screen ko bulata hai.
    // Logged-in user ek parameter (User user) ke zariye har screen ko diya jata hai.
    // Logout par yeh method khatam ho jata hai, is liye user ka data aage nahi bachta.
    class DashboardUI
    {
        UserUI userUI = new UserUI();
        PlatformUI platformUI = new PlatformUI();
        UploadUI uploadUI = new UploadUI();
        VideoUI videoUI = new VideoUI();

        public void Start(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== DASHBOARD : " + user.FullName + " ==========");
                Console.WriteLine("1. MY PROFILE");
                Console.WriteLine("2. CONNECT PLATFORMS");
                Console.WriteLine("3. VIEW PLATFORM STATUS");
                Console.WriteLine("4. UPLOAD VIDEO");
                Console.WriteLine("5. MY VIDEOS");
                Console.WriteLine("6. EDIT VIDEO");
                Console.WriteLine("7. DELETE VIDEO");
                Console.WriteLine("8. SEARCH VIDEOS");
                Console.WriteLine("9. UPLOAD HISTORY");
                Console.WriteLine("10. CHANGE PASSWORD");
                Console.WriteLine("11. LOGOUT");

                int op = InputHelper.ReadInt("Select an option: ");

                if (op == 1) userUI.Profile(user);
                else if (op == 2) platformUI.ConnectMenu(user);
                else if (op == 3) platformUI.ViewStatus(user);
                else if (op == 4) uploadUI.UploadVideo(user);
                else if (op == 5) videoUI.MyVideos(user);
                else if (op == 6) videoUI.EditVideo(user);
                else if (op == 7) videoUI.DeleteVideo(user);
                else if (op == 8) videoUI.SearchVideos(user);
                else if (op == 9) uploadUI.ShowHistory(user);
                else if (op == 10) userUI.ChangePassword(user);
                else if (op == 11)
                {
                    Console.WriteLine("Logged out.");
                    InputHelper.Pause();
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong choice!");
                    InputHelper.Pause();
                }
            }
        }
    }
}
