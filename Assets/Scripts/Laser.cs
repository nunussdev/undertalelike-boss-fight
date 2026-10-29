using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public Collider2D col;
    public float activeTime = 0.2f;

    private void Awake()
    {
        col.enabled = false; 
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.05f);

        col.enabled = true; 
        yield return new WaitForSeconds(activeTime);

        Destroy(gameObject);
    }
}
