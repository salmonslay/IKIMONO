using System.Collections;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace IKIMONO
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Webhook : MonoBehaviour
    {
        /// <summary>
        /// The message contents (up to 2000 characters)
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Override the default username of the webhook
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; } = "IKIMONO";

        /// <summary>
        /// Override the default avatar of the webhook
        /// </summary>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; } = "https://i.imgur.com/BQk2XFY.png";

        /// <summary>
        /// The URL to the webhook
        /// </summary>
        public string URL { get; set; } =
            "https://discord.com/api/webhooks/721675223841374228/fzRfJkuLvyrmN0caW3y5vU0_lVI-yeXWZ7Td8eBL2Yjm4n9s5l04mp0mbZ6CDWbxMpAI";


        public static Webhook Create(string content)
        {
            GameObject go = new GameObject("Webhook");
            Webhook webhook = go.AddComponent<Webhook>();
            webhook.Content = content;
            return webhook;
        }
        
        /// <summary>
        /// Serialize the webhook to a JSON string
        /// </summary>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            });
        }
        
        /// <summary>
        /// POST this webhook to Discord. It will NOT send if the application is in production mode.
        /// </summary>
        public void Send()
        {
            if (!Debug.isDebugBuild)
            {
                Debug.Log($"Webhook: {Content}");
            }
            else
            {
                StartCoroutine(Post(URL, ToJson()));
            }

            Destroy(gameObject, 30);
        }
        
        private static IEnumerator Post(string url, string jsonString)
        {
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            string response = request.downloadHandler.text;
            int statusCode = (int) request.responseCode;
            
            Debug.Log($"Webhook response: {statusCode}, {response}");
        }
    }
}