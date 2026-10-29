using UnityEngine;
using System.Collections;

public abstract class AttackPattern : MonoBehaviour
{
    [Header("Pattern Settings")]
    public float duration = 5f;

    public abstract IEnumerator Execute();
}
