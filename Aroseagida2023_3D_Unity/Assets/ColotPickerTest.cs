using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColotPickerTest : MonoBehaviour
{

    public FlexibleColorPicker fcp;
    public Material m;
    //public Color newColor;
    private TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m.color = fcp.color;
        
    }
}
