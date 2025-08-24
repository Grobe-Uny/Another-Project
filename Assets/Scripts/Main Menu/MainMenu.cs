using System;
using UnityEngine;
using UnityEngine.UI;
using Benetti;

public class MainMenu : MonoBehaviour
{
    [Header("Basic Buttons for MVP functioning")]
    [SerializeField]private Button NewGame;
    [SerializeField]private Button Options;
    [SerializeField]private Button Exit;
    // Start is called before the first frame update
    void Start()
    {
        NewGame.onClick.AddListener(()=>BeginNewGame());
        Options.onClick.AddListener(()=>OpenOptions());
        Exit.onClick.AddListener(()=>ExitGame());
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
