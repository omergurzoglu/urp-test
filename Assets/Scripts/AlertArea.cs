
using System;

using UnityEngine;

public class AlertArea : MonoBehaviour
{
    public static event Action<GameObject> EnemyAlerted; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Skeleton>(out var skeleton))
        {
            if (!skeleton.IsAlerted())
            {
                skeleton.Alerted = true;
                OnEnemyAlerted(gameObject);
                skeleton.PlayerFound();
            }
            
        }
    }

    private static void OnEnemyAlerted(GameObject obj)
    {
        EnemyAlerted?.Invoke(obj);
    }
}
