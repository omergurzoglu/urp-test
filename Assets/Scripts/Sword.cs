

using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private BoxCollider boxCollider;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Combo = Animator.StringToHash("Combo");

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if ((animator.GetBool(Attack) || animator.GetBool(Combo)) && collision.gameObject.TryGetComponent<ITakeDamage>(out var hit))
        {
            hit.TakeDamage();
            
        }
    }
}