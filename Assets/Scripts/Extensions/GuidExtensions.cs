namespace MagicLeapSerialization.Assets.Scripts.Extensions
{
    public static class GuidExtensions
    {
        public static string[] GetParts(this System.Guid guid) => guid.ToString().Split('-');
    }
}