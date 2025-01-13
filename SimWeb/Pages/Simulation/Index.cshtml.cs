using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator.Maps;
using Simulator;

namespace SimWeb.Pages.Simulation;

public class IndexModel : PageModel
{
    public int CurrentTurn { get; private set; }
    public int TotalTurns { get; private set; }
    public int SizeX { get; private set; }
    public int SizeY { get; private set; }
    public Dictionary<Point, char>[] TurnLogs { get; private set; } = Array.Empty<Dictionary<Point, char>>();
    public int GridSize { get; private set; } // Właściwość GridSize
    public string[,] Grid { get; private set; }

    public IndexModel()
    {
        GridSize = 7; // Ustaw rozmiar siatki (np. 7x7)
        Grid = new string[GridSize, GridSize];
        CurrentTurn = 0;

        // Wypełnij początkową siatkę
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int y = 0; y < GridSize; y++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                Grid[x, y] = ""; // Domyślnie puste komórki
            }
        }

        // Przykładowe umiejscowienie orka i elfa
        Grid[3, 2] = "O"; // Ork
        Grid[4, 1] = "E"; // Elf
    }

    public void OnGet(int? turn)
    {
        // Definicja mapy (zakładając, że jest to mapa o rozmiarze 5x5)
        SizeX = 7;
        SizeY = 7;

        // Przygotowanie symulacji
        var map = new SmallSquareMap(SizeX);
        var creatures = new List<Creature>
        {
            new Orc("Gorbag"),
            new Elf("Elandor"),
        };
        var positions = new List<Point>
        {
            new Point(3, 2),
            new Point(4, 1),
        };
        var moves = "dlruuddlrldruldr";

        var simulation = new Simulator.Simulation(map, creatures, positions, moves);
        var turnLogs = new List<Dictionary<Point, char>>();

        // Symulacja
        while (!simulation.Finished)
        {
            turnLogs.Add(GetSymbols(simulation));
            simulation.Turn();
        }

        turnLogs.Add(GetSymbols(simulation)); // Dodaj ostatni stan
        TurnLogs = turnLogs.ToArray();

        // Ustawienie bieżącej i całkowitej liczby tur
        TotalTurns = TurnLogs.Length;
        CurrentTurn = turn ?? 0;

        // Ograniczenie CurrentTurn do dostępnych wartości
        if (CurrentTurn < 0) CurrentTurn = 0;
        if (CurrentTurn >= TotalTurns) CurrentTurn = TotalTurns - 1;
    }

    public char GetSymbol(int x, int y)
    {
        var point = new Point(x, y);
        return TurnLogs[CurrentTurn].TryGetValue(point, out var symbol) ? symbol : ' ';
    }

    private Dictionary<Point, char> GetSymbols(Simulator.Simulation simulation)
    {
        var symbols = new Dictionary<Point, char>();
        for (int i = 0; i < simulation.Creatures.Count; i++)
        {
            var creature = simulation.Creatures[i];
            var position = simulation.Positions[i];

            if (symbols.ContainsKey(position))
            {
                // Jeśli pole jest już zajęte, ustaw symbol "X"
                symbols[position] = 'X';
            }
            else
            {
                // Dodaj symbol odpowiedni dla stworzenia
                symbols[position] = creature switch
                {
                    Orc => 'O',
                    Elf => 'E',
                    _ => ' '
                };
            }
        }
        return symbols;
    }
}