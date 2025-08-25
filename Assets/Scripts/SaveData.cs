/// <summary>
/// A simple data class to hold settings for JSON serialization.
/// This class uses public fields instead of properties to be compatible with Unity's JsonUtility.
/// </summary>
[System.Serializable]
public class SaveData
{
    public bool IsFullScreenOn = true;
    public bool IsVSyncOn = true;
    public bool IsMutedOn = false;
    public float MasterVolume = 1.0f;
    public float MusicVolume = 0.8f;
    public float UISoundsVolume = 1.0f;
    public int ResolutionIndex = 0;
    public int QualitySetting = 2; // Default to "Medium" or similar
}
