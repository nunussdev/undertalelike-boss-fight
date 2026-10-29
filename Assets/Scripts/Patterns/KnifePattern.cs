using UnityEngine;
using System.Collections;

public class KnifePattern : AttackPattern
{
    public GameObject warningPrefab;
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
        
        for (int i = 0; i < spawners.Length; i++)
        {
            int rnd = Random.Range(i, spawners.Length);
            var temp = spawners[i];
            spawners[i] = spawners[rnd];
            spawners[rnd] = temp;
        }

        foreach (var s in spawners)
        {
            
            GameObject warn = Instantiate(warningPrefab, s.transform.position, s.transform.rotation);
            yield return new WaitForSeconds(warningTime);
            Destroy(warn);

          
            GameObject knife = Instantiate(knifePrefab, s.transform.position, s.transform.rotation);

            
            Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
                rb.linearVelocity = (Vector2)knife.transform.right * knifeSpeed;
            }



            yield return new WaitForSeconds(delayBetween);
        }
    }
}
