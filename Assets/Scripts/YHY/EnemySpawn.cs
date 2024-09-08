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

            // 적을 랜덤한 스폰 지점으로 이동
            Transform spawnPoint = spawns[Random.Range(0, spawns.Length)];
            SpawnEnemyAt(spawnPoint);

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
    private void SpawnEnemyAt(Transform spawnPoint)
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy); // 현재 적이 있다면 제거
        }

        currentEnemy = Instantiate(enemy); // 새 적 생성
        currentEnemy.transform.position = spawnPoint.position; // 스폰 지점으로 적의 위치 설정

        // 스폰 지점의 태그를 확인하여 사다리 타기 동작 수행
        if (spawnPoint.CompareTag("Up"))
        {
            // 사다리 타고 올라가는 동작 (1층 -> 2층)
            StartCoroutine(MoveEnemyAlongLadder(Vector3.up * (4.4f - spawnPoint.position.y)));
        }
        else if (spawnPoint.CompareTag("Down"))
        {
            // 사다리 타고 내려가는 동작 (2층 -> 1층)
            StartCoroutine(MoveEnemyAlongLadder(Vector3.down * (spawnPoint.position.y - -3.1f)));
        }
        else
        {
            // 사다리가 아닌 경우, 각 층의 고정된 Y 좌표로 이동
            float targetY = spawnPoint.position.y < 3.5f ? -3.1f : 4.4f;
            Vector3 position = spawnPoint.position;
            position.y = targetY;
            currentEnemy.transform.position = position;
        }
    }

    // 적을 사다리를 따라 이동시키는 코루틴
    private IEnumerator MoveEnemyAlongLadder(Vector3 direction)
    {
        float ladderSpeed = 7f; // 사다리 타는 속도
        float ladderMovementDistance = direction.magnitude; // 사다리 이동 거리

        float startTime = Time.time;
        Vector3 startPosition = currentEnemy.transform.position;
        Vector3 endPosition = startPosition + direction;

        while (Time.time < startTime + (ladderMovementDistance / ladderSpeed))
        {
            float t = (Time.time - startTime) * ladderSpeed / ladderMovementDistance;
            currentEnemy.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // 이동 완료 후 위치를 정확히 설정
        currentEnemy.transform.position = endPosition;

        // 이동 후 자연스럽게 걷기 시작 (기존 걷기 로직으로 이동)
        // 이 부분은 적의 걷기 로직과 연결해야 할 수 있습니다.
    }
    public void PlayerHit()
    {
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
