using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Benetti;

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
    public OptionsMenu optionsMenu;
    [SerializeField]public RectTransform[] animatableObjects;
    [SerializeField] public RectTransform Tabs;
    void Start()
    {
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
        UIAnimations.SlideYUILinearWithDisable(Tabs, 0, animationTime);
        for (int i = 0; i < animatableObjects.Length; i++)
        {
            UIAnimations.FadeUI(animatableObjects[i], 0, 0.001f);
            animatableObjects[i].gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(animationTime);
        
        UIAnimations.ScaleUIWithDisable(OptionsMenuUI, optionsMenuOriginalSize, animationTime);
    }
    
    IEnumerator OpenOptionsAnimationSequence()
    {
        
        OptionsMenuUI.gameObject.SetActive(true);
        UIAnimations.ScaleUI(OptionsMenuUI, optionsMenuSize, animationTime);
        
        for (int i = 0; i < animatableObjects.Length; i++)
        {
            animatableObjects[i].gameObject.SetActive(true);
            UIAnimations.FadeUI(animatableObjects[i], 0.45f,animationTime);
        }
        yield return new WaitForSeconds(animationTime);
        
        Tabs.gameObject.SetActive(true);
        
        UIAnimations.SlideYUILinear(Tabs, 100, animationTime);
    }
}
