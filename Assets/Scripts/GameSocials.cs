using UnityEngine;
using GooglePlayGames;
using TheWasteland.Plugins;
using Universal.Singletons;

namespace TheWasteland.SocialNet
{
    public class GameSocials : MonoBehaviourSingleton<GameSocials>
    {
        string leaderboardID = "";
#if UNITY_ANDROID || PLATFORM_ANDROID
        public static PlayGamesPlatform platform = null;
#endif

        void Start()
        {
            LoggerPlugin.inst.RegisterLog("Entered game socials");
#if UNITY_ANDROID || PLATFORM_ANDROID
            LoggerPlugin.inst.RegisterLog("Entered android preprocess if");
            LoggerPlugin.inst.RegisterLog("Has Platform 1: " + (platform == null ? "No" : "Yes"));
            if (platform == null)
            {
                // //NOT SUPPORTED ANYMORE
                // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                // PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.DebugLogEnabled = true;
                platform = PlayGamesPlatform.Activate();
                LoggerPlugin.inst.RegisterLog("GPGS - Play Games activated successfully");
            }
            LoggerPlugin.inst.RegisterLog("Has Platform 2: " + (platform == null ? "No" : "Yes"));
#endif

            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    LoggerPlugin.inst.RegisterLog("GameSocials - Logged in successfully");
                    LoggerPlugin.inst.RegisterLog("GameSocials - ID: " + Social.localUser.id);
                    LoggerPlugin.inst.RegisterLog("GameSocials - Name: " + Social.localUser.userName);
                }
                else
                {
                    LoggerPlugin.inst.RegisterLog("GameSocials - Failed to login");
                }
            });
            
            UnlockAchievement(GPGSIds.achievement_game_started);
        }

        public void AddScoreToLeaderboard(int score)
        {
            if (score % 5 != 0)
            {
                LoggerPlugin.inst.RegisterLog("GameSocials ERROR - Score must be a multiple of 5");
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
        public void UnlockAchievement(string id)
        {
            if (Social.localUser.authenticated)
                Social.ReportProgress(id, 100f, success => { });
            else
                LoggerPlugin.inst.RegisterLog("GameSocials - Failed to unlock achievement");
        }
    }
}