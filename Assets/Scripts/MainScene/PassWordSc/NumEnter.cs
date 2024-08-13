using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumEnter : MonoBehaviour
{
    public TextMeshPro txt_CurPassWord;
    public int PassWord;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        txt_CurPassWord.text += PassWord.ToString();
    }
}
