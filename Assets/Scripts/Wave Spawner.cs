using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;


    public float timeBetweenWaves = 10f;
    private float countdown = 2f; // ����� �� ������ ������ ����� (����� ����� ����� ������� (�������))
    private int waveIndex = 0;

    public Text waveCountdownText;

    public GameManager gameManager;

    void Update ()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime; // ������� ����������� ������ 2f (2�) �� 1

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        if (!GameManager.gameIsOver)
        { 
            if (Mathf.Round(countdown) <= 10f)
            {
                waveCountdownText.text = "Next wave: " + string.Format("{0:00.00}", countdown);
            }
            else
            {
                waveCountdownText.text = "";
            }
        }
        else
        {
            waveCountdownText.text = "";     
        }
    }

    IEnumerator SpawnWave () // IENumerator ��������� ��������� ��� �� ����� ��� ����������
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            if (GameManager.gameIsOver)
            {
                break;
            }
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1.5f); // �������� �� ������� ��� ���������� ����
        }
        waveIndex++;

    }

    void SpawnEnemy (GameObject enemy) // ��������� ������ 
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); // ���� ������ ����� ������
    }
}
