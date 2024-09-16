using Spectre.Console;
using Spectre.Console.Rendering;

namespace LedSim;

public class Led
{
    private readonly Color[,] _colors;
    private readonly Color _defaultColor;

    public Led(int columnCount, int rowCount) : this(columnCount, rowCount, Color.Black)
    {
    }

    public Led(int columnCount, int rowCount, Color defaultColor)
    {
        _colors = new Color[columnCount, rowCount];
        _defaultColor = defaultColor;
        Clear();
        Render();
    }

    public Color this[int column, int row]
    {
        get => _colors[column, row];
        set
        {
            _colors[column, row] = value;
            Render();
        }
    }

    public void Wait(int milliseconds)
    {
        Task.Delay(milliseconds).GetAwaiter().GetResult();
    }

    public void Clear(bool renderOnClear = true)
    {
        for (int row = 0; row < _colors.GetLength(1); row++)
        {
            for (byte col = 0; col < _colors.GetLength(0); col++)
            {
                _colors[col, row] = _defaultColor;
            }
        }

        if (renderOnClear)
            Render();
    }

    private void Render()
    {
        var grid = new Grid();

        for (byte col = 0; col < _colors.GetLength(0); col++)
        {
            grid.AddColumn(new GridColumn()
            {
                Padding = new Padding(0, 0, 2, 0),
            });
        }

        for (byte row = 0; row < _colors.GetLength(1); row++)
        {
            var cells = new List<IRenderable>();
            for (byte col = 0; col < _colors.GetLength(0); col++)
            {
                cells.Add(new Text("  ", new Style(null, _colors[col, row])));
            }
            grid.AddRow(cells.ToArray());
            grid.AddEmptyRow();
        }

        AnsiConsole.Clear();
        AnsiConsole.Write(grid);
    }
}