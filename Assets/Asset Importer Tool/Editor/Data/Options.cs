// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This is a list of unity supported platform name options.
    /// </summary>
    public enum PlatformOption
    {
        // A list of platform names
        None, Android, iOS, NintendoSwitch,
        PS4, PS5, Standalone, tvOS, WindowsStoreApps,
        WebGL, XBoxOne
    }

    /// <summary>
    /// This is a list of unity import settings texture sizes.
    /// </summary>
    public enum TextureSizeOption
    {
        // A list of texture sizes
        _32 = 32, _64 = 64, _128 = 128, _256 = 256,
        _512 = 512, _1024 = 1024, _2048 = 2048,
        _4096 = 4096, _8192 = 8192
    }
}