using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public int startHealth = 30;
    public string enemyType;
    private float health;

    private bool repeat = false;

    public int reward = 20;

    Animator animator;

    private Transform target;
    private int waypointIndex = 0;

    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        target = Waypoints.points[0];
        health = startHealth;
        healthBar.color = Color.green;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;


        if ((health / startHealth) <= .2f && enemyType == "Raper")
        {
            speed = 2.5f; // изменить если слишком быстро бежит
        }

        if (health <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        int chanceOfRevival = Random.Range(1, 101);
        if (enemyType != "Zombie")
        {
            DestroyEnemy();
        }
        if (!repeat && chanceOfRevival <= 5) // шанс воскрешения зомби
        {
            health = startHealth;
            healthBar.fillAmount = health / startHealth;
            animator.SetTrigger("revival");
            repeat = true;
        }
        else
        {
            DestroyEnemy();
            repeat = false;
        }
    }

    void DestroyEnemy()
    {
        isDead = true;

        PlayerStats.Money += reward;
        animator.SetTrigger("didDie");

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject, 0.405f); // если не нравится как долго проигрывается анимация исправь время (удаление объекта)

    }

    void Update ()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.02f)
        {
            GetNextWaypoint();
            RotateIntoMoveDirection();
        }

        if (health / startHealth > 0.5f)
        {
            healthBar.color = Color.green;
        }

        if (health/startHealth <= .5f)
        {
            healthBar.color = Color.yellow;
        }

        if (health / startHealth <= .2f)
        {
            healthBar.color = Color.red;
        }

        if (GameManager.gameIsOver)
        {
            Destroy(gameObject);
        }
    }

    void GetNextWaypoint ()
    {
        if (waypointIndex >= Waypoints.points.Length - 1) 
        {

            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    private void RotateIntoMoveDirection()
    {
        //1
        Vector3 newStartPosition = Waypoints.points[waypointIndex].transform.position;
        Vector3 newEndPosition = Waypoints.points[waypointIndex + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);
        //2
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        //3
        //GameObject sprite = gameObject.transform.Find("walk8").gameObject;
        //sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }

}
