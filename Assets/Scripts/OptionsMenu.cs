using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Benetti; // Import the Benetti namespace to use the helper

/// <summary>
/// Manages the options menu UI and functionality.
/// Relies on JsonDataHelper.cs for saving and loading settings.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [Header("UI Element References")]
    public Toggle FullscreenToggle;
    public Toggle VSyncToggle;
    public Toggle MuteAudioToggle;
    public TMP_Dropdown QualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Slider UiSoundsSlider;
    public Slider MasterVolumeSlider;
    public Slider MusicSlider;

    [Header("Audio")]
    public AudioMixer Mixer;

    private SaveData _saveData;
    private bool _settingsChanged;

    private const string SAVE_FILE_NAME = "gamesettings.json";

    void Start()
    {
        _settingsChanged = false;
        LoadSettings(); // Load settings or create a default file

        // --- Initialize UI Listeners ---
        // These listeners update the settings in real-time and set the "dirty flag"
        QualityDropdown.onValueChanged.AddListener(index => {
            QualitySettings.SetQualityLevel(index, true);
            _settingsChanged = true;
        });
        FullscreenToggle.onValueChanged.AddListener(value => {
            Screen.fullScreenMode = value ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            _settingsChanged = true;
        });
        VSyncToggle.onValueChanged.AddListener(value => {
            QualitySettings.vSyncCount = value ? 1 : 0;
            _settingsChanged = true;
        });

        // Listeners for sliders and toggles that call the public methods below
        MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        UiSoundsSlider.onValueChanged.AddListener(SetUISoundVolume);
        MuteAudioToggle.onValueChanged.AddListener(SetMute);
    }

    /// <summary>
    /// A good place to save is when the menu is closed/disabled.
    /// This method is automatically called by Unity when the GameObject is disabled.
    /// </summary>
    void OnDisable()
    {
        if (_settingsChanged)
        {
            SaveSettings();
            _settingsChanged = false; // Reset flag
        }
    }

    /// <summary>
    /// Saves the current UI settings to a file using the JsonDataHelper.
    /// </summary>
    public void SaveSettings()
    {
        // Populate the SaveData object from the current UI state
        _saveData.IsFullScreenOn = FullscreenToggle.isOn;
        _saveData.IsVSyncOn = VSyncToggle.isOn;
        _saveData.IsMutedOn = MuteAudioToggle.isOn;
        _saveData.MasterVolume = MasterVolumeSlider.value;
        _saveData.MusicVolume = MusicSlider.value;
        _saveData.UISoundsVolume = UiSoundsSlider.value;
        _saveData.ResolutionIndex = resolutionDropdown.value;
        _saveData.QualitySetting = QualityDropdown.value;

        // Use the helper to save the data to a file
        JsonDataHelper.Save(_saveData, SAVE_FILE_NAME);
        Debug.Log("Settings saved.");
    }

    /// <summary>
    /// Loads settings from a file using the JsonDataHelper and applies them to the UI.
    /// </summary>
    public void LoadSettings()
    {
        // Use the helper to load the data
        _saveData = JsonDataHelper.Load<SaveData>(SAVE_FILE_NAME);

        // Apply the loaded settings to the UI
        FullscreenToggle.isOn = _saveData.IsFullScreenOn;
        VSyncToggle.isOn = _saveData.IsVSyncOn;
        MuteAudioToggle.isOn = _saveData.IsMutedOn;
        MasterVolumeSlider.value = _saveData.MasterVolume;
        MusicSlider.value = _saveData.MusicVolume;
        UiSoundsSlider.value = _saveData.UISoundsVolume;
        resolutionDropdown.value = _saveData.ResolutionIndex;
        QualityDropdown.value = _saveData.QualitySetting;

        // Ensure audio is updated on load to reflect saved state
        SetMasterVolume(_saveData.MasterVolume);
        SetMusicVolume(_saveData.MusicVolume);
        SetUISoundVolume(_saveData.UISoundsVolume);
        SetMute(_saveData.IsMutedOn);
    }

    // --- Public methods to be called by listeners ---
    public void SetMasterVolume(float value) { Mixer.SetFloat("MasterVolume", value > 0 ? Mathf.Log10(value) * 20 : -80f); _settingsChanged = true; }
    public void SetMusicVolume(float value) { Mixer.SetFloat("MusicVolume", value > 0 ? Mathf.Log10(value) * 20 : -80f); _settingsChanged = true; }
    public void SetUISoundVolume(float value) { Mixer.SetFloat("UIsoundsVolume", value > 0 ? Mathf.Log10(value) * 20 : -80f); _settingsChanged = true; }
    public void SetMute(bool isMuted) { Mixer.SetFloat("MasterVolume", isMuted ? -80f : Mathf.Log10(MasterVolumeSlider.value) * 20); _settingsChanged = true; }
}
