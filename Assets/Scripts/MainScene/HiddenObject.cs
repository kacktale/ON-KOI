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
            textMeshProUGUI.DOColor(new Color(1, 1, 1, 4), 0.4f);
            spriteRenderer.DOColor(new Color(1, 1, 1, 4), 0.4f);
        }
        else
        {
            textMeshProUGUI.DOColor(new Color(1, 1, 1, 0), 0.4f);
            spriteRenderer.DOColor(new Color(1, 1, 1, 0), 0.4f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TwoSee"))
        {
            IsAppear = true;
            Invoke("OffTwosee", 4.5f);
        }
    }
    void OffTwosee()
    {
        IsAppear = false;
    }
}
