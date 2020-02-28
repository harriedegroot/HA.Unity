using HADotNet.Core;
using HADotNet.Core.Clients;
using HADotNet.Core.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HomeAssistant
{
    public class HomeAssistant: MonoBehaviour
    {
        public static HomeAssistant Instance { get; private set; }

        public string DebugUrl = "http://localhost:8123";
        public string Url;
        public string ApiKey = "<insert_your_ha_api_key_here>";

        public HomeAssistant()
        {
            if (Instance != null) throw new Exception("A Scene may only contain a single HomeAssistance instance");
            Instance = this;
        }

        void Awake()
        {
            var url = Debug.isDebugBuild ? DebugUrl : Url;

            if (string.IsNullOrEmpty(url)) 
            {
                var uri = new Uri(Application.absoluteURL);
                url = uri.GetLeftPart(UriPartial.Authority);
            }

            ClientFactory.Initialize(url, ApiKey);
        }

        public void GetState(string entityId, Action<StateObject> handler = null) => StartCoroutine(ClientFactory.GetClient<StatesClient>().GetState(entityId, handler));

        public void CallService(string domain, string service, object fields, Action<List<StateObject>> handler = null) => StartCoroutine(ClientFactory.GetClient<ServiceClient>().CallService(domain, service, fields, handler));

        public void CallService(string domain, string service, Action<List<StateObject>> handler = null) => StartCoroutine(ClientFactory.GetClient<ServiceClient>().CallService(domain, service, handler));
    }
}
