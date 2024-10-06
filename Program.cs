using System.Runtime.InteropServices;

namespace XScreenshot;

internal class Program
{
    [STAThread]
    private static void Main()
    {
        IntPtr display = XInterop.XOpenDisplay();

        if (display == IntPtr.Zero) Environment.Exit(1);

        int screen = XInterop.XDefaultScreen(display);
        uint width = (uint)XInterop.XDisplayWidth(display: display, screenNumber: screen);
        uint height = (uint)XInterop.XDisplayHeight(display: display, screenNumber: screen);

        IntPtr rootWindow = XInterop.XRootWindow(display: display, screenNumber: screen);
        IntPtr xImage = XInterop.XGetImage(
            display: display,
            drawable: rootWindow,
            x: 0,
            y: 0,
            width: width,
            height: height,
            planeMask: ulong.MaxValue,
            format: 2);

        byte[] imageData = new byte[width * height * 4]; // RGBA
        int index = 0;

        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
        {
            ulong pixel = XInterop.XGetPixel(ximage: xImage, x: x, y: y);
            imageData[index++] = (byte)(pixel >> 16 & 0xFF); // Red
            imageData[index++] = (byte)(pixel >> 8 & 0xFF);  // Green
            imageData[index++] = (byte)(pixel & 0xFF);       // Blue
            imageData[index++] = (byte)(pixel >> 24 & 0xFF); // Alpha
        }

        XInterop.XDestroyImage(xImage);

        IntPtr unmanagedPointer = Marshal.AllocHGlobal(imageData.Length);
        Marshal.Copy(source: imageData, startIndex: 0, destination: unmanagedPointer, length: imageData.Length);

        int result = StbImageWrite.WriteJpg(
            filename: "image.jpg",
            width: (int)width,
            height: (int)height,
            comp: 4, // RGBA
            data: unmanagedPointer,
            quality: 90);

        Marshal.FreeHGlobal(unmanagedPointer);

        Environment.Exit(result);
    }
}