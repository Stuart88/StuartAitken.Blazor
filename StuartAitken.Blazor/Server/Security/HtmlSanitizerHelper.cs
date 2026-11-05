using Ganss.Xss;

namespace StuartAitken.Blazor.Server.Security
{
    /// <summary>
    /// Strips script/event-handler/etc. content out of rich-text project descriptions before
    /// they're persisted, so a compromised admin session can't plant stored XSS for every visitor.
    /// </summary>
    public static class HtmlSanitizerHelper
    {
        #region Private Fields

        private static readonly HtmlSanitizer _sanitizer = new();

        #endregion Private Fields

        #region Public Methods

        public static string? Sanitize(string? html) => html == null ? null : _sanitizer.Sanitize(html);

        #endregion Public Methods
    }
}
