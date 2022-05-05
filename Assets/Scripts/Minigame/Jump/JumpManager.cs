using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IKIMONO.Minigame.Jump
{
    public class JumpManager : MonoBehaviour
    {
        public static JumpManager Instance;

        public JumpPlayer Player { get; private set; }

        public GameState GameState { get; private set; } = GameState.None;

        /// <summary>
        /// The highest point the player has reached in the current game, in meters (global Y axis)
        /// </summary>
        public float HighestJump { get; private set; } = 0;

        public int CoinsCollected { get; private set; } = 0;
        
        public int JumpCount { get; set; } = 1; // start at 1, because the ground jump is made differently
        
        [SerializeField] private Text _coinText;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private Text _gameOverText;

        private void Awake()
        {
            // Singleton, but make a new one for each game
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
            else
            {
                Instance = this;
            }

            Player = FindObjectOfType<JumpPlayer>();

            AudioManager.Instance.playSound("MinigameMusic", "One");
            // @PhilipAudio: Audio here I suppose? Do *not* restart it if the player restarts the game.
        }

        private void Update()
        {
            // Update highest jump
            HighestJump = Mathf.Max(HighestJump, Player.transform.position.y);
        }

        public void AddCoin()
        {
            CoinsCollected++;
            _coinText.text = CoinsCollected.ToString();
        }

        public void GameOver()
        {
            _gameOverPanel.SetActive(true);
            const string color = "#C08C2B";
            _gameOverText.text = $"You travelled <color={color}>{Mathf.RoundToInt(HighestJump)}</color> meters, collected <color={color}>{CoinsCollected}</color> coin{(CoinsCollected == 1 ? "" : "s")} and jumped <color={color}>{JumpCount}</color> times!";

            // This is extremely ugly, a refactor would be nice at some point
            Pet.Player.Instance.Pet.Fun.Increase(HighestJump / 32); // divide by 32 to make it a bit less predictable
            Pet.Player.Instance.AddCoins(CoinsCollected);

            AudioManager.Instance.randomizeSound("GameOver");
            // @PhilipAudio: Play the game over sound here, and tune down the music a bit.
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene("MinigameJump");
        }
        
        public void Quit()
        {
            SceneManager.LoadScene("Main");
        }
    }
}