using System.Collections.Generic;
using System.Linq;

namespace Connect4
{
    public class Game
    {
        private readonly List<Cell> _grid = CreateGrid().ToList();
        private int _turn = 0;
        private string[] Players { get; } = {"Red", "Yellow"};

        public bool Winner =>
            Columns()
                .Any(row => row.Count(c => c.Color == CurrentTurn) > 3);

        public string CurrentTurn => Players[_turn];

        public void Select(int column)
        {
            var availableColumn = _grid.FirstOrDefault(pos => pos.X == column && pos.Color == null);
            if (availableColumn == null) return;

            availableColumn.Color = CurrentTurn;
            _turn++;
            if (_turn >= Players.Length) _turn = 0;
        }

        public Cell Find(int x, int y)
        {
            return Rows()
                .SelectMany(q => q.Where(cell => cell.X == x))
                .Single(cell => cell.Y == y);
        }
        
        private static IEnumerable<Cell> CreateGrid() =>
            Enumerable.Range(0, 6)
                .SelectMany(GenerateRow);

        private static IEnumerable<Cell> GenerateRow(int y) =>
            Enumerable.Range(0, 7)
                .Select(x => new Cell(x, y));

        public IEnumerable<IEnumerable<Cell>> Rows() =>
            _grid.Where(pos => pos.X == 0)
                .OrderByDescending(l => l.Y)
                .Select(row => _grid.Where(cell => cell.Y == row.Y));
        
        private IEnumerable<IEnumerable<Cell>> Columns() =>
            _grid.Where(q => q.Y == 0)
                .Select(rowNumber => _grid.Where(cell => cell.X == rowNumber.X));
    }
}