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
        private int _lastCheckedMeter = 0;
        
        /// <summary>
        /// The last meter with a platform spawned
        /// </summary>
        private int _lastSpawnedMeter = 0;
        
        
        private Vector3 _screenBounds;

        private void Awake()
        {
            _screenBounds = Camera.main!.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            float playerYPosition = JumpManager.Instance.Player.transform.position.y;

            if (playerYPosition + 20 > _lastCheckedMeter)
            {
                TrySpawnPlatform(++_lastCheckedMeter);
            }
        }

        private void TrySpawnPlatform(int y)
        {
            float spawnRate = Mathf.Lerp(_initialSpawnRate, _minSpawnRate, _lastSpawnedMeter / _finalPoint);
            
            if (UnityEngine.Random.value > spawnRate && y + 5 < _lastSpawnedMeter)
            {
                return;
            }

            _lastSpawnedMeter = y;
            
            // instantiate the platform
            GameObject platformContainer = Instantiate(_platformPrefab);
            Platform platform = platformContainer.GetComponentInChildren<Platform>();
            
            // set the platform's width
            float platformWidth = Mathf.Lerp(_initialWidth, _minWidth, _lastSpawnedMeter / _finalPoint);
            platform.SetWidth(platformWidth);

            // set the platform's position
            Vector3 position = Vector3.zero;
            position.y = y;
            position.x = UnityEngine.Random.Range(-_screenBounds.x, _screenBounds.x);
            platformContainer.transform.position = position;
        }
    }
}