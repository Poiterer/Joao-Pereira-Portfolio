using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Poiterer.Utilities;

public class Grid<TGridObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private TGridObject[,] gridArray;

    private float cellSize;

    private Vector3 originPosition;
    

    public Grid(int width, int height, float cellSize, Func<TGridObject> createGridObject, Vector3 originPosition = new Vector3())
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        
        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject();
            }
        }

        bool showDebug = true;
        if (showDebug)
        {
            TextMesh[,] debugTextArray  = new TextMesh[width, height];
            Debug.Log("Created " + width + " by " + height + " grid");

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    //Debug.Log(x + ", " + y);
                    debugTextArray[x,y] = UtilitiesClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width,height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }
    
    
    public int GetWidth()
    {
        return width;
    }
    
    public int GetHeight()
    {
        return height;
    }
    
    public float GetCellSize()
    {
        return cellSize;
    }
    
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >=0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            TriggerGridObjectChanged(x,y);
        }

    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }
    public void TriggerGridObjectChanged(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        TriggerGridObjectChanged(x, y);
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >=0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    } 
    
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

}

