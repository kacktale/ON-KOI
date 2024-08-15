using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource DefaultSound, TwoSeeSound;
    public Transform TwoSee;
    public GameObject TwoSeeObj;
    bool IsTwoSeed = false;
    bool IsDefault = true;
    [Header("플레이어 설정")]
    Rigidbody2D rigid;
    public float PlayerSpeed = 5f;
    public float CurPlayerSpeed = 5f;
    public float PlayerJump = 5f;
    public float CurPlayerJump = 5f;
    public bool IsGround = false;
    public bool IsPassword = false;
    // Start is called before the first frame update
    void Start()
    {
        
        TwoSeeObj.SetActive(false);
        DefaultSound = GameObject.Find("Click").GetComponents<AudioSource>()[1];
        TwoSeeSound = GameObject.Find("Click").GetComponents<AudioSource>()[0];
        DefaultSound.Play();
        TwoSeeSound.Play();
        TwoSeeSound.volume = 0f;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 PlayerPos = new Vector3(h, 0, 0);
        transform.position += PlayerPos * CurPlayerSpeed * Time.deltaTime;
    }
    private void Update()
    {
        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) &&!IsPassword)
            {
                rigid.AddForce(Vector3.up * CurPlayerJump, ForceMode2D.Impulse);
                IsGround = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsTwoSeed)
        {
            TwoSeeObj.SetActive(true);
            IsDefault = false;
            TwoSee.DOScale(new Vector3(40, 40, 1), 0.4f).SetEase(Ease.InQuint);
            IsTwoSeed = true;
            StartCoroutine(OffTwoSee());
        }
        if(IsDefault && DefaultSound.volume <1)
        {
            DefaultSound.volume += Time.deltaTime * 6;
            TwoSeeSound.volume -= Time.deltaTime * 6;
        }
        else if(!IsDefault)
        {
            if(TwoSeeSound.volume >= 1)
            {
                TwoSeeSound.volume = 1;
                DefaultSound.volume =0;
            }
            DefaultSound.volume -= Time.deltaTime * 6;
            TwoSeeSound.volume += Time.deltaTime * 6;
        }
    }
    public IEnumerator OffTwoSee()
    {
        yield return new WaitForSeconds(5);
        IsDefault = true;
        TwoSee.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.4f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(0.4f);
        TwoSeeObj.SetActive(false);
        yield return new WaitForSeconds(2);
        IsTwoSeed = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
        }
    }
}
