using UnityEngine;
using GooglePlayGames;
using Universal.Singletons;

namespace TheWasteland.SocialNet
{
    public class GameSocials : MonoBehaviourSingleton<GameSocials>
    {
        string leaderboardID = "";
#if UNITY_ANDROID || PLATFORM_ANDROID
        public static PlayGamesPlatform platform = null;
#endif
        
        //Unity Events
        void Start()
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            if (platform == null)
            {
                // //NOT SUPPORTED ANYMORE
                // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                // PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.DebugLogEnabled = true;
                platform = PlayGamesPlatform.Activate();
                Debug.Log("GPGS - Play Games activated successfully");
            }
            else 
                Debug.Log("GPGS - Play Games activation failed");
#endif
            
            LogIn();
            
            UnlockAchievement(GPGSIds.achievement_game_started);
        }

        //Methods
        public void LogIn(bool logInFromPlatform = false)
        {
            if (logInFromPlatform)
            {
#if UNITY_ANDROID || PLATFORM_ANDROID
                platform.Authenticate(OnGoogleLogIn);
#endif
            }
            else
                Social.localUser.Authenticate(OnUnityLogIn);
        }
        public void AddScoreToLeaderboard(int score)
        {
            if (score % 5 != 0)
            {
                Debug.Log("GameSocials ERROR - Score must be a multiple of 5");
                return;
            }
            if (Social.localUser.authenticated)
            {
                Social.ReportScore(score, leaderboardID, success => { });
            }
        }
        public void ShowLeaderboard()
        {
            if (Social.localUser.authenticated)
            {
                platform.ShowLeaderboardUI();
            }
        }
        public void ShowAchievements()
        {
            if (Social.localUser.authenticated)
            {
                platform.ShowAchievementsUI();
            }
        }
        public void UnlockAchievement(string id, bool unlockFromPlatform = false)
        {
            if (unlockFromPlatform)
            {
#if UNITY_ANDROID || PLATFORM_ANDROID
                platform.UnlockAchievement(id);
#endif
                return;
            }
            
            if (Social.localUser.authenticated)
                Social.ReportProgress(id, 100f, success => { });
            else
                Debug.Log("GameSocials - Failed to unlock achievement");
            
        }
        void OnUnityLogIn(bool success)
        {
            if (success)
            {
                Debug.Log("GameSocials - Logged in successfully");
                Debug.Log("GameSocials - ID: " + Social.localUser.id);
                Debug.Log("GameSocials - Name: " + Social.localUser.userName);
            }
            else
            {
                Debug.Log("GameSocials - Failed to login");
            }
        }
#if UNITY_ANDROID || PLATFORM_ANDROID
        void OnGoogleLogIn(GooglePlayGames.BasicApi.SignInStatus signInStatus)
        {
            if (signInStatus == GooglePlayGames.BasicApi.SignInStatus.Success)
            {
                Debug.Log("GPGS - Logged in successfully");
                Debug.Log("GPGS - ID: " + platform.localUser.id);
                Debug.Log("GPGS - Name: " + platform.localUser.userName);
            }
            else
            {
                Debug.Log("GPGS - Failed to login: " + signInStatus);
            }
        }
#endif
    }
}