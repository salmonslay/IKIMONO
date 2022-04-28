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

            if (UnityEngine.Random.value > GetCurrentSpawnRate() && y + 5 < _lastSpawnedMeter)
            {
                return;
            }

            _lastSpawnedMeter = y;
            GameObject spawnedPlatform = Instantiate(_platformPrefab);
            Platform platform = spawnedPlatform.GetComponent<Platform>();
            platform.SetWidth(GetCurrentWidth());

            Vector3 position = Vector3.zero;

            position.y = y;
            position.x = UnityEngine.Random.Range(-_screenBounds.x, _screenBounds.x);

            spawnedPlatform.transform.position = position;
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