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
        if (!SceneManager.GetSceneByName("ManagerLoader").isLoaded)
            SceneManager.LoadSceneAsync((int)SceneEnum.ManagerHolder, LoadSceneMode.Additive);
        #endif
        originalPosition = ExitPromptRectTransform.position.y;
        NewGame.onClick.AddListener(()=>BeginNewGame());
        Options.onClick.AddListener(()=>OpenOptions());
        OpenPrompt.onClick.AddListener(() =>
        {
            ExitPromptRectTransform.gameObject.SetActive(true);
            UIAnimations.SlideYUILinear(ExitPromptRectTransform, newPosition, animationTime);
        });
        Yes.onClick.AddListener(()=>ExitGame());
        No.onClick.AddListener(()=> { UIAnimations.SlideYUILinearWithDisable(ExitPromptRectTransform, originalPosition, animationTime); });
        ExitOptionsButton.onClick.AddListener(()=>ExitOptions());
    }
    void BeginNewGame()
    {
        Debug.Log("Starting New Game!");
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
        
        UIAnimations.SlideYUILinear(Tabs, 100, animationTime);
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < animatableObjectsTabsOptions.Length; i++)
        {
            animatableObjectsTabsOptions[i].gameObject.SetActive(true);
            UIAnimations.FadeUI(animatableObjectsTabsOptions[i], 1, 0.0001f);
        }
    }
}
