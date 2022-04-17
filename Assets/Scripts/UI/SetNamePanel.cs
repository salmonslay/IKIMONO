using System;
using IKIMONO.Pet;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IKIMONO.UI
{
    public class SetNamePanel : MonoBehaviour
    {
        [SerializeField] private InputField _nameInputField;
        [SerializeField] private Text _statusText;
        private void Start()
        {
            _nameInputField.text = Player.Instance.Pet.Name;
            _statusText.text = "";
        }
        
        public void SetRandom()
        {
            _nameInputField.text = NameSet.GetRandomName();
        }

        public void Save()
        {
            string newName = _nameInputField.text.Trim();
            
            if(newName.Length == 0)
            {
                _statusText.text = "Please enter a name!";
                return;
            }
            
            Player.Instance.Pet.SetName(newName);
            Destroy(gameObject);
        }

        public static void Open()
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/SetNamePanel"));
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class NameSet
    {
        private static NameSet _instance;

        [JsonProperty("girls")] public string[] FemaleNames = Array.Empty<string>();

        [JsonProperty("boys")] public string[] MaleNames = Array.Empty<string>();

        static NameSet()
        {
            Initialize();
        }

        private static void Initialize()
        {
            TextAsset json = Resources.Load<TextAsset>("Data/names");

            if (json == null)
            {
                Debug.LogWarning("No names found! Please add a names.json file to the Resources/Data/ folder.");
            }
            else
            {
                _instance = JsonConvert.DeserializeObject<NameSet>(json.text);
            }
        }


        public static string GetRandomName()
        {
            return Random.Range(0, 2) == 1 ? GetRandomFemaleName() : GetRandomMaleName();
        }
        
        public static string GetRandomFemaleName()
        {
            return _instance.FemaleNames[Random.Range(0, _instance.FemaleNames.Length)];
        }
        
        public static string GetRandomMaleName()
        {
            return _instance.MaleNames[Random.Range(0, _instance.MaleNames.Length)];
        }
    }
}