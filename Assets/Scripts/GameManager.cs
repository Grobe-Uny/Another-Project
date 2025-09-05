using System.Collections;
using System.Collections.Generic;
using Benetti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject LoadingScreen;
    public Slider loadingBar;
    
    
    [Space]
    [SerializeField]private RectTransform loadingScreenLeft, loadingScreenRight;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            Debug.Log("GameManager instance created");
        }
        else
        {
            Debug.LogWarning("Multiple GameManager instances detected. Destroying duplicate.");
            Destroy(this);
            return;
        }

        if (!SceneManager.GetSceneByBuildIndex((int)SceneEnum.MainMenu)
            .isLoaded && !SceneManager.GetSceneByBuildIndex((int)SceneEnum.PrototypingScene).isLoaded)
            SceneManager.LoadSceneAsync((int)SceneEnum.MainMenu, LoadSceneMode.Additive);
    }
    
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    /*public void LoadGame()
    {
        LoadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneEnum.MainMenu));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneEnum.PrototypingScene, LoadSceneMode.Additive));
        
        StartCoroutine(GetSceneLoadProgress());
    }*/
    
    public void LoadGame()
    {
        /*RectTransform[] loadingScreenParts =  { loadingScreenLeft, loadingScreenRight };
        UIAnimations.SlideXUIsLinearCustomActions(loadingScreenParts, 0, 2f, () => { LoadingScreen.SetActive(true); },
            () => { });*/
     LoadingScreen.SetActive(true);

        // Unload MainMenu i Loadaj Gameplay scenu
        scenesLoading.Clear();
        scenesLoading.AddRange(SceneHandler.UnloadScenesAsync(new int[] { (int)SceneEnum.MainMenu }));
        scenesLoading.AddRange(SceneHandler.LoadScenesAsync(new int[] { (int)SceneEnum.PrototypingScene }));

        StartCoroutine(GetSceneLoadProgress());
    }

    private float totalSceneProgress;
    IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0f;
                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                loadingBar.value = Mathf.RoundToInt(totalSceneProgress / 100f);
                // Here you can update a loading bar or text with totalProgress / scenesLoading.Count
                Debug.Log($"Loading progress: { totalSceneProgress / scenesLoading.Count * 100}%");
                yield return null;
            }
        }
        /*RectTransform[] loadingScreenParts =  { loadingScreenLeft, loadingScreenRight };
        float[] positions = { -960f, 960f };
        UIAnimations.SlideXiUIsLinearCustomActions(loadingScreenParts, positions, 2f, () => { },
            () => { LoadingScreen.SetActive(false); });*/
        LoadingScreen.SetActive(false);
        scenesLoading.Clear();
    }

}

