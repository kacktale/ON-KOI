using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] firstFloorSpawns;  // 1�� ���� ���� �迭
    public Transform[] secondFloorSpawns; // 2�� ���� ���� �迭
    public GameObject enemy;              // ������ �� ��ü
    public GameObject JumpScare;
    public float spawnInterval = 5f;      // ���� ������ �ð� ����
    public float enemyLifetime = 3f;      // ���� ���� �ð� (���� �ð�)
    
    private GameObject currentEnemy;      // ���� ������ �� ��ü

    private Transition transition;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transition = FindAnyObjectByType<Transition>();
        JumpScare.SetActive(false);
    }

    // ���� �ֱ������� �����ϴ� �ڷ�ƾ
    public IEnumerator SpawnEnemyRoutine()
    {
        Debug.Log("�������");
        while (true)
        {
            // ���� �÷��̾��� ���� Ȯ��
            bool isOnFirstFloor = IsPlayerOnFirstFloor();

            // ���� ���� �迭 ����
            Transform[] spawns = isOnFirstFloor ? firstFloorSpawns : secondFloorSpawns;

            // ���� ������ ���� �������� �̵�
            Transform spawnPoint = spawns[Random.Range(0, spawns.Length)];
            SpawnEnemyAt(spawnPoint);

            // ���� ���� �ð��� ��ٸ� �� ���� ����
            yield return new WaitForSeconds(enemyLifetime);
            Destroy(currentEnemy);

            // ���� ���ݸ�ŭ ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // �÷��̾ 1���� �ִ��� Ȯ��
    private bool IsPlayerOnFirstFloor()
    {
        // �÷��̾��� Y ��ǥ�� ������� ���� ����
        float playerY = Camera.main.transform.position.y; // �÷��̾��� Y ��ǥ�� ������
        return playerY < 3.5f; // �� ���̸� �������� 1������ 2������ �Ǵ�
    }

    // ���� ������ ���� �������� �̵� �� ��ٸ� Ÿ�� ���� �߰�
    private void SpawnEnemyAt(Transform spawnPoint)
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy); // ���� ���� �ִٸ� ����
        }

        currentEnemy = Instantiate(enemy); // �� �� ����
        currentEnemy.transform.position = spawnPoint.position; // ���� �������� ���� ��ġ ����

        // ���� ������ �±׸� Ȯ���Ͽ� ��ٸ� Ÿ�� ���� ����
        if (spawnPoint.CompareTag("Up"))
        {
            // ��ٸ� Ÿ�� �ö󰡴� ���� (1�� -> 2��)
            StartCoroutine(MoveEnemyAlongLadder(Vector3.up * (4.4f - spawnPoint.position.y)));
        }
        else if (spawnPoint.CompareTag("Down"))
        {
            // ��ٸ� Ÿ�� �������� ���� (2�� -> 1��)
            StartCoroutine(MoveEnemyAlongLadder(Vector3.down * (spawnPoint.position.y - -3.1f)));
        }
        else
        {
            // ��ٸ��� �ƴ� ���, �� ���� ������ Y ��ǥ�� �̵�
            float targetY = spawnPoint.position.y < 3.5f ? -3.1f : 4.4f;
            Vector3 position = spawnPoint.position;
            position.y = targetY;
            currentEnemy.transform.position = position;
        }
    }

    // ���� ��ٸ��� ���� �̵���Ű�� �ڷ�ƾ
    private IEnumerator MoveEnemyAlongLadder(Vector3 direction)
    {
        float ladderSpeed = 7f; // ��ٸ� Ÿ�� �ӵ�
        float ladderMovementDistance = direction.magnitude; // ��ٸ� �̵� �Ÿ�

        float startTime = Time.time;
        Vector3 startPosition = currentEnemy.transform.position;
        Vector3 endPosition = startPosition + direction;

        while (Time.time < startTime + (ladderMovementDistance / ladderSpeed))
        {
            float t = (Time.time - startTime) * ladderSpeed / ladderMovementDistance;
            currentEnemy.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // �̵� �Ϸ� �� ��ġ�� ��Ȯ�� ����
        currentEnemy.transform.position = endPosition;

        // �̵� �� �ڿ������� �ȱ� ���� (���� �ȱ� �������� �̵�)
        // �� �κ��� ���� �ȱ� ������ �����ؾ� �� �� �ֽ��ϴ�.
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
