using System;
using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
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
        
        

        private void Update()
        {
            // TODO: Do some sort of math to make the spawn rate pseudo-random
            
            // TODO: Make the spawn rate decrease as the player progresses
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