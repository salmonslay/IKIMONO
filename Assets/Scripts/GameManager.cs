using System.Collections;
using IKIMONO.Pet;
using UnityEngine;

namespace IKIMONO
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
#if UNITY_ANDROID
        private AndroidNotifications _androidNotifications;
#endif

        private Settings _settings;
       

        private void Start()
        {
            Player.Instance.Pet.UpdateValues();
            _settings = Player.Instance.Settings;
#if UNITY_ANDROID
            _androidNotifications = AndroidNotifications.Instance;
            _androidNotifications.CancelAllNotifications();
#endif
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

            //Lägger in background såhär, är det rätt?
            //gameObject.AddComponent<Background>();

        }

        private static IEnumerator UpdateNeedValues()
        {
            while (true)
            {
                Player.Instance.Pet.UpdateValues();
                yield return new WaitForSeconds(15);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void OnApplicationQuit()
        {
            Player.Instance.Pet.UpdateValues();
            ScheduleNeedNotifications();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Player.Instance.Pet.UpdateValues();
            if (pauseStatus)
            {
                ScheduleNeedNotifications();
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Player.Instance.Pet.UpdateValues();
            if (!hasFocus)
            {
                ScheduleNeedNotifications();
            }
        }

        [SerializeField] private float fireNotificationsAtNeedValue = 30;
        private void ScheduleNeedNotifications()
        {
#if UNITY_ANDROID
            if (!_settings.NotificationsToggle) return;

            _androidNotifications.CancelAllNotifications();
            foreach (PetNeed need in Player.Instance.Pet.Needs)
            {
                if (need.GetType() == typeof(PetNeedEnergy) && ((PetNeedEnergy)need).IsSleeping)
                {
                    continue;
                }
                if (need.HasNotifications && need.Value > fireNotificationsAtNeedValue)
                {
                    float fireDelay = (float)need.GetTimeAtValue(fireNotificationsAtNeedValue).Subtract(System.DateTime.Now).TotalSeconds;
                    _androidNotifications.PushNotification(_androidNotifications.BuildNotification(
                        need.NotificationTitle,
                        need.NotificationDescription,
                        need.NotificationIcon, fireDelay));
                }
            }
#endif
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