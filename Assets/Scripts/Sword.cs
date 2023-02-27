

using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]private Animator animator;
    private BoxCollider _boxCollider;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
    }


    private void Update()
    {
        if (animator.GetBool(Attack))
        {
            _boxCollider.isTrigger = true;
        }
        else
        {
            _boxCollider.isTrigger = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITakeDamage>(out var hit))
        { hit.TakeDamage();
        }
        
    }
}