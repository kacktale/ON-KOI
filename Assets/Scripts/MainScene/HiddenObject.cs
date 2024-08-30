using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    public bool IsAppear = false;
    SpriteRenderer spriteRenderer;
    public TextMeshPro textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color -= new Color(0, 0, 0, 255);
        textMeshProUGUI.color -= new Color(0, 0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAppear)
        {
            textMeshProUGUI.DOFade(1, 0.4f);
            spriteRenderer.DOFade(1, 0.4f);
        }
        else
        {
            textMeshProUGUI.DOFade(0, 0.4f);
            spriteRenderer.DOFade(0, 0.4f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TwoSee"))
        {
            IsAppear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TwoSee"))
        {
            IsAppear = false;
        }
    }
}
