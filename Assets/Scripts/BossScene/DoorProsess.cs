using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorProsess : MonoBehaviour
{
    public DoorGimic[] DoorproCessing;
    public Image[] ProCessings;
    public bool DoorWarning;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DoorGimic", 12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoorGimic()
    {
        int DoorSelect = Random.Range(0, DoorproCessing.Length);

        DoorproCessing[DoorSelect].IsOpend = true;
        ProCessings[DoorSelect].color = new Color(255,0,0);

        int NextDoor = Random.Range(10, 16);
        Invoke("DoorGimic", NextDoor);
    }
}
