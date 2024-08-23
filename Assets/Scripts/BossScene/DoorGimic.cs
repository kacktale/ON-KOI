using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorGimic : MonoBehaviour
{
    public Slider Processing,O2Val;
    public bool IsOpend = false;
    public bool Opend = false;
    // Start is called before the first frame update
    void Start()
    {
        Processing = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Opend)
        {
            if (IsOpend)
            {
                Processing.value -= Time.deltaTime * 0.5f;
            }
            if (Processing.value <= 0)
            {
                O2Val.value--;
                Opend = true;
            }
        }
    }
}
