using System;
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


    [Header("Stuff for animating exit prompt")]
    public RectTransform ExitPromptRectTransform;
    [SerializeField]private float originalPosition;
    public float newPosition;
    public float animationTime;
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
    }
    void BeginNewGame()
    {
        Debug.Log("Starting New Game!");
    }

    void OpenOptions()
    {
        Debug.Log("Opening Options!");
    }

    void ExitGame()
    {
        Debug.Log("Exiting the Game");
        Application.Quit();
    }
}
