using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap
{
    public const int HEATMAP_MIN_VALUE = 0;
    public const int HEATMAP_MAX_VALUE = 100;
    
    public Grid<int> grid { get; private set; }

    public HeatMap()
    {
        
    }

    public HeatMap(Grid<int> grid)
    {
        this.grid = grid;
    }

    public HeatMap(int width, int height, float cellSize, Vector3 originPosition = new Vector3())
    {
        grid = new Grid<int>(width, height, cellSize, () => default, originPosition);
    }

    public void SetGrid(Grid<int> newGrid)
    {
        grid = newGrid;
    }

    public int GetGridValue(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
    public int GetGridValue(Vector3 worldPosition)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        return GetGridValue(x, y);
    }
    
    public void SetGridValue(int x, int y, int value)
    {
        grid.SetGridObject(x,y,Mathf.Clamp(value,HEATMAP_MIN_VALUE, HEATMAP_MAX_VALUE));
    }
    public void SetGridValue(Vector3 worldPosition, int value)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        SetGridValue(x,y,value);
    }

    public void AddGridValue(int x, int y, int value)
    {
        SetGridValue(x,y,grid.GetGridObject(x, y) + value);
    }
    
    public void AddValueInAreaDiamond(Vector3 worldPosition, int value, int fullValueRange, int totalRange = 0)
    {
        if (totalRange < fullValueRange)
        {
            totalRange = fullValueRange;
        }

        int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));
        
        grid.GetXY(worldPosition, out int originX, out int originY);
        for (int x = 0; x < totalRange; x++)
        {
            for (int y = 0; y < totalRange - x; y++)
            {
                int radius = x + y;
                int addValueAmount = value;

                if (radius > fullValueRange)
                {
                    addValueAmount -= lowerValueAmount * (radius - fullValueRange);
                }
                
                AddGridValue(originX + x, originY + y, addValueAmount);
                if (x != 0)
                {
                    AddGridValue(originX - x, originY + y, addValueAmount);
                }
                if (y != 0)
                {
                    AddGridValue(originX + x, originY - y, addValueAmount);
                    if (x != 0)
                    {
                        AddGridValue(originX - x, originY - y, addValueAmount);
                    }
                }
            }
        }
    }
}
