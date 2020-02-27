using HADotNet.Core.Models;
using System;
using System.Collections;

namespace HADotNet.Core.Clients
{
    /// <summary>
    /// Provides access to the root API call (located at /api/) to ensure the API is working normally.
    /// </summary>
    public class RootApiClient : BaseClient
    {
        public RootApiClient() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RootApiClient" />.
        /// </summary>
        /// <param name="instance">The Home Assistant base instance URL.</param>
        /// <param name="apiKey">The Home Assistant long-lived access token.</param>
        public RootApiClient(Uri instance, string apiKey) : base(instance, apiKey) { }

        /// <summary>
        /// Retrieves the API status message for the Home Assistant instance, to ensure it is running.
        /// </summary>
        /// <returns>A <see cref="MessageObject" /> indicating the status of the connected instance.</returns>
        public IEnumerator GetStatusMessage(Action<MessageObject> handler = null) => Get("/api/", handler);
    }
}
