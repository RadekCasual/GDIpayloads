﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

public class invert
{
    // Constants for GDI functions
    const int SRCCOPY = 0x00CC0020;
    const int PATINVERT = 0x005A0049;

    // P/Invoke declarations for GDI functions
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDc);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateSolidBrush(uint crColor);

    [DllImport("gdi32.dll")]
    public static extern int SelectObject(IntPtr hdc, IntPtr h);

    [DllImport("gdi32.dll")]
    public static extern bool PatBlt(IntPtr hdc, int x, int y, int w, int h, int rop);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteDC(IntPtr hdc);

    public static void Main()
    {
        Random r = new Random();
        int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
        uint[] rndclr = { 0xF0FFFF }; // Light cyan color (can be changed to any hexadecimal color)

        // Infinite loop to simulate fullscreen
        while (true)
        {
            IntPtr hdc = GetDC(IntPtr.Zero); // Get screen DC
            IntPtr brush = CreateSolidBrush(rndclr[r.Next(rndclr.Length)]); // Create a random color brush
            SelectObject(hdc, brush); // Select the brush into the DC

            // Perform a drawing operation (invert the screen)
            PatBlt(hdc, 0, 0, x, y, PATINVERT); // This covers the full screen

            // Clean up resources
            DeleteObject(brush);
            DeleteDC(hdc);

            // Sleep for 0,5 second before repeating (this value can be changed to any time, e.g. 1 second = 1000)
            Thread.Sleep(500);
        }
    }
}
