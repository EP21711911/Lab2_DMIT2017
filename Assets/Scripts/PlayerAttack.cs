using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject slashEffectSide;
    public GameObject slashEffectUp;
    public GameObject slashEffectDown;

    public Transform attackSpawnPoint;

    public Animator animator;
    public Rigidbody2D rb;
    private float attackCooldown = 2f;
    private float attackTimer;
    private Vector2 lastMovementDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slashEffectSide != null)
        {
            // Check if the instance is active in the entire hierarchy
            if (!slashEffectSide.activeInHierarchy)
            {
                slashEffectSide.SetActive(true);
            }
        }
                attackTimer -= Time.deltaTime;
        //if (Input.GetMouseButtonDown(0) && attackTimer <= 0 && !animator.GetBool("isAttacking"))
        if (Input.GetMouseButtonDown(0) && attackTimer <= 0)
        {
            animator.SetBool("isAttacking", true);
            rb.linearVelocity = Vector2.zero;
            attackTimer = attackCooldown;
            PlaySlashEffect();
            Debug.Log("Player is attacking");

        }
        lastMovementDirection = new Vector2(animator.GetFloat("xinput"), animator.GetFloat("yinput"));

    }

    void PlaySlashEffect()
    {
        GameObject prefabToSpawn = slashEffectSide; // default

        //check direction
        if (lastMovementDirection.y > 0.5f)
        {
            prefabToSpawn = slashEffectUp;
        }
        else
            if(lastMovementDirection.y <-0.5f)
        {
            prefabToSpawn.SetActive(false);
        }
        else
            if (lastMovementDirection.x > 0.5f)
        {
            prefabToSpawn.GetComponent<SpriteRenderer>().flipX = false;
            prefabToSpawn = slashEffectSide;
        }
        else
            if (lastMovementDirection.x < -0.5f)
        {
            prefabToSpawn.GetComponent<SpriteRenderer>().flipX = true;
            prefabToSpawn = slashEffectSide;
        }

        Destroy(Instantiate(prefabToSpawn, attackSpawnPoint.position, Quaternion.identity), 0.3f);
    }

    public void SetLastMoveDirection(Vector2 dir)
    {
        if(dir != Vector2.zero)
        {
            lastMovementDirection = dir;
        }
    }

    // Called via animation Event
    public void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }
}
