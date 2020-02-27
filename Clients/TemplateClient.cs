using System;

namespace HADotNet.Core.Clients
{
    /// <summary>
    /// Provides access to the template API for rendering Home Assistant templates.
    /// </summary>
    public sealed class TemplateClient : BaseClient
    {
        public TemplateClient() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient" />.
        /// </summary>
        /// <param name="instance">The Home Assistant base instance URL.</param>
        /// <param name="apiKey">The Home Assistant long-lived access token.</param>
        public TemplateClient(Uri instance, string apiKey) : base()
        {
            Initialize(instance, apiKey);
        }

        /// <summary>
        /// Renders a template and returns the resulting output as a string.
        /// </summary>
        /// <returns>A string of the rendered template output.</returns>
        public void RenderTemplate(string template, Action<string> handler = null)
        {
            Post<string>("/api/template", new { template }, handler);
        }
    }
}
