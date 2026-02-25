using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 5;
    private float lastHorizontalAxis = 0;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log(horizontal);

        if (horizontal < 0)
        {
        spriteRenderer.flipX = true;
        }
        else 
        { 
        spriteRenderer.flipX= false;
        }
        animator.SetFloat("Xinput", horizontal);
        animator.SetFloat("Yinput", vertical);


        rb.linearVelocity = new Vector2 (horizontal, vertical) * speed;

    }

}
