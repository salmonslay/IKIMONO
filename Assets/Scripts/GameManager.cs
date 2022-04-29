using System.Collections;
using IKIMONO.Pet;
using UnityEngine;

namespace IKIMONO
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        AndroidNotifications androidNotifications;
        private void Start()
        {
            Player.Instance.Pet.UpdateValues();
            androidNotifications = AndroidNotifications.Instance;
            androidNotifications.CancelAllNotifications();

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Webhook.Create($"start\n{Player.Instance.GetCodeBlock()}").Send();
            StartCoroutine(UpdateNeedValues());
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
            Webhook.Create($"quit\n{Player.Instance.GetCodeBlock()}").Send();
            ScheduleNeedNotifications();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Player.Instance.Pet.UpdateValues();
            Webhook.Create($"pause {pauseStatus}\n{Player.Instance.GetCodeBlock()}").Send();
            if (pauseStatus)
            {
                ScheduleNeedNotifications();
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Player.Instance.Pet.UpdateValues();
            Webhook.Create($"focus {hasFocus}\n{Player.Instance.GetCodeBlock()}").Send();
            if (!hasFocus)
            {
                ScheduleNeedNotifications();
            }
        }

        private void ScheduleNeedNotifications()
        {
            androidNotifications.CancelAllNotifications();
            foreach (PetNeed need in Player.Instance.Pet.Needs)
            {
                if (need.GetType() == typeof(PetNeedEnergy) && ((PetNeedEnergy)need).IsSleeping)
                {
                    continue;
                }
                float fireAtNeedValue = 30;
                if (need.HasNotifications && need.Value > fireAtNeedValue)
                {
                    float fireDelay = (float)need.GetTimeAtValue(fireAtNeedValue).Subtract(System.DateTime.Now).TotalSeconds;
                    androidNotifications.PushNotification(androidNotifications.BuildNotification(
                        need.NotificationTitle,
                        need.NotificationDescription,
                        need.NotificationIcon, fireDelay));
                }
            }
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