﻿using HADotNet.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HADotNet.Core.Clients
{
    /// <summary>
    /// Provides access to the event API for retrieving information about events and firing events.
    /// </summary>
    public sealed class EventClient : BaseClient
    {
        public EventClient() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventClient" />.
        /// </summary>
        /// <param name="instance">The Home Assistant base instance URL.</param>
        /// <param name="apiKey">The Home Assistant long-lived access token.</param>
        public EventClient(Uri instance, string apiKey) : base(instance, apiKey) { }

        /// <summary>
        /// Retrieves the current Home Assistant configuration object.
        /// </summary>
        /// <returns>A <see cref="ConfigurationObject" /> representing the current Home Assistant configuration.</returns>
        public IEnumerator GetEvents(Action<List<EventObject>> handler = null) => Get("/api/events", handler);
    }
}
