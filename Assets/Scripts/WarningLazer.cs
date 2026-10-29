using UnityEngine;
using System.Collections;

public class WarningLazer : MonoBehaviour
{
    public float warningTime = 0.7f;
    public GameObject realLaserPrefab;

    public IEnumerator ExecuteWarning()
    {
        
        gameObject.SetActive(true);

        yield return new WaitForSeconds(warningTime);

        
        Instantiate(realLaserPrefab, transform.position, transform.rotation);

        
        Destroy(gameObject);
    }
}
