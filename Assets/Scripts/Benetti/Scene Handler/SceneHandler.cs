using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Benetti
{
    public class SceneHandler
    {
        /// <summary>
        /// this custom class loops through array of scenes and loads them asynchronously.
        /// </summary>
        /// <param name="scenesToLoad">Array of scenes that needs to be loaded asynchronously</param>
        /// <param name="mode">In which mode should it load the scenes</param>
        /// <param name="scenesLoading">List of async operations to track the loading progress</param>
        static void LoadScenesAsync(Scene[] scenesToLoad, LoadSceneMode mode, List<AsyncOperation> scenesLoading)
        {
            scenesLoading = new List<AsyncOperation>();
            for(int i = 0; i < scenesToLoad.Length; i++)
            {
                if (!scenesToLoad[i].isLoaded)
                {
                    scenesLoading.Add(SceneManager.LoadSceneAsync(scenesToLoad[i].buildIndex, mode));
                }
            }
            
            
            
            
            
        }
    }

    
}
