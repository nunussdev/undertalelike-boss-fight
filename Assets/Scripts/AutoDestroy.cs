using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float life = 0.7f;
    void Start() => Destroy(gameObject, life);
}
