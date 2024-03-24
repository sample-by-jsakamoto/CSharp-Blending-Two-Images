using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

// Create an empty image canvas that is 256 x 256 px by ImageSharp.
using var canvas = new Image<Rgba32>(256, 256);

// Load the table image and draw it on the canvas
using var tableImage = await LoadImage("./assets/table.png");
canvas.Mutate(ctx => ctx.DrawImage(tableImage, 1f));

// Load the apple image and draw it on the canvas
using var appleImage = await LoadImage("./assets/apple.png");
canvas.Mutate(ctx => ctx.DrawImage(appleImage, new Point(96, 50), 1f));

// Get the canvas image as a byte array.
var memoryStream = new MemoryStream();
await canvas.SaveAsPngAsync(memoryStream);
var imageBytes = memoryStream.ToArray();

// Save it.
await SaveImage("image.png", imageBytes);

Console.WriteLine("The image was saved.");
Console.ReadLine();

async ValueTask<Image> LoadImage(string imageName)
{
    var imageBytes = await File.ReadAllBytesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName));

    // Create a ImageSharp.Image from the byte array.
    return Image.Load(imageBytes);
}

async ValueTask SaveImage(string imageName, byte[] imageBytes)
{
    await File.WriteAllBytesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName), imageBytes);
}
