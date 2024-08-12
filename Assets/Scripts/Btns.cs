using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btns : MonoBehaviour
{
    public Slider BTN_slider;
    public Transform BTN_slider_OBJ;
    public GameObject Btn_OBJ;
    bool IsHover = false;
    // Start is called before the first frame update
    void Start()
    {
        //BTN_slider_OBJ.DOScaleX(0, 0);
        BTN_slider.value = 0;
        Btn_OBJ.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHover)
        {
            Btn_OBJ.SetActive(true);
            //BTN_slider_OBJ.DOScale(new Vector3(1.05f, 1.05f, 1.05f),0.4f);
            if (BTN_slider.value >= 1)
            {
                BTN_slider.value = 1;
            }
            else
            {
                BTN_slider.value += Time.deltaTime*4;
            }
        }
        else
        {
            //BTN_slider_OBJ.DOScale(new Vector3(0,1,1), 0.4f);
            if (BTN_slider.value <= 0)
            {
                BTN_slider.value = 0;
                Btn_OBJ.SetActive(false);
            }
            else
            {
                BTN_slider.value -= Time.deltaTime*4;
            }
        }
        IsHover = false;
    }
    private void OnMouseOver()
    {
        IsHover = true;
    }
    private void OnMouseDown()
    {
        Debug.Log("버튼클릭");
    }
}
