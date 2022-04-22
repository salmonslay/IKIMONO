using IKIMONO.Pet;
using UnityEngine;

namespace IKIMONO
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            Player.Instance.Pet.UpdateValues();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Player.Instance.Pet.UpdateValues();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Player.Instance.Pet.UpdateValues();
        }
    }
}