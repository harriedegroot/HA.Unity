using HADotNet.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HADotNet.Core.Clients
{
    /// <summary>
    /// Provides access to the states API for retrieving information about the current state of entities.
    /// </summary>
    public sealed class StatesClient : BaseClient
    {
        public StatesClient() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatesClient" />.
        /// </summary>
        /// <param name="instance">The Home Assistant base instance URL.</param>
        /// <param name="apiKey">The Home Assistant long-lived access token.</param>
        public StatesClient(Uri instance, string apiKey) : base(instance, apiKey) { }

        /// <summary>
        /// Retrieves a list of current entities and their states.
        /// </summary>
        /// <returns>A <see cref="List{StateObject}" /> representing the current state.</returns>
        public IEnumerator GetStates(Action<List<StateObject>> handler = null) => Get("/api/states", handler);

        /// <summary>
        /// Retrieves the state of an entity by its ID.
        /// </summary>
        /// <returns>A <see cref="StateObject" /> representing the current state of the requested <paramref name="entityId" />.</returns>
        public IEnumerator GetState(string entityId, Action<StateObject> handler = null) => Get($"/api/states/{entityId}", handler);
    }
}
