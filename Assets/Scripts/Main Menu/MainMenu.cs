using System;
using UnityEngine;
using UnityEngine.UI;
using Benetti;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button NewGame;
    [SerializeField] private Button Options;
    [SerializeField] private Button Exit;

    [Header("Menus")]
    [SerializeField] private AnimatedPanel optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        NewGame.onClick.AddListener(() => BeginNewGame());
        Options.onClick.AddListener(() => OpenOptions());
        Exit.onClick.AddListener(() => ExitGame());

        // Start with the options panel hidden
        if (optionsPanel != null)
        {
            optionsPanel.gameObject.SetActive(false);
        }
    }
    void BeginNewGame()
    {
        Debug.Log("Starting New Game!");
    }

    void OpenOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.Show();
        }
        else
        {
            Debug.LogWarning("Options Panel is not assigned in the MainMenu script.");
        }
    }

    void ExitGame()
    {
        Debug.Log("Exiting the Game");
        Application.Quit();
    }
}
