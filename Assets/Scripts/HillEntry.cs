using UnityEngine;

public class HillEntry : MonoBehaviour
{
    public Collider2D[] hillColliders;
    public Collider2D[] boundaryCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D hill in hillColliders)
            {
                hill.enabled = false;
            }
            foreach (Collider2D boundary in boundaryCollider)
            {
                boundary.enabled = true;
            }
            
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

    }
}
