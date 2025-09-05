using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Benetti;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Basic Buttons for MVP functioning")]
    [SerializeField]private Button NewGame;
    [SerializeField]private Button Options;
    [SerializeField]private Button OpenPrompt;
    [SerializeField]private Button Yes;
    [SerializeField]private Button No;
    [SerializeField]private Button ExitOptionsButton;
    

    [Header("Stuff for animating exit prompt")]
    public RectTransform ExitPromptRectTransform;
    [SerializeField]private float originalPosition;
    public float newPosition;
    public float animationTime;
    
    [Header("UI Elements for MVP functioning")]
    [SerializeField]private RectTransform OptionsMenuUI;
    [SerializeField]private Vector2 optionsMenuSize;
    [SerializeField]private Vector2 optionsMenuOriginalSize;
    [SerializeField][Tooltip("Here you put all objects that you want to quickly fade out when closing options menu")]public RectTransform[] animatableObjectsMainOptions;
    [SerializeField][Tooltip("Here you put all objects that you want to quickly fade in/out under tabs in tabs")]public RectTransform[] animatableObjectsTabsOptions;
    [SerializeField] public RectTransform Tabs;
    void Start()
    {
        #if UNITY_EDITOR
        if (!SceneManager.GetSceneByName("PersistentScene").isLoaded && SceneManager.loadedSceneCount == 1)
            SceneManager.LoadSceneAsync((int)SceneEnum.PersistentScene, LoadSceneMode.Additive);
        #endif
        originalPosition = ExitPromptRectTransform.position.y;
        InitializeButtons();
    }

    #region Button Initialization

    public void InitializeButtons()
    {
        NewGame.onClick.AddListener(() => MainMenuHandler.ButtonClickHandler(BeginNewGame));
        Options.onClick.AddListener(()=>MainMenuHandler.ButtonClickHandler(OpenOptions));
        OpenPrompt.onClick.AddListener(() => MainMenuHandler.ButtonClickHandler(OpenExitPrompt));
        Yes.onClick.AddListener(()=>MainMenuHandler.ButtonClickHandler(ExitGame));
        No.onClick.AddListener(()=> MainMenuHandler.ButtonClickHandler(CloseExitPrompt));
        ExitOptionsButton.onClick.AddListener(()=>MainMenuHandler.ButtonClickHandler(ExitOptions));
    }
    public void BeginNewGame()
    {
        Debug.Log("Starting New Game!");
        GameManager.instance.LoadGame();
    }

    void OpenOptions()
    {
        Debug.Log("Opening Options!");
        StartCoroutine(OpenOptionsAnimationSequence());
    }

    void ExitOptions()
    {
        Debug.Log("Exiting Options!");
        StartCoroutine(CloseOptionsAnimationSequence());
      
    }
    void ExitGame()
    {
        Debug.Log("Exiting the Game");
        Application.Quit();
    }
    void OpenExitPrompt()
    {
        ExitPromptRectTransform.gameObject.SetActive(true);
        UIAnimations.SlideYUILinear(ExitPromptRectTransform, newPosition, animationTime);
    }
    void CloseExitPrompt()
    {
        UIAnimations.SlideYUILinearWithDisable(ExitPromptRectTransform, originalPosition, animationTime, null,
            () => { AudioManager.instance.PlaySound("ButtonClick"); });
    }

    #endregion
   
    IEnumerator CloseOptionsAnimationSequence()
    {
        for (int i = 0; i < animatableObjectsTabsOptions.Length; i++)
        {
            UIAnimations.FadeUI(animatableObjectsTabsOptions[i],0, 0.0001f);
            animatableObjectsTabsOptions[i].gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.01f);
        
        UIAnimations.SlideYUILinearWithDisable(Tabs, 0, animationTime);
        
        for (int i = 0; i < animatableObjectsMainOptions.Length; i++)
        {
            UIAnimations.FadeUI(animatableObjectsMainOptions[i], 0, 0.001f);
            animatableObjectsMainOptions[i].gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(animationTime);
        
        UIAnimations.ScaleUIWithDisable(OptionsMenuUI, optionsMenuOriginalSize, animationTime);
    }
    
    IEnumerator OpenOptionsAnimationSequence()
    {
        
        OptionsMenuUI.gameObject.SetActive(true);
        UIAnimations.ScaleUI(OptionsMenuUI, optionsMenuSize, animationTime);

        for (int i = 0; i < animatableObjectsMainOptions.Length; i++)
        {
            animatableObjectsMainOptions[i].gameObject.SetActive(true);
            UIAnimations.FadeUI(animatableObjectsMainOptions[i], 0.45f,animationTime);
        }
        yield return new WaitForSeconds(animationTime);
        
        Tabs.gameObject.SetActive(true);
        
        UIAnimations.SlideYUILinearCustomAction(Tabs, 100, animationTime, PopulatingOptions);
        
    }

    void PopulatingOptions()
    {
        for (int i = 0; i < animatableObjectsTabsOptions.Length; i++)
        {
            animatableObjectsTabsOptions[i].gameObject.SetActive(true);
            UIAnimations.FadeUI(animatableObjectsTabsOptions[i], 1, 0.0001f);
        }
    }
    
}
