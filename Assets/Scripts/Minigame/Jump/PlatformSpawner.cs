using System;
using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _platformPrefab;
        
        [Header("Platform Spawning")]
        
        [Tooltip("The rate at which platforms are spawned")]
        [SerializeField] private float _spawnRate = 5f;
        
        [Tooltip("The minimum rate at which platforms are spawned")]
        [SerializeField] private float _minSpawnRate = 0.5f;

        private void Update()
        {
            // TODO: Do some sort of math to make the spawn rate pseudo-random
            
            // TODO: Make the spawn rate decrease as the player progresses
        }

        private void SpawnPlatform()
        {
            throw new NotImplementedException("SpawnPlatform not implemented");
        }
        
    }
}