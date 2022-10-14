using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Poiterer.Utilities;

public class GridTestScript : MonoBehaviour
{
    private Grid<bool> grid;
    
    private void Start()
    {
        grid = new Grid<bool>(4, 2, 10, () => default(bool));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetGridObject(UtilitiesClass.GetMouseWorldPosition(), true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetGridObject(UtilitiesClass.GetMouseWorldPosition()));
        }
    }
}
