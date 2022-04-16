using IKIMONO.Pet;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IKIMONO.UI
{
    public class SetNamePanel : MonoBehaviour
    {
        public static NameSet NameSet { get; private set; } // TODO: Init before use?
        
        [SerializeField] private InputField _nameInputField;
        [SerializeField] private Text _statusText;
        private void Start()
        {
            InitializeNameSet();
            _nameInputField.text = Player.Instance.Pet.Name;
            _statusText.text = "";
        }
        
        public static void InitializeNameSet()
        {
            if (NameSet != null) return;
            
            TextAsset json = Resources.Load<TextAsset>("Data/names");
                
            if (json == null)
            {
                Debug.LogWarning("No names found! Please add a names.json file to the Resources/Data/ folder.");
                NameSet = new NameSet();
            }
            NameSet = JsonConvert.DeserializeObject<NameSet>(json.text);
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
        [JsonProperty("girls")] public string[] FemaleNames;

        [JsonProperty("boys")] public string[] MaleNames;

        public string GetRandomName()
        {
            return Random.Range(0, 2) == 1 ? GetRandomFemaleName() : GetRandomMaleName();
        }
        
        public string GetRandomFemaleName()
        {
            return FemaleNames[Random.Range(0, FemaleNames.Length)];
        }
        
        public string GetRandomMaleName()
        {
            return MaleNames[Random.Range(0, MaleNames.Length)];
        }
    }
}