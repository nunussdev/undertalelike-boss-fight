    using UnityEngine;

public class HeartMovement : MonoBehaviour
{
    
    public float speed = 5f;
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
    }
    public void HardStop()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;   
            rb.angularVelocity = 0f;
        }
    }

}
