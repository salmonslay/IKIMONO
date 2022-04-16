using UnityEngine;

namespace IKIMONO.Pet
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            Player.Instance.AddCoins(5);
            Player.Instance.Pet.Hunger.Increase(5);
            Player.Instance.Pet.UpdateValues();
            print(Player.Instance.ToString());
            Player.Instance.Save();
        }

        public void NukeData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("boom. restart the game");
        }

        public void UpdateAll()
        {
            Player.Instance.Pet.UpdateValues();
        }
    }
}