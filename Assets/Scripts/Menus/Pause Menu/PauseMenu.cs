using Benetti;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button ExitToMainMenuButton;
    [SerializeField] private Button ExitToDesktopButton;
    
    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(ResumeGame);
        OptionsButton.onClick.AddListener(OpenOptions);
        ExitToMainMenuButton.onClick.AddListener(ExitToMainMenu);
        ExitToDesktopButton.onClick.AddListener(ExitToDesktop);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenuUI.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseMenuUI.SetActive(true);
                GameStateManager.SetState(GameStateManager.GameState.Paused);
                Debug.Log("Game Paused!");
            }
        }
    }
    void ResumeGame()
    {
        Debug.Log("Resuming Game!");
        PauseMenuUI.SetActive(false);
        GameStateManager.SetState(GameStateManager.GameState.InGame);
    }
    void OpenOptions()
    {
        Debug.Log("Opening Options Menu!");
    }

    void ExitToMainMenu()
    {
        Debug.Log("Exiting to Main Menu!");
        SceneManager.LoadSceneAsync((int)SceneEnum.MainMenu, LoadSceneMode.Single);
        Time.timeScale = 1f; // Resume game time
    }
    void ExitToDesktop()
    {
        Application.Quit();
        Debug.Log("Exiting to Desktop!");
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }
}
