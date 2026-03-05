using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    [Header("Animation Settings")]
    public GameObject fadeObject;
    private Animator animator;
    private float animationDuration = 2;

    private void Start()
    {
        animator = fadeObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            animator.SetBool("Teleporting", true);
            StartCoroutine(TeleportSequence(collision.gameObject));


        }
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; //Stop the player dead in their tracks
            rb.simulated = false;       // Turn off physics temporarily so they don't fall
        }
        MonoBehaviour movementScript = player.GetComponent<PlayerMovement>();
        if (movementScript != null)
        {
            movementScript.enabled = false; //Get movement Script
        }
        yield return new WaitForSeconds(animationDuration);
        animator.SetBool("Teleporting", false);
        player.transform.position = destination.position;
        // Revert
        if (rb != null)
        {
            rb.simulated = true;
        }
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }
    }
}
