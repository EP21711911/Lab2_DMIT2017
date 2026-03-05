using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public float speed = 5;
    private float lastHorizontalAxis = 0;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDirection = Vector2.down;


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        Debug.Log(movement);

        if (lastMoveDirection.x <0)
        {
        spriteRenderer.flipX = true;
        }
        else 
        { 
        spriteRenderer.flipX= false;
        }

        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement;
            playerAttack.SetLastMoveDirection(movement);
        }

        animator.SetFloat("Xinput", lastMoveDirection.x);
        animator.SetFloat("Yinput", lastMoveDirection.y);
        animator.SetFloat("Speed", movement.magnitude);


        rb.linearVelocity = new Vector2 (movement.x, movement.y) * speed;

    }

}
