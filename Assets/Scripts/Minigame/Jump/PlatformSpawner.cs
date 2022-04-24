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
        
        [Header("Platform Spawning")]
        
        [Tooltip("The odds of a platform spawning per meter")]
        [SerializeField] private float _initialSpawnRate = 70f;
        
        [Tooltip("The minimum odds at which a platform will spawn")]
        [SerializeField] private float _minSpawnRate = 5f;

        [Tooltip("The Y point where the lowest odds will be reached")] 
        [SerializeField] private float _finalPoint = 500f;
        
        /// <summary>
        /// The last meter checked for spawning
        /// </summary>
        private float _lastSpawnedMeter = 0f;

        private void Update()
        {
            // TODO: Run the odds for spawning a platform if the player is close enough away to the last one spawned
        }

        private void SpawnPlatform()
        {
            throw new NotImplementedException("SpawnPlatform not implemented");
        }

        private float GetCurrentSpawnRate()
        {
            // This is basically a linear function:
            // The spawn rate will go from _initialSpawnRate to _minSpawnRate as the player progresses towards the _finalPoint
            float percent = Math.Min(JumpManager.Instance.HighestJump / _finalPoint, 1);
            return Mathf.Clamp(_initialSpawnRate - (_initialSpawnRate - _minSpawnRate) * percent, _minSpawnRate, _initialSpawnRate);
        }
        
    }
}