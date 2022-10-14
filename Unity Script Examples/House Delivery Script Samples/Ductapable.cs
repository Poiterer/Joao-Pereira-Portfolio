using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ductapable : MonoBehaviour
{
    private bool isDuctaped = false;
    [SerializeField]
    private GameObject tapePrefab;

    public void TapeInPlace()
    {
        if (!isDuctaped)
        {
            tapePrefab.SetActive(true);
            isDuctaped = true;

            StartCoroutine(TapedTimer());
        }
    }

    public bool GetTapedStatus()
    {
        return isDuctaped;
    }

    private IEnumerator TapedTimer()
    {
        yield return new WaitForSeconds(15f);

        tapePrefab.SetActive(false);
        isDuctaped = false;
    }
}
