using System;
using Newtonsoft.Json;
using UnityEngine;

namespace IKIMONO.Pet
{
    /// <summary>
    /// Represents a complete save file.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SaveFile
    {
        #region Member Variables

        /// <summary>
        /// The index for the chosen background. 
        /// </summary>
        public int BackgroundIndex { get; } = -1;
        
        /// <summary>
        /// The amount of money the player has.
        /// </summary>
        public int Coins { get; } = 0;
        
        /// <summary>
        /// The DateTime the save file was created.
        /// </summary>
        public DateTime CreationDate { get; } = DateTime.Now;
        
        #endregion
        
        #region Methods
        /// <summary>
        /// Saves this save file to the PlayerPrefs.
        /// </summary>
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            PlayerPrefs.SetString("SaveFile", json);
            PlayerPrefs.Save();
            Debug.Log("Saved save file.");
        }
        
        /// <summary>
        /// Creates a SaveFile class from the PlayerPrefs. If no save file exists, a new one is created.
        /// </summary>
        /// <returns>The SaveFile class.</returns>
        public static SaveFile Load()
        {
            if (PlayerPrefs.HasKey("SaveFile"))
            {
                try
                {
                    string json = PlayerPrefs.GetString("SaveFile");
                    return JsonConvert.DeserializeObject<SaveFile>(json);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to load save file: " + e.Message); // TODO: Inform the user somehow? Maybe a popup, but this shouldn't happen.
                    return new SaveFile();
                }
            }

            Debug.Log("No save file found.");
            return new SaveFile();
        }
        #endregion
    }
}