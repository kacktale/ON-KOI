using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ProGressBar : MonoBehaviour
{
    //��������
    public Transform Door;
    // Start is called before the first frame update
    void Start()
    {
        //UI�� �� ���� �߰� �ϱ�
        transform.position = Door.position + new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //UI�� �� ���� �߰� �ϱ�
        transform.position = Door.position + new Vector3(0,2,0);
    }
}
