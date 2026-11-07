using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private int width = 18;
    [SerializeField] private int height = 10;
    [SerializeField] private Vector2Int originCell = Vector2Int.zero;

    private bool[,] occupiedCells;

    private void Awake()
    {
        if(grid == null)
            grid = GetComponent<Grid>();

        occupiedCells = new bool[width, height];
    }

    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        return grid.WorldToCell(worldPos);
    }

    public Vector3 GetCellCenter(Vector3Int cell)
    {
        return grid.GetCellCenterWorld(cell);
    }

    public bool IsInsideGrid(Vector3Int cell)
    {
        return cell.x >= originCell.x
            && cell.x < originCell.x + width
            && cell.y >= originCell.y
            && cell.y < originCell.y + height;
    }

    private Vector2Int CellToIndex(Vector3Int cell)
    {
        return new Vector2Int
            (cell.x - originCell.x,
            cell.y - originCell.y);
    }

    public bool IsOccupied(Vector3Int cell)
    {
        if (!IsInsideGrid(cell))
            return true;

        Vector2Int index = CellToIndex(cell);
        return occupiedCells[index.x, index.y];
    }

    public bool TryOccupyCell(Vector3Int cell)
    {
        if(IsOccupied(cell))
        {
            return false;
        }

        Vector2Int index = CellToIndex(cell);
        occupiedCells[index.x, index.y] = true;
        return true;
    }

    public void FreeCell(Vector3Int cell)
    {
        if(IsInsideGrid(cell))
        {
            Vector2Int index = CellToIndex(cell);
            occupiedCells[index.x, index.y] = false;
        }
    }
}
