using SkiaSharp;

// Create an empty image canvas that is 256 x 256 px by SkiaSharp.
using var surface = SKSurface.Create(new SKImageInfo(width: 256, height: 256));
var canvas = surface.Canvas;

// Load the table image and draw it on the canvas.
using var tableImage = await LoadImage("./assets/table.png");
canvas.DrawImage(tableImage, 0, 0);

// Load the apple image and draw it on the canvas.
using var appleImage = await LoadImage("./assets/apple.png");
canvas.DrawImage(appleImage, 96, 50);

// Get the canvas image as a byte array.
var imageBytes = surface
    .Snapshot()
    .Encode(SKEncodedImageFormat.Png, quality: 100)
    .ToArray();

// Save it.
await SaveImage("image.png", imageBytes);

Console.WriteLine("The image was saved.");
Console.ReadLine();

async ValueTask<SKImage> LoadImage(string imageName)
{
    var imageBytes = await File.ReadAllBytesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName));

    // Create a SKImage from the byte array.
    return SKImage.FromEncodedData(imageBytes);
}

async ValueTask SaveImage(string imageName, byte[] imageBytes)
{
    await File.WriteAllBytesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName), imageBytes);
}
