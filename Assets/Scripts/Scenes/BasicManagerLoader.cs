using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicManagerLoader : MonoBehaviour
{
    public BasicManagerLoader bml;

    private void Start()
    {
        if (bml == null)
        {
            bml = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        
        if(!SceneManager.GetSceneByName("MainMenu").isLoaded /*&& !SceneManager.GetSceneByName("Game").isLoaded*/)
            SceneManager.LoadSceneAsync((int)SceneEnum.MainMenu, LoadSceneMode.Additive);
    }
}
