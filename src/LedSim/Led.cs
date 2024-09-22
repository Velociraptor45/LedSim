using Spectre.Console;
using Spectre.Console.Rendering;

namespace LedSim;

public class Led : IDisposable
{
    private readonly Color[,] _colors;
    private readonly Color _defaultColor;
    private readonly Table _table;
    private readonly CancellationTokenSource _cts = new();

    public Led(int columnCount, int rowCount) : this(columnCount, rowCount, Color.Black)
    {
    }

    public Led(int columnCount, int rowCount, Color defaultColor)
    {
        _colors = new Color[columnCount, rowCount];
        _defaultColor = defaultColor;
        _table = new Table
        {
            ShowHeaders = false,
            ShowRowSeparators = true,
            ShowFooters = false,
        };

        Render();

        Console.CancelKeyPress += delegate
        {
            Dispose();
        };
    }

    public Color this[int column, int row]
    {
        get => _colors[column, row];
        set
        {
            _colors[column, row] = value;
            _table.Rows.Update(row, column, GetText(value));
        }
    }

    public void Wait(int milliseconds)
    {
        Task.Delay(milliseconds).GetAwaiter().GetResult();
    }

    public void Clear()
    {
        for (int row = 0; row < _colors.GetLength(1); row++)
        {
            for (byte col = 0; col < _colors.GetLength(0); col++)
            {
                this[col, row] = _defaultColor;
            }
        }
    }

    private void Render()
    {
        for (byte col = 0; col < _colors.GetLength(0); col++)
        {
            _table.AddColumn(new TableColumn(string.Empty));
        }

        for (byte row = 0; row < _colors.GetLength(1); row++)
        {
            var cells = new List<IRenderable>();
            for (byte col = 0; col < _colors.GetLength(0); col++)
            {
                cells.Add(GetText(_defaultColor));
            }
            _table.AddRow(cells.ToArray());
        }

        Task.Run(() =>
            AnsiConsole.Live(_table)
                .AutoClear(false)
                .StartAsync(ctx =>
                {
                    while (true)
                    {
                        ctx.Refresh();
                    }
                }), _cts.Token);
    }

    private Text GetText(Color color)
    {
        return new Text("  ", new Style(null, color));
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}