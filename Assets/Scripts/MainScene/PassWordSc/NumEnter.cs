using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumEnter : MonoBehaviour
{
    public Password Password;
    public TextMeshPro txt_CurPassWord;
    public int PassWord;
    public bool IsArrow = false;
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
        if (IsArrow)
        {
            if (PassWord == 1)
            {
                txt_CurPassWord.text += "ก่";
            }
            else if (PassWord == 2)
            {
                txt_CurPassWord.text += "ก็";
            }
            else if (PassWord == 3)
            {
                txt_CurPassWord.text += "ก้";
            }
            else
            {
                txt_CurPassWord.text += "กๆ";
            }
        }
        else
        {
            txt_CurPassWord.text += PassWord.ToString();
        }
    }
}
