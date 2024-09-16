using LedSim;
using Spectre.Console;

const int rowCount = 3;
const int columnCount = 5;

var led = new Led(columnCount, rowCount, Color.Black);

var random = new Random(28);

for (int row = 0; row < rowCount; row++)
{
    for (int col = 0; col < columnCount; col++)
    {
        led.Wait(500);
        led.Clear(false);
        led[col, row] = new Color(
            (byte)random.Next(255),
            (byte)random.Next(255),
            (byte)random.Next(255)
            );
    }
}

led.Wait(500);
led.Clear();