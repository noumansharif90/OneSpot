ONESPOT  -  VIDEO SHARING SIMULATOR  -  C#  -  4 LAYERS  -  FILE HANDLING (NO DATABASE)
=====================================================================================

WHAT IS ONESPOT?
  A console program where a user signs up, "connects" social media platforms (YouTube, TikTok,
  Instagram, Facebook, X), stores the INFORMATION of their videos, and SIMULATES uploading
  those videos to the connected platforms.

  IMPORTANT (honest simulation):
    - Connecting a platform does NOT log in to a real account.
    - Uploading does NOT publish a real video. No video file is stored, only its details.
    - "SUCCESS" or "FAILED" is decided by simple practice rules (see Entity_Platform.txt).
    - There is no Analytics anywhere in the project.


HOW TO RUN
  1. Double-click OneSpot.sln (opens Visual Studio).
  2. Press Ctrl+Alt+L to show Solution Explorer (the file list).
  3. Press Ctrl+F5 to run.
  Ordinary user : press 1 SIGN UP, then 2 SIGN IN.
  Administrator : main menu option 5.   TEST ACCOUNT:  admin / admin123
                  (written in the code on purpose for this class project; a real app never does that)


READ THE FILES IN THIS ORDER
  00_README_START_HERE.txt              this file
  01_Four_Layers_and_How_They_Connect   the most important file: who calls whom, and what "new" does
  02_Constructors_and_Base              every constructor in the project, and ": base(...)"
  03_Inheritance_Override_Polymorphism  the Platform family (virtual / override / base)
  Entity_Program_Dashboard_Helper       Program.cs, DashboardUI.cs, InputHelper.cs
  Entity_User                           the 4 User files
  Entity_Video                          the 4 Video files
  Entity_Platform                       the 8 Platform files
  Entity_Upload                         the 4 Upload files
  Entity_Admin                          the 2 Admin files
  05_List_and_File_Handling             lists, foreach, files, malformed lines
  06_Input_Validation                   safe input, TryParse, ToLower/Contains
  07_Testing_Checklist                  the 17 test scenarios, step by step
  08_Project_Folders_and_Create_In_VS   what bin/obj/.vs are, and how to create a project yourself


THE FILES AT A GLANCE
  Program.cs                      UI           main menu (starting point)
  Helper/InputHelper.cs           UI helper    safe keyboard input
  Dashboard/DashboardUI.cs        UI           the 11-option user dashboard
  User/UserModel.cs               Model
  User/UserDataAccess.cs          Data Access  users.txt
  User/UserLogic.cs               Logic        register, login, profile, password
  User/UserUI.cs                  UI           sign up, sign in, profile, change password
  Video/VideoModel.cs             Model
  Video/VideoDataAccess.cs        Data Access  videos.txt
  Video/VideoLogic.cs             Logic        add, edit, delete, search, ownership check
  Video/VideoUI.cs                UI           my videos, edit, delete, search
  Platform/PlatformModel.cs       Model        parent class Platform
  Platform/YouTubeModel.cs  TikTokModel.cs  InstagramModel.cs  FacebookModel.cs  XPlatformModel.cs
                                  Model        the 5 child classes
  Platform/PlatformDataAccess.cs  Data Access  platforms.txt
  Platform/PlatformLogic.cs       Logic        connect / disconnect
  Platform/PlatformUI.cs          UI           connect menu, status
  Upload/UploadModel.cs           Model        UploadRecord
  Upload/UploadDataAccess.cs      Data Access  upload_history.txt
  Upload/UploadLogic.cs           Logic        run the simulated upload, save the record
  Upload/UploadUI.cs              UI           upload video screen, upload history
  Admin/AdminLogic.cs             Logic        admin login check
  Admin/AdminUI.cs                UI           admin menu


THE FOUR TEXT FILES (the "database")
  Saved next to the program: <project>\bin\Debug\net8.0\   Open them in Notepad to see your data.
  Every line is one record. The pieces are separated by the "|" character.
  The "|" character is NOT allowed in anything the user types (the program says so and asks again),
  so the lines can always be cut apart safely.

  users.txt            id|fullName|username|password|email|age
                       1|Ali Khan|ali|pass1|ali@mail.com|20
  videos.txt           id|ownerId|title|description|category|duration|format|dateAdded
                       1|1|My Match|Best goals|Sports|120|Short Video (9:16)|2026-10-28
  platforms.txt        userId|platformName|connected
                       1|YouTube|True
  upload_history.txt   id|videoId|ownerId|platformName|status|dateTime
                       1|1|1|YouTube|SUCCESS|2026-10-28 14:30

  HOW THE FILES ARE RELATED (by numbers)
     videos.txt.ownerId         =  users.txt.id        (who owns the video)
     platforms.txt.userId       =  users.txt.id        (whose connection it is: each user has their own)
     upload_history.txt.ownerId =  users.txt.id
     upload_history.txt.videoId =  videos.txt.id       (which video the attempt was about)

  A user only ever sees lines where the owner number is their own id. That is how users are kept apart.
  If a file is missing, the program starts with an empty list. If a line is damaged it is skipped.
  Nothing is erased when the program starts. New records are added to the existing ones.
  To reset everything: close the program and delete the four txt files.

  What happens to upload history when a video is deleted?
     The history lines STAY (they are a record of practice attempts) and show "(video deleted)".
     Deleting in OneSpot never means deleting anything from a real platform.


WHERE EACH ASSIGNMENT-SPEC CLASS LIVES IN OUR C# VERSION
  Spec class           In this project
  User                 User/ (4 files)
  Video                Video/ (4 files)
  Platform (base)      Platform/PlatformModel.cs      (a normal class with virtual methods)
  YouTube ... XPlatform  Platform/*Model.cs
  UploadManager        Upload/UploadLogic.cs + UploadUI.cs
  FileManager          the four *DataAccess.cs files (each entity has its own, that is layer 4)
  OneSpot (workflow)   Program.cs + Dashboard/DashboardUI.cs


WHICH OOP CONCEPT IS WHERE
  Classes and objects        every class; objects made with "new"
  Encapsulation              private fields + properties in all Model files
                             (User.Id has only "get", Platform.Name has only "get")
  Constructors               every Model file (see 02_Constructors_and_Base)
  Inheritance                YouTube : Platform, TikTok : Platform, ... (see 03)
  Override                   Connect() in all five children, Upload() and GetRules() in TikTok/Instagram,
                             ToString() in User and Video
  Polymorphism               List<Platform> holds the five different children, and p.Connect() / p.Upload(video)
                             runs the right child's version
  Association                Platform.Upload(Video video) receives a Video; UploadRecord points to a Video and a User by their ids
  File handling              the four *DataAccess.cs files
  List / array               List<User>, List<Video>, List<Platform>...; the categories are a string array
  Exception handling         try/catch around file reading/writing (IOException)


KNOWN LIMITS (kept simple on purpose, because this is beginner level)
  - Passwords are saved as plain text in users.txt. This is a classroom simplification.
    A real application must store a hash, never the password itself.
  - The administrator account is a test account written in the code.
  - If the newest video is deleted, its id number can be given to the next new video, and an old
    history line could then show the new video's title. A real database uses ids that are never reused.
  - Platform rules (max seconds, short-only) are invented practice rules, not the real platforms' rules.
  - The program does not mask the password while typing.
