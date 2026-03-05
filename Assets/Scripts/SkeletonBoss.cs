using UnityEngine;
using System.Collections;

public class SkeletonBoss : MonoBehaviour {
    [Header("Enemy Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float stoppingDistance = 1f;
    public float health = 10f;
    [SerializeField] private GameObject enemy;
    private Transform player;

    [Header("Attack Settings")]
    [SerializeField] private float attackRadius = 6.2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private SpriteRenderer attackIndicator;

    private bool isAttacking = false;
    ProfileData profile;
    private void Start()
    {
        profile = SaveManager.instance.currentProfile;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        // Make sure we actually found the player
        if (player != null && !isAttacking && health > 0)
        {
            float distanceFromPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceFromPlayer <= detectionRadius)
            {
                FlipTowardsPlayer();

                if (distanceFromPlayer > stoppingDistance)
                {
                    // Move towards the player
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                else
                {
                    // We reached the stopping distance therfore we start the attack sequence
                    StartCoroutine(AttackSequence());
                }
            }
        }
        else
            if (health <= 0)
        {
            Destroy(enemy);
        }
    }

    private void FlipTowardsPlayer()
    {
        Transform EnemyT = enemy.GetComponent<Transform>();

        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1, EnemyT.localScale.z);
        }
        else if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1,1,EnemyT.localScale.z);
        }
    }

    private IEnumerator AttackSequence()
    {
        // Lock the enemy in place so they don't slide while attacking
        isAttacking = true;

        // Flicker Yellow 3 times (The Warning)
        for (int i = 0; i < 3; i++)
        {
            // Set to semi-transparent yellow
            attackIndicator.color = new Color(1f, 0.92f, 0.016f, 0.5f);
            yield return new WaitForSeconds(0.2f);

            // Turn invisible
            attackIndicator.color = Color.clear;
            yield return new WaitForSeconds(0.2f);
        }

        // Turn Red ey (The Strike)
        attackIndicator.color = new Color(1f, 0f, 0f, 0.5f); // Semi-transparent red

        // Check if the player is actually inside the blast radius right now
        float distanceFromPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceFromPlayer <= attackRadius)
        {

            Debug.Log("Player got hit! Minus one heart!");


            profile.Hearts -= 1;
        }

        // Leave the red circle on screen for a split second so the player sees it
        yield return new WaitForSeconds(0.3f);

        // Hide the circle and wait for the cooldown before moving again
        attackIndicator.color = Color.clear;
        yield return new WaitForSeconds(attackCooldown);

        // Unlock the enemy so they can chase you again
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Yellow circle for the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Red circle for the stopping distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("PlayerWeapon"))
        {
            health -= 1;
            Debug.LogWarning($"This enemy took dmg; their health is now {health}");

        }
    }
}
