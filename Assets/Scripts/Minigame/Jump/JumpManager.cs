using System;
using UnityEngine;

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
        }

        private void Update()
        {
            // Update highest jump
            HighestJump = Mathf.Max(HighestJump, Player.transform.position.y);
        }
    }
}