using System;
using System.Collections;
using System.Collections.Generic;
using Poiterer.Utilities;
using UnityEngine;

public class GridGenericsTesting : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    private HeatMap heatMap;

    private Grid<StringGridObject> stringGrid;

    private void Start()
    {
        //heatMap = new HeatMap(20, 10, 10f);
        
        //heatMapVisual.SetHeatMapGrid(heatMap);

        stringGrid = new Grid<StringGridObject>(20, 10, 10f, (() => new StringGridObject()));
    }

    private void Update()
    {
        Vector3 position = UtilitiesClass.GetMouseWorldPosition();
        
        if (Input.GetMouseButtonDown(0))
        {
            //heatMap.AddValueInAreaDiamond(position, 100, 2,10);
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            stringGrid.GetGridObject(position).AddLetter("A");
            stringGrid.TriggerGridObjectChanged(position);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            stringGrid.GetGridObject(position).AddLetter("B");
            stringGrid.TriggerGridObjectChanged(position);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            stringGrid.GetGridObject(position).AddLetter("C");
            stringGrid.TriggerGridObjectChanged(position);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stringGrid.GetGridObject(position).AddNumber("1");
            stringGrid.TriggerGridObjectChanged(position);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stringGrid.GetGridObject(position).AddNumber("2");
            stringGrid.TriggerGridObjectChanged(position);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            stringGrid.GetGridObject(position).AddNumber("3");
            stringGrid.TriggerGridObjectChanged(position);
        }
        
    }
}

public class StringGridObject
{
    
    
    private string letters;
    private string numbers;

    public StringGridObject()
    {
        letters = "";
        numbers = "";
    }
    
    public void AddLetter(string letter)
    {
        letters += letter;
    }
    public void AddNumber(string number)
    {
        numbers += number;
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
