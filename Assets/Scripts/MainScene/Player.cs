using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("가져오는거")]
    Music music;
    LightToAttack LightToAttack;
    public AudioSource DefaultSound, TwoSeeSound,FootSteep,TwoSeeEfects;
    public Transform TwoSee;
    public GameObject TwoSeeObj,LightObj;
    bool IsTwoSeed = false;
    bool IsDefault = true;

    [Header("플레이어 설정")]
    Rigidbody2D rigid;
    public float PlayerSpeed = 5f;
    public float CurPlayerSpeed = 5f;
    public float PlayerJump = 5f;
    public float CurPlayerJump = 5f;
    public float CoolTime = 5f;
    public float CurCoolTime = 5f;

    [Header("콜라이더 & 인터랙션")]
    public bool IsGround = false;
    public bool IsPassword = false;
    public bool IsDoor = false;
    bool IsLight = false;

    [Header("부가설정")]
    public bool IsFlashGet = false;

    // Start is called before the first frame update
    void Start()
    {
        music = FindAnyObjectByType<Music>();

        LightToAttack = FindAnyObjectByType<LightToAttack>();

        TwoSeeObj.SetActive(false);

        DefaultSound = GameObject.Find("Click").GetComponents<AudioSource>()[1];
        TwoSeeSound = GameObject.Find("Click").GetComponents<AudioSource>()[0];
        DefaultSound.Play();
        TwoSeeSound.Play();
        TwoSeeEfects.Play();
        TwoSeeEfects.volume = 0;
        TwoSeeSound.volume = 0f;
        FootSteep.volume = music.SoundEffectValue;

        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        //오른쪽으로 바라보기
        if(h == 1 && !IsDoor && !IsLight)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (!FootSteep.isPlaying && IsGround)
            {
                FootSteep.Play();
            }
        }
        //왼쪽으로 바라보기
        else if (h == -1 && !IsDoor && !IsLight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            if (!FootSteep.isPlaying && IsGround)
            {
                FootSteep.Play();
            }
        }

        //인터랙션이 되지 않았을때
        if (!IsDoor && !IsLight)
        {
            Vector3 PlayerPos = new Vector3(h, 0, 0);
            transform.position += PlayerPos * CurPlayerSpeed * Time.deltaTime;
        }
    }
    private void Update()
    {
        //점프코드
        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) &&!IsPassword)
            {
                rigid.AddForce(Vector3.up * CurPlayerJump, ForceMode2D.Impulse);
                IsGround = false;
            }
        }

        //투시코드
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsTwoSeed)
        {
            TwoSeeObj.SetActive(true);
            IsDefault = false;
            TwoSee.DOScale(new Vector3(40, 40, 1), 0.4f).SetEase(Ease.InQuint);
            IsTwoSeed = true;
            StartCoroutine(OffTwoSee());
        }

        //플래쉬코드(보스전용)
        if (Input.GetKeyDown(KeyCode.Z) && CurCoolTime <= 0 && IsFlashGet && !IsDoor)
        {
            CurCoolTime = CoolTime;
            StartCoroutine(LightAttack());
        }

        //사운드 조절(투시 전)
        if (IsDefault && DefaultSound.volume * music.MusicValue <1)
        {
            if(DefaultSound.volume < music.MusicValue)
            {
                DefaultSound.volume += Time.deltaTime * 6;
                TwoSeeSound.volume -= Time.deltaTime * 6;
                TwoSeeEfects.volume -= Time.deltaTime * 6;
            }
            else
            {
                DefaultSound.volume = music.MusicValue;
                TwoSeeSound.volume =0;
                TwoSeeEfects.volume = 0;
            }
        }
        //사운드 조절(투시 중)
        else if(!IsDefault)
        {
            if(TwoSeeSound.volume >= music.MusicValue)
            {
                TwoSeeSound.volume = music.MusicValue;
                TwoSeeEfects.volume = music.SoundEffectValue;
                DefaultSound.volume =0;
            }
            DefaultSound.volume -= Time.deltaTime * 6;
            TwoSeeSound.volume += Time.deltaTime * 6;
            TwoSeeEfects.volume += Time.deltaTime * 6;
        }

        //쿨타임
        CurCoolTime -= Time.deltaTime;
    }

    //플래쉬 공격
    public IEnumerator LightAttack()
    {
        IsLight = true;
        LightObj.SetActive(true);

        yield return new WaitForSeconds(1);

        IsLight = false;
        LightObj.SetActive(false);
    }

    //투시 끄기
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
