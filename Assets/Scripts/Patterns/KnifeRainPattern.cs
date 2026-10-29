using UnityEngine;
using System.Collections;

public class KnifeRainPattern : AttackPattern
{
    public Transform target;          
    public float aimOffset = 0.6f;   

    public GameObject knifePrefab;

    public float warningTime = 0.6f;
    public float knifeTime = 0.25f;      
    public float delayBetween = 0.15f;

    public float knifeSpeed = 18f;       

    private BulletSpawner[] spawners;

    private void Awake()
    {
        spawners = GetComponentsInChildren<BulletSpawner>();
        Debug.Log("Spawner count: " + spawners.Length);
    }

    public override IEnumerator Execute()
    {
        // shuffle
        for (int i = 0; i < spawners.Length; i++)
        {
            int rnd = Random.Range(i, spawners.Length);
            var temp = spawners[i];
            spawners[i] = spawners[rnd];
            spawners[rnd] = temp;
        }

        foreach (var s in spawners)
        {
            
            GameObject knife = Instantiate(knifePrefab, s.transform.position, Quaternion.identity);

            
            Vector2 tpos = target.position;

            
            tpos += Random.insideUnitCircle * aimOffset;

            
            Vector2 dir = (tpos - (Vector2)s.transform.position).normalized;

            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            knife.transform.rotation = Quaternion.Euler(0, 0, angle);

            
            Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = dir * knifeSpeed;
            }

            yield return new WaitForSeconds(delayBetween);
        }

    }


}