using UnityEngine;
using System.Collections;

public class StabRainPattern : AttackPattern
{
   
    private BulletSpawner[] spawners;

    private void Awake()
    {
        spawners = GetComponentsInChildren<BulletSpawner>();
        Debug.Log("Spawner count: " + spawners.Length);
    }

    public override IEnumerator Execute()
    {
        foreach (var s in spawners)
            s.StartFiring();

        yield return new WaitForSeconds(duration);

        foreach (var s in spawners)
            s.StopFiring();
    }
}
