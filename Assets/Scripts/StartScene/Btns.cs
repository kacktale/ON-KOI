using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Btns : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Slider BTN_slider;
    public Transform BTN_slider_OBJ;
    public GameObject Btn_OBJ;
    bool IsHover = false;
    // Start is called before the first frame update
    void Start()
    {
        Text.DOColor(Color.white,0.4f);
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
                Text.color = Color.black;
                BTN_slider.value = 1;
            }
            else
            {
                Text.color += new Color(Time.deltaTime*100, Time.deltaTime*100, Time.deltaTime*100);
                BTN_slider.value += Time.deltaTime*4;
            }
        }
        else
        {
            //BTN_slider_OBJ.DOScale(new Vector3(0,1,1), 0.4f);
            if (BTN_slider.value <= 0)
            {
                Text.color = Color.white;
                BTN_slider.value = 0;
                Btn_OBJ.SetActive(false);
            }
            else
            {
                Text.color -= new Color(Time.deltaTime * 100, Time.deltaTime * 100, Time.deltaTime * 100);
                BTN_slider.value -= Time.deltaTime*4;
            }
        }
        IsHover = false;
    }
    private void OnMouseOver()
    {
        IsHover = true;
    }
}
