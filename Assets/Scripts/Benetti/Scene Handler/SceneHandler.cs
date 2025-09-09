using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Benetti
{
    public class SceneHandler
    {
        /*/// <summary>
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

        }*/
        
        // <summary>
        /// Loads multiple scenes asynchronously and returns a list of AsyncOperations for progress tracking.
        /// </summary>
        /// <param name="sceneIndices">Build indices of the scenes to load</param>
        /// <param name="mode">LoadSceneMode (Single or Additive)</param>
        /// <returns>List of AsyncOperations for progress tracking</returns>
        public static List<AsyncOperation> LoadScenesAsync(int[] sceneIndices, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            var scenesLoading = new List<AsyncOperation>();

            foreach (int index in sceneIndices)
            {
                Scene scene = SceneManager.GetSceneByBuildIndex(index);
                if (!scene.isLoaded)
                {
                    var operation = SceneManager.LoadSceneAsync(index, mode);
                    if (operation != null)
                        scenesLoading.Add(operation);
                }
            }

            return scenesLoading;
        }

        /// <summary>
        /// Unloads multiple scenes asynchronously.
        /// </summary>
        public static List<AsyncOperation> UnloadScenesAsync(int[] sceneIndices)
        {
            var scenesUnloading = new List<AsyncOperation>();

            foreach (int index in sceneIndices)
            {
                Scene scene = SceneManager.GetSceneByBuildIndex(index);
                if (scene.isLoaded)
                {
                    var operation = SceneManager.UnloadSceneAsync(index);
                    if (operation != null)
                        scenesUnloading.Add(operation);
                }
            }

            return scenesUnloading;
        }
    }
    
}


