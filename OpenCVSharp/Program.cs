using OpenCvSharp;

// Create an empty image canvas that is 256 x 256 px by OpenCVSharp.
var canvas = new Mat(256, 256, MatType.CV_8UC3, Scalar.Black);

// Load the table image and draw it on the canvas
var tableImage = await LoadImage("./assets/table.png");
tableImage.CopyTo(canvas);// [new Rect(0, 0, tableImage.Width, tableImage.Height)]);

// Load the apple image with alpha channel
var appleImage = await LoadImage("./assets/apple.png");

// Create a mask from the alpha channel of the apple image
var mask = new Mat();
Cv2.ExtractChannel(appleImage, mask, 3);
Cv2.CvtColor(appleImage, appleImage, ColorConversionCodes.BGRA2BGR);
Cv2.CvtColor(mask, mask, ColorConversionCodes.GRAY2BGR);

// Create a masked version of the apple image
Cv2.BitwiseAnd(appleImage, mask, appleImage);

// Blend the masked apple image with the masked tableImage
var appleRect = new Rect(96, 50, appleImage.Width, appleImage.Height);
var blendedImage = tableImage[appleRect];
Cv2.BitwiseNot(mask, mask);
Cv2.BitwiseAnd(blendedImage, mask, blendedImage);
Cv2.BitwiseOr(blendedImage, appleImage, blendedImage);

// Draw back the belnded image to the canvae
blendedImage.CopyTo(canvas[appleRect]);

// Get the canvas image as a byte array.
var imageBytes = canvas.ToBytes(".png");

// Save it.
await SaveImage("image.png", imageBytes);

Console.WriteLine("The image was saved.");
Console.ReadLine();

async ValueTask<Mat> LoadImage(string imageName)
{
    var imageBytes = await File.ReadAllBytesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName));

    // Create a Mat from the byte array.
    return Cv2.ImDecode(imageBytes, ImreadModes.Unchanged);
}

async ValueTask SaveImage(string imageName, byte[] imageBytes)
{
    await File.WriteAllBytesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName), imageBytes);
}
