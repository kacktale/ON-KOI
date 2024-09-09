using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] firstFloorSpawns;  // 1층 스폰 지점 배열
    public Transform[] secondFloorSpawns; // 2층 스폰 지점 배열
    public GameObject enemy;              // 스폰할 적 객체
    public GameObject JumpScare;
    public float spawnInterval = 5f;      // 적이 스폰될 시간 간격
    public float enemyLifetime = 3f;      // 적의 생명 시간 (지속 시간)
    
    private GameObject currentEnemy;      // 현재 스폰된 적 객체

    private Transition transition;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transition = FindAnyObjectByType<Transition>();
        JumpScare.SetActive(false);
    }

    // 적을 주기적으로 스폰하는 코루틴
    public IEnumerator SpawnEnemyRoutine()
    {
        Debug.Log("실행시작");
        while (true)
        {
            // 현재 플레이어의 층을 확인
            bool isOnFirstFloor = IsPlayerOnFirstFloor();

            // 스폰 지점 배열 선택
            Transform[] spawns = isOnFirstFloor ? firstFloorSpawns : secondFloorSpawns;


            int a = UnityEngine.Random.Range(0, spawns.Length);
            // 적을 랜덤한 스폰 지점으로 이동
            Transform spawnPoint = spawns[a];

            StartCoroutine(SpawnEnemyAt(spawnPoint, a, isOnFirstFloor));

            // 적의 생명 시간을 기다린 후 적을 제거
            yield return new WaitForSeconds(enemyLifetime);
            Destroy(currentEnemy);

            // 스폰 간격만큼 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 플레이어가 1층에 있는지 확인
    private bool IsPlayerOnFirstFloor()
    {
        // 플레이어의 Y 좌표를 기반으로 층을 구분
        float playerY = Camera.main.transform.position.y; // 플레이어의 Y 좌표를 가져옴
        return playerY < 3.5f; // 층 높이를 기준으로 1층인지 2층인지 판단
    }

    // 적을 지정된 스폰 지점으로 이동 및 사다리 타기 동작 추가
    private IEnumerator SpawnEnemyAt(Transform spawnPoint,int SpawnLocation ,bool isOnFirstFloor)
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy); // 현재 적이 있다면 제거
        }

        currentEnemy = Instantiate(enemy); // 새 적 생성
        currentEnemy.transform.position = spawnPoint.position; // 스폰 지점으로 적의 위치 설정
        switch (SpawnLocation)
        {
            case 0:
                while (true)
                {
                    currentEnemy.transform.position += new Vector3(0.1f, 0, 0);
                    currentEnemy.transform.rotation = Quaternion.Euler(180, 0, 0);
                    yield return null;
                }
            case 1:
                while (true)
                {
                    currentEnemy.transform.position -= new Vector3(0.1f, 0, 0);
                    currentEnemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                    yield return null;
                }
            case 2:
                if (isOnFirstFloor)
                {
                    currentEnemy.transform.DOMoveY(-5f, spawnInterval);
                    yield return new WaitForSeconds(spawnInterval);
                    if(currentEnemy != null)
                    {
                        PlayerHit();
                    }
                }
                else
                {
                    currentEnemy.transform.DOMoveY(5f, spawnInterval);
                    yield return new WaitForSeconds(spawnInterval);
                    if (currentEnemy != null)
                    {
                        PlayerHit();
                    }
                }
                break;
        }
    }
    public void PlayerHit()
    {
        Destroy(currentEnemy);
        JumpScare.SetActive(true);
        Invoke("ReloadTransition", 0.5f);
    }
    private void ReloadTransition()
    {
        transition.GameOverTransition();
    }

    public void EnemyDeath()
    {
        audioSource.Play();
    }
}
