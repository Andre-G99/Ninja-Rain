using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour
{
    public float targetRatio = 16f / 9f;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.aspect = targetRatio;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
