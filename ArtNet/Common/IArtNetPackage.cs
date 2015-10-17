namespace ArtNet.Common
{
    /// <summary>
    /// An ArtNet package that can be send using a strem (socket)
    /// </summary>
    public interface IArtNetPackage
    {
        /// <summary>
        /// Gets the bytes that should be send.
        /// </summary>
        /// <returns></returns>
        byte[] GetBytes();
    }
}