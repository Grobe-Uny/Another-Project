using Benetti;
using UnityEngine;
/// <summary>
/// This class handles the application of essential settings when the game first launches.
/// It runs automatically before any scene is loaded.
/// </summary>
public static class SettingsInitializer
{
    // This attribute tells Unity to run this method after the game has loaded
    // but before the first scene is loaded.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Initialize()
    {
        const string fileName = "Settings.json";

        SaveData saveData = JsonDataHelper.Load<SaveData>(fileName);

        QualitySettings.vSyncCount = saveData.isVsyncOn ? 1 : 0;
        Screen.fullScreenMode = saveData.isFullScreenOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        
    }
}
