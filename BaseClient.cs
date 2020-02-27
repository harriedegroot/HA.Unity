using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine.Networking;

namespace HADotNet.Core
{
    /// <summary>
    /// Represents the base client from which all other API clients derive.
    /// </summary>
    public abstract class BaseClient
    {
        public Uri Domain { get; private set; }
        public string ApiKey { get; private set; }

        public BaseClient() { }
        public BaseClient(Uri domain, string apiKey) => Initialize(domain, apiKey);

        public TClient Initialize<TClient>(Uri domain, string apiKey) where TClient : BaseClient => (TClient)Initialize(domain, apiKey);

        /// <summary>
        /// Initializes a new <see cref="BaseClient" /> instance.
        /// </summary>
        /// <param name="domain">The Home Assistant instance URL.</param>
        /// <param name="apiKey">The long-lived Home Assistant API key.</param>
        public BaseClient Initialize(Uri domain, string apiKey) 
        {
            Domain = domain;
            ApiKey = apiKey;

            return this;
        }

        /// <summary>
        /// Performs a GET request on the specified path.
        /// </summary>
        /// <typeparam name="T">The type of data to deserialize and return.</typeparam>
        /// <param name="path">The relative API endpoint path.</param>
        /// <returns>The deserialized data of type <typeparamref name="T" />.</returns>
        protected IEnumerator Get<T>(string path, Action<T> handler = null) where T : class
        {
            if (ApiKey == null) throw new Exception("Client not initialized: ApiKey not found");

            using (UnityWebRequest request = UnityWebRequest.Get(Domain + path.TrimStart('/')))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", $"Bearer {ApiKey}");

                // Request and wait for the desired page.
                yield return request.SendWebRequest();

                if (request.isNetworkError)
                    throw new Exception($"Unexpected response code {request.responseCode} from Home Assistant API endpoint {path}.");

                T response = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                handler?.Invoke(response);
            }
        }

        /// <summary>
        /// Performs a POST request on the specified path.
        /// </summary>
        /// <typeparam name="T">The type of object expected back.</typeparam>
        /// <param name="path">The path to post to.</param>
        /// <param name="body">The body contents to serialize and include.</param>
        /// <param name="isRawBody"><see langword="true" /> if the body should be interpereted as a pre-built JSON string, or <see langword="false" /> if it should be serialized.</param>
        /// <returns></returns>
        protected IEnumerator Post<T>(string path, object body, Action<T> handler = null, bool isRawBody = false) where T : class
        {
            if (ApiKey == null) throw new Exception("Client not initialized: ApiKey not found");
            
            var url = Domain + path.TrimStart('/');

            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                var serialized = isRawBody ? body?.ToString() : JsonConvert.SerializeObject(body);
                byte[] data = System.Text.Encoding.UTF8.GetBytes(serialized);

                request.uploadHandler = new UploadHandlerRaw(data);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", $"Bearer {ApiKey}");

                yield return request.SendWebRequest();

                if (request.isNetworkError)
                    throw new Exception($"Unexpected response code {request.responseCode} from Home Assistant API endpoint {path}.");

                T response = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                handler?.Invoke(response);
            }

            //using (UnityWebRequest request = UnityWebRequest.Post(Domain + path.TrimStart('/'), ))
            //{
            //    request.SetRequestHeader("Content-Type", "application/json");
            //    request.SetRequestHeader("Authorization", $"Bearer {ApiKey}");

            //    // Request and wait for the desired page.
            //    yield return request.SendWebRequest();

            //    if (request.isNetworkError)
            //        throw new Exception($"Unexpected response code {request.responseCode} from Home Assistant API endpoint {path}.");

            //    T response = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
            //    handler?.Invoke(response);
            //}
        }
    }
}
