using System;
using System.Runtime.InteropServices;

namespace XScreenshot;

/// <summary>
/// Interop with the shared lib for X11
/// </summary>
internal static class XInterop
{
    [DllImport("libX11.so.6", EntryPoint = "XOpenDisplay")]
    public static extern IntPtr XOpenDisplay(string displayName = ":0.0");
    
    [DllImport("libX11.so.6", EntryPoint = "XRootWindow")]
    public static extern IntPtr XRootWindow(IntPtr display, int screenNumber);

    [DllImport("libX11.so.6", EntryPoint = "XDefaultScreen")]
    public static extern int XDefaultScreen(IntPtr display);

    [DllImport("libX11.so.6", EntryPoint = "XDisplayWidth")]
    public static extern int XDisplayWidth(IntPtr display, int screenNumber);

    [DllImport("libX11.so.6", EntryPoint = "XDisplayHeight")]
    public static extern int XDisplayHeight(IntPtr display, int screenNumber);

    [DllImport("libX11.so.6", EntryPoint = "XGetImage")]
    public static extern IntPtr XGetImage(IntPtr display, IntPtr drawable, int x, int y, uint width, uint height,
                                          ulong planeMask, int format);

    [DllImport("libX11.so.6", EntryPoint = "XGetPixel")]
    public static extern ulong XGetPixel(IntPtr ximage, int x, int y);

    [DllImport("libX11.so.6", EntryPoint = "XDestroyImage")]
    public static extern void XDestroyImage(IntPtr ximage);
}

/// <summary>
/// Interop with the awesome http://nothings.org/stb
/// </summary>
internal static class StbImageWrite
{
    [DllImport("libstb_image_write.so", EntryPoint = "write_jpg")]
    public static extern int WriteJpg(string filename, int width, int height, int comp, IntPtr data, int quality);
}