# TMMCLineCounterApp

TMMCLineCounterApp - README

Description:
A simple Windows console app that counts the number of vertical black lines in black & white images created in MS Paint.

Black Pixel Detection:
- Uses a component-wise threshold check:
  A pixel is considered black if R, G, and B are all < 32.
- This ensures robustness against JPEG compression artifacts.

Requirements:
- Windows OS
- .NET 7 SDK

Build & Run:
1. dotnet build -c Release
2. Run with one argument:
   TMMCLineCounterApp.exe "absolute-path-to-img-file.jpg"

Usage:
- Program must be run with exactly 1 argument.
- If invalid arguments provided, prints help message.
- If successful, prints a single integer (line count).
- Exceptions are printed to console.

Exit Codes:
0 = success
1 = file not found or invalid args
2 = exception
