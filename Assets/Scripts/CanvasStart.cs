using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasStart : MonoBehaviour
{
    CanvasScaler scaler;
    // Start is called before the first frame update
    void Start()
    {
        scaler = GetComponent<CanvasScaler>();
        scaler.scaleFactor = 5.55f;
    }

    // Update is called once per frame
    void Update()
    {
        if(scaler.scaleFactor <= 1)
        {
            scaler.scaleFactor = 1;
            return;
        }
        else
        {
            scaler.scaleFactor -= Time.deltaTime*10;
        }
    }
}
