using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public event Action OnEnemyDestroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            OnEnemyDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
