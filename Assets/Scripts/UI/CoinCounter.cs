using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;

namespace IKIMONO.UI
{
    // TODO: Replace with event system
    public class CoinCounter : MonoBehaviour
    {
        private Text _text;
        
        private void Awake()
        {
            _text = GetComponent<Text>();
        }
        
        private void Update()
        {
            _text.text = Player.Instance.Coins.ToString();
        }
    }
}