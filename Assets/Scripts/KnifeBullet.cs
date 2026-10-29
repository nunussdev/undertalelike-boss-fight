using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnifeBullet : MonoBehaviour
{
      
    public float rotation = 0f;
    public float speed = 1f;


    private Vector2 spawnPoint;
    


    
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }


   
    void Update()
    {

    }


    private Vector2 Movement(float timer)
    {
        
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Lazer çarptı: " + other.name);
        
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return; 
        }

        
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage(10); 
            }

            Destroy(gameObject);
        }
    }





}
