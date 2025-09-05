using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject LoadingScreen;
    public Slider loadingBar;
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

    public void LoadGame()
    {
        LoadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneEnum.MainMenu));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneEnum.PrototypingScene, LoadSceneMode.Additive));
        
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
        LoadingScreen.SetActive(false);
    }
}
