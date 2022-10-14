using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapVisual : MonoBehaviour
{
    private HeatMap heatMap;
    private Mesh mesh;
    private bool updateMesh;
    
    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateHeatMapVisual();
        }
    }

    public void SetHeatMapGrid(HeatMap heatMap)
    {
        this.heatMap = heatMap;
        UpdateHeatMapVisual();
        heatMap.grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }
    
    private void Grid_OnGridValueChanged(object sender, Grid<int>.OnGridValueChangedEventArgs onGridValueChangedEventArgs)
    {
            //Debug.Log("Cenas");
            updateMesh = true;
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtilities.CreateEmptyMeshArrays(heatMap.grid.GetWidth() * heatMap.grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < heatMap.grid.GetWidth(); x++)
        {
            for (int y = 0; y < heatMap.grid.GetHeight(); y++)
            {
                int index = x * heatMap.grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * heatMap.grid.GetCellSize();
                //Debug.Log(index);

                int gridValue = heatMap.GetGridValue(x, y);
                float gridValueNormalized = (float)gridValue / HeatMap.HEATMAP_MAX_VALUE;
                Vector2 gridValueUV = new Vector2(gridValueNormalized, 0);
                MeshUtilities.AddToMeshArrays(vertices, uv, triangles, index, heatMap.grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
