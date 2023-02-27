using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITest : MonoBehaviour
{
    public Transform stackingUI;
    private void Start()
    {
        Vector3 screenPoint = stackingUI.position + new Vector3(0, 0, -5.0f); //UIobjesi
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
        transform.position = worldPos; //cameranın child objesi
    }
}
