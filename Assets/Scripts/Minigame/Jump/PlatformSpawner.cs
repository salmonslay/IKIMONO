using System;
using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    /// <summary>
    /// One platform can spawn per meter of distance (the global scoped Y axis).
    /// </summary>
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _platformPrefab;
        
        [Header("Platforms")]
        
        [Tooltip("The odds of a platform spawning per meter")]
        [SerializeField] private float _initialSpawnRate = 0.7f;
        
        [Tooltip("The minimum odds at which a platform will spawn")]
        [SerializeField] private float _minSpawnRate = 0.05f;
        
        [Tooltip("The initial width of the platform")]
        [SerializeField] private float _initialWidth = 1.5f;
        
        [Tooltip("The minimum width at which a platform will spawn")]
        [SerializeField] private float _minWidth = 0.5f;

        [Tooltip("The Y point where the lowest spawning odds and platform width will be reached")] 
        [SerializeField] private float _finalPoint = 500f;
        
        /// <summary>
        /// The last meter checked for spawning
        /// </summary>
        private float _lastSpawnedMeter = 0f;

        private void Update()
        {
            // TODO Tova:
            // - If the player is close enough to the last spawned meter, run SpawnPlatform(_lastSpawnedMeter)
        }

        private void SpawnPlatform(int y)
        {
            // TODO Tova:
            // - Check if this meter has already been spawned (if so, don't spawn)
            // - Run the odds for spawning a platform from GetCurrentSpawnRate()
            // - If true, spawn a platform, else return
            
            // - Instantiate the platform (_platformPrefab)
            // - Set the width of the platform from GetCurrentWidth()
            // - Set the X position of the platform to a random value within the border
            // - Set the Y position of the platform to the Y value passed in 
            
            throw new NotImplementedException("SpawnPlatform not implemented");
        }

        private float GetCurrentSpawnRate()
        {
            // This is basically a linear function:
            // The spawn rate will go from _initialSpawnRate to _minSpawnRate as the player progresses towards the _finalPoint
            float percent = Math.Min(JumpManager.Instance.HighestJump / _finalPoint, 1);
            return Mathf.Clamp(_initialSpawnRate - (_initialSpawnRate - _minSpawnRate) * percent, _minSpawnRate, _initialSpawnRate);
        }
        
        private float GetCurrentWidth()
        {
            // The math is the same as above, but the width will go from _initialWidth to _minWidth
            float percent = Math.Min(JumpManager.Instance.HighestJump / _finalPoint, 1);
            return Mathf.Clamp(_initialWidth - (_initialWidth - _minWidth) * percent, _minWidth, _initialWidth);
        }
        
    }
}