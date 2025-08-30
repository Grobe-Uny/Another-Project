/// <summary>
/// A simple data class to hold settings for JSON serialization.
/// This class uses public fields instead of properties to be compatible with Unity's JsonUtility.
/// </summary>
[System.Serializable]
public class SaveData
{
   public bool isVsyncOn;
   public bool isFullScreenOn;
}
