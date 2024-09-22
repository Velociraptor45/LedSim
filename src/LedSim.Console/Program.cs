using LedSim;
using Spectre.Console;

const int rowCount = 3;
const int columnCount = 5;

using var led = new Led(columnCount, rowCount, Color.Black);

for (int row = 0; row < rowCount; row++)
{
    for (int col = 0; col < columnCount; col++)
    {
        led.Wait(500);
        led.Clear();
        led[col, row] = new Color(255, 100, 0);
    }
}

led.Wait(500);
led.Clear();