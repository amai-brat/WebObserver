using System.Text.RegularExpressions;

namespace WebObserver.Main.Application.Helpers;

public partial class RegexHelper
{
    [GeneratedRegex("[&?]list=([^&]+)")]
    public static partial Regex YouTubePlaylistIdRegex();
}