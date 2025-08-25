using UnityEngine;
using System;
using System.IO;

namespace Benetti
{
    /// <summary>
    /// A static helper class for saving and loading data to and from JSON files.
    /// Uses Unity's JsonUtility for serialization.
    /// </summary>
    public static class JsonDataHelper
    {
        /// <summary>
        /// Saves the provided data object to a JSON file.
        /// </summary>
        /// <param name="data">The object to save. This object must be serializable by JsonUtility.</param>
        /// <param name="fileName">The name of the file to save (e.g., "gamesettings.json").</param>
        /// <typeparam name="T">The type of the data object.</typeparam>
        public static void Save<T>(T data, string fileName)
        {
            // Get the full path to the file in a persistent data location
            string path = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                // Serialize the object to a JSON string with pretty print for readability
                string json = JsonUtility.ToJson(data, true);

                // Write the JSON string to the file
                File.WriteAllText(path, json);

                #if UNITY_EDITOR
                Debug.Log($"Successfully saved data to {path}");
                #endif
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save data to {path}. Error: {e.Message}");
            }
        }

        /// <summary>
        /// Loads a data object from a JSON file.
        /// If the file does not exist, it returns a new instance of the object.
        /// </summary>
        /// <param name="fileName">The name of the file to load.</param>
        /// <typeparam name="T">The type of the data object to load. Must have a parameterless constructor.</typeparam>
        /// <returns>The loaded data object, or a new instance if the file doesn't exist.</returns>
        public static T Load<T>(string fileName) where T : new()
        {
            // Get the full path to the file
            string path = Path.Combine(Application.persistentDataPath, fileName);

            // If the file doesn't exist, return a default new instance
            if (!File.Exists(path))
            {
                #if UNITY_EDITOR
                Debug.LogWarning($"File not found at {path}. Returning a new default instance.");
                #endif
                return new T();
            }

            try
            {
                // Read the JSON string from the file
                string json = File.ReadAllText(path);

                // Deserialize the JSON string back into an object
                T data = JsonUtility.FromJson<T>(json);

                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data from {path}. Error: {e.Message}. Returning a new default instance.");
                // In case of error (e.g., corrupted file), return a default instance
                return new T();
            }
        }
    }
}
