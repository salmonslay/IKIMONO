using System.Collections;
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
            
            StartCoroutine(UpdateNeedValues());
        }

        private static IEnumerator UpdateNeedValues()
        {
            while (true)
            {
                yield return new WaitForSeconds(15);
                Player.Instance.Pet.UpdateValues();
            }
            // ReSharper disable once IteratorNeverReturns
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
        
        /// <summary>
        /// Plays a random AudioClip from an array
        /// </summary>
        /// <param name="clips">The clip pool to play from.</param>
        /// <returns>The AudioSource that is playing the clip.</returns>
        public static AudioSource PlayAudio(AudioClip[] clips)
        {
            return clips.Length == 0 ? null : PlayAudio(clips[Random.Range(0, clips.Length)]);
        }
        
        /// <summary>
        /// Plays a random AudioClip
        /// </summary>
        /// <param name="clip">The clip to play.</param>
        /// <returns>The AudioSource that is playing the clip.</returns>
        public static AudioSource PlayAudio(AudioClip clip)
        {
            if (clip == null) return null;
            GameObject obj = new GameObject();
            AudioSource source = obj.AddComponent<AudioSource>();

            source.clip = clip;
            source.Play();

            Destroy(obj, clip.length + 0.5f);
            return source;
        }
    }
}