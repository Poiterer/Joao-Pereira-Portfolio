using System.Collections;
using System.Collections.Generic;
using Poiterer.Utilities;
using UnityEngine;

public class HeatMapTesting : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    private HeatMap heatMap;
    void Start()
    {
        heatMap = new HeatMap(40, 20, 5f);
        heatMapVisual.SetHeatMapGrid(heatMap);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilitiesClass.GetMouseWorldPosition();
            heatMap.AddValueInAreaDiamond(position,100,2, 10);
        }
    }
}
