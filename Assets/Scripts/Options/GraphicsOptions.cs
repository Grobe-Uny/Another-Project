using Benetti;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsOptions : MonoBehaviour
{
    [Header("UI connected to settings")]
    public Toggle VsyncToggle;
    public Toggle FullScreenToggle;
    
    
    
    [Header("Save Data related stuff")]
    private bool _hasSettingChanged;
    [SerializeField]public SaveData saveData;
    public const string saveFile = "Settings.json";
    void Start()
    {
        _hasSettingChanged = false;
        LoadSettings();
        VsyncToggle.onValueChanged.AddListener(value => {
            QualitySettings.vSyncCount = value ? 1 : 0;
            _hasSettingChanged = true;
        });
        FullScreenToggle.onValueChanged.AddListener(value => {
            Screen.fullScreenMode = value ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            _hasSettingChanged = true;
        });
    }
        /// <summary>
        /// A good place to save is when the menu is closed/disabled.
        /// This method is automatically called by Unity when the GameObject is disabled.
        /// </summary>
        void OnDisable()
        {
            if (_hasSettingChanged)
            {
                SaveSettings();
                _hasSettingChanged = false; // Reset flag
            }
        }

    void SaveSettings()
    {
        if (!_hasSettingChanged)
            return;

        saveData.isVsyncOn = VsyncToggle.isOn;
        saveData.isFullScreenOn = FullScreenToggle.isOn;
        
        
        JsonDataHelper.Save(saveData, saveFile);

    }

    void LoadSettings()
    {
        saveData = JsonDataHelper.Load<SaveData>(saveFile);

        VsyncToggle.isOn = saveData.isVsyncOn;
        FullScreenToggle.isOn = saveData.isFullScreenOn;
    }
}
