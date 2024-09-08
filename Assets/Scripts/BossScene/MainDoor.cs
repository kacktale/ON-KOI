using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//보스 전용 코드
public class MainDoor : MonoBehaviour
{
    [Header("진행 프로그래스 바")]
    public Slider Progress;
    public GameObject ProgressObj;
    public bool Started = false;
    
    Player Player;
    EnemySpawn enemySpawn;

    DoorProsess DoorProsess;
    [Header("추가설정")]
    public Transform SeeWa,DoorUl,O2Ul;
    public bool IsPlayerTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = FindAnyObjectByType<EnemySpawn>();

        DoorProsess = FindAnyObjectByType<DoorProsess>();

        ProgressObj.SetActive(false);

        Progress.value = 0;

        Player = FindAnyObjectByType<Player>();

        DoorUl.position += new Vector3(0, 5, 0);
        O2Ul.position += new Vector3(-13,0,0);

        SeeWa.position = new Vector3(0.21f, 69, 0);
        SeeWa.localScale = new Vector3(25.13f, 32f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (IsPlayerTouched)
            {
                if (!Started)
                {
                    Started = true;
                    DoorUl.DOMoveY(3.21f, 0.7f).SetEase(Ease.OutQuint);
                    O2Ul.DOMoveX(-0.59f, 0.7f).SetEase(Ease.OutQuint);
                    int StartGameTime = Random.Range(9, 12);
                    int StartEnemySpawn = Random.Range(13, 16);
                    Invoke("StartEnemySpawn", StartEnemySpawn);
                    Invoke("StartDoor", StartGameTime);
                }
                Player.IsDoor = true;

                ProgressObj.SetActive(true);

                Player.CurPlayerJump = 0;
                Player.CurPlayerSpeed = 0;
                Player.IsPassword = true;

                SeeWa.DOScale(new Vector3(1, 1.28f, 1), 0.4f).SetEase(Ease.OutQuint);
                SeeWa.DOLocalMove(new Vector3(0.21f,2.7f,0),0.4f).SetEase(Ease.OutQuint);

                if(Progress.value > 180)
                {
                    Progress.value = 180;
                }

                Progress.value += Time.deltaTime;
            }
        }

        //컨트롤 버튼 땔 시 시야 확대&움직임
        if (Input.GetKeyUp(KeyCode.LeftControl) && IsPlayerTouched)
        {
            ProgressObj.SetActive(false);

            StartCoroutine(PlayerRecover());

            SeeWa.DOScale(new Vector3(25.13f, 32f, 1), 0.4f).SetEase(Ease.InQuint);
            SeeWa.DOLocalMove(new Vector3(0.21f, 69, 0), 0.4f).SetEase(Ease.InQuint);
        }
    }
    //문 열림 시작
    void StartDoor()
    {
        DoorProsess.DoorGimic();
    }
    void StartEnemySpawn()
    {
        StartCoroutine(enemySpawn.SpawnEnemyRoutine());
    }

    //플레이어 원상복구
    IEnumerator PlayerRecover()
    {
        yield return new WaitForSeconds(0.4f);

        Player.IsDoor = false;
        Player.CurPlayerJump = Player.PlayerJump;
        Player.CurPlayerSpeed = Player.PlayerSpeed;
        Player.IsPassword = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerTouched = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerTouched = false;
        }
    }
}
