using System;

public class Office
{
    private const char Floor = '1';
    private const char Wall = '0';

    private readonly char[][] _officeGrid;
    private readonly int _rowsCount;
    private readonly int _columnsCount;
    private bool[][] _visitedTiles;

    public Office(char[][] officeGrid)
    {
        this._officeGrid = officeGrid;
        this._rowsCount = officeGrid.GetLength(0);
        this._columnsCount = _officeGrid[0].GetLength(0);
        this.InitVisitedFloors();
    }

    public int GetNumberOfOffices()
    {
        int result = 0;

        for (int row = 0; row < _rowsCount; row++)
        {
            for (int col = 0; col < _columnsCount; col++)
            {
                bool isOffice = IsOffice(row, col);

                if (isOffice)
                {
                    result++;
                }
            }
        }
        

        return result;
    }

    public bool IsOffice(int row, int col)
    {
        if(!CanVisitTile(row, col))
        {
            return false;
        }

        bool isOffice = IsOfficeTile(row, col);
        if (isOffice)
        {
            VisitAdjacentFloors(row, col);
        }

        return isOffice;
    }

    public void VisitAdjacentFloors(int row, int col)
    {
        var isRightTileOffice = IsOfficeTile(row, col + 1);
        if (isRightTileOffice)
        {
            VisitAdjacentFloors(row, col + 1);
        }

        var isLeftTileOffice = IsOfficeTile(row + 1, col);
        if (isLeftTileOffice)
        {
            VisitAdjacentFloors(row + 1, col);
        }
    }

    private bool IsOfficeTile(int row, int col)
    {
        bool canVisitTile = CanVisitTile(row, col);
        if (!canVisitTile)
        {
            return false;
        }

        this._visitedTiles[row][col] = true;
        var tile = this._officeGrid[row][col];

        return tile == '1';
    }

    private bool CanVisitTile(int row, int col)
    {
        if (row < 0 || row >= this._rowsCount)
        {
            return false;
        }

        if (col < 0 || col >= this._columnsCount)
        {
            return false;
        }

        if (_visitedTiles[row][col])
        {
            return false;
        }

        return true;
    }

    private void InitVisitedFloors()
    {
        this._visitedTiles = new bool[this._rowsCount][];
        for (int i = 0; i < this._rowsCount; i++)
        {
            this._visitedTiles[i] = new bool[this._columnsCount];
        }
    }
}

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var matrix = GetMatrix();
            var office = new Office(matrix);
            var numOffices = office.GetNumberOfOffices();
            Console.WriteLine(numOffices);
        }

        public static char[][] GetMatrix()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            char[][] matrix = new char[rows][];
            for (var i = 0; i < rows; i++)
            {
                var line = Console.ReadLine();
                matrix[i] = line.ToCharArray();
            }
            return matrix;
        }
    }
}