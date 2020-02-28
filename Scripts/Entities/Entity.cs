using HADotNet.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HomeAssistant
{
    public abstract class Entity
    {
        public string Domain { get; set; }
        public string Id { get; protected set; }
        public Entity(string domain, string id)
        {
            Domain = domain;
            Id = id;
        }

        public void GetState(Action<StateObject> handler = null) => HomeAssistant.Instance.GetState(Id, handler);

        public void CallService(string service, Action<List<StateObject>> handler = null) => CallService(service, null, handler);

        public void CallService(string service, object fields, Action<List<StateObject>> handler = null)
        {
            var data = fields?
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(fields))
                ?? new Dictionary<string, object>();

            data["entity_id"] = Id;

            HomeAssistant.Instance.CallService(Domain, service, data, handler);
        }
    }
}
