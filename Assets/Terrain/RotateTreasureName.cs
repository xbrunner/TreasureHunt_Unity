using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateTreasureName : MonoBehaviour
{

    public TextMeshPro benthselaminian; // the TextMeshPro for local use
    public TextMeshPro staff; // the TextMeshPro for local use
    public TextMeshPro common; // the TextMeshPro for local use

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotCamera = Camera.main.transform.rotation;
        benthselaminian.transform.rotation = rotCamera;
        staff.transform.rotation = rotCamera;
        common.transform.rotation = rotCamera;
    }
}
