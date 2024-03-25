using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

// Create a canvas from the table image.
using var canvas = await LoadImage("./assets/table.png");

// Clear out any preexisting color palette if the image has one.
// The encoder will automatically create a new palette if needed.
PngMetadata metadata = canvas.Metadata.GetPngMetadata();
metadata.ColorTable = null;

// Load the apple image and draw it on the canvas
using var appleImage = await LoadImage("./assets/apple.png");
canvas.Mutate(ctx => ctx.DrawImage(appleImage, new Point(96, 50), 1f));

// Save it.
await SaveImage(canvas, "image.png");

Console.WriteLine("The image was saved.");
Console.ReadLine();

// Load as Rgba32 to ensure we always have an alpha channel for transparency.
Task<Image<Rgba32>> LoadImage(string imageName)
    => Image.LoadAsync<Rgba32>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName));

Task SaveImage(Image image, string imageName)
    => image.SaveAsPngAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName));
