using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuctapeScript : MonoBehaviour
{
    
    private int chargesLeft = 3;
    [SerializeField] private GameObject phase1Mesh;
    [SerializeField] private GameObject phase2Mesh;
    [SerializeField] private GameObject phase3Mesh;

    public void UseCharge()
    {
        chargesLeft--;
        //place ductape on the object
        switch (chargesLeft)
        {
            case 3:
                phase1Mesh.SetActive(true);
                phase2Mesh.SetActive(false);
                phase3Mesh.SetActive(false);
                break;

            case 2:
                phase1Mesh.SetActive(false);
                phase2Mesh.SetActive(true);
                phase3Mesh.SetActive(false);
                break;

            case 1:
                phase1Mesh.SetActive(false);
                phase2Mesh.SetActive(false);
                phase3Mesh.SetActive(true);
                break;
        }

        if (chargesLeft < 1)
        {
            Destroy(gameObject);
        }
    }

    public int CurrentCharges()
    {
        return chargesLeft;
    }

    public List<GameObject> GetMeshes()
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(phase1Mesh);
        list.Add(phase2Mesh);
        list.Add(phase3Mesh);

        return list;
    }
}
