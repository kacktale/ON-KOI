using DG.Tweening;
using System;
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


            int a = UnityEngine.Random.Range(0, spawns.Length);
            // ���� ������ ���� �������� �̵�
            Transform spawnPoint = spawns[a];

            StartCoroutine(SpawnEnemyAt(spawnPoint, a, isOnFirstFloor));

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
    private IEnumerator SpawnEnemyAt(Transform spawnPoint,int SpawnLocation ,bool isOnFirstFloor)
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy); // ���� ���� �ִٸ� ����
        }

        currentEnemy = Instantiate(enemy); // �� �� ����
        currentEnemy.transform.position = spawnPoint.position; // ���� �������� ���� ��ġ ����
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
