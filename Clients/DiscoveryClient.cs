using HADotNet.Core.Models;
using System;
using System.Collections;

namespace HADotNet.Core.Clients
{
    /// <summary>
    /// Provides access to the discovery API for retrieving the current Home Assistant instance information.
    /// </summary>
    public sealed class DiscoveryClient : BaseClient
    {
        public DiscoveryClient() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryClient" />.
        /// </summary>
        /// <param name="instance">The Home Assistant base instance URL.</param>
        /// <param name="apiKey">The Home Assistant long-lived access token.</param>
        public DiscoveryClient(Uri instance, string apiKey) : base(instance, apiKey) { }

        /// <summary>
        /// Retrieves the current Home Assistant discovery object.
        /// </summary>
        /// <returns>A <see cref="DiscoveryObject" /> representing the current Home Assistant instance information.</returns>
        public IEnumerator GetDiscoveryInfo(Action<DiscoveryObject> handler = null) => Get("/api/discovery_info", handler);
    }
}
