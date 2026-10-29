using UnityEngine;
using System.Collections;

public class SideWavePattern : AttackPattern
{
    public GameObject warningPrefab;
    public GameObject laserPrefab;

    public float warningTime = 0.6f;
    public float laserTime = 0.25f;
    public float delayBetween = 0.15f;

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
            GameObject warn = Instantiate(
                warningPrefab,
                s.transform.position,
                s.transform.rotation
            );

            yield return new WaitForSeconds(warningTime);
            Destroy(warn);

            GameObject laser = Instantiate(
                laserPrefab,
                s.transform.position,
                s.transform.rotation
            );

            yield return new WaitForSeconds(laserTime);
            Destroy(laser);

            yield return new WaitForSeconds(delayBetween);
        }
    }

}
