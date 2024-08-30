using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorGimic : MonoBehaviour
{
    public Slider Processing, O2Val;
    public bool IsOpend = false; // 문이 열림
    public bool Opend = false; // 문이 열려서 연기가 들어옴
    public Image DoorWarning,O2Col;
    bool isWarningActive = false;

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

                if (!isWarningActive)
                {
                    StartCoroutine(DorWarning());
                }
            }

            if (Processing.value <= 0)
            {
                O2Val.value--;
                O2Col.color += new Color(85,0,0);
                Opend = true;
            }
        }
    }

    IEnumerator DorWarning()
    {
        isWarningActive = true;
        DoorWarning.DOFade(0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        DoorWarning.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);

        isWarningActive = false;
    }
}
