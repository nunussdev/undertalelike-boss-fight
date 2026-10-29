using UnityEngine;

public class Knife : MonoBehaviour
{
    public int damage = 15;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.TakeDamage(damage);
        }
    }
}
