using System;
namespace ArtNet.Common
{
    public interface IArtNetPackage
    {
        byte[] GetBytes();
    }
}
