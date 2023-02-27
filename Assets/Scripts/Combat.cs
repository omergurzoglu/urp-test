
using System;
using UnityEngine;

public class Combat : MonoBehaviour
{
   private Animator _animator;
   private static readonly int AttackBool = Animator.StringToHash("Attack");

   private void Awake()
   {
      _animator = GetComponent<Animator>();
      
   }

   private void Update()
   {
      Attack();
   }

   private void Attack()
   {
      if (Input.GetKey(KeyCode.F))
      {
         _animator.SetBool(AttackBool,true);
      }
      else
      {
         _animator.SetBool(AttackBool,false);
      }
   }

   public void StartRootMotion()
   {
      _animator.applyRootMotion = true;
   }
   public void EndRootMotion()
   {
      _animator.applyRootMotion = false;
   }
}
