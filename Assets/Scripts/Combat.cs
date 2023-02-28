
using System;
using UnityEngine;

public class Combat : MonoBehaviour
{
   private Animator _animator;
   private static readonly int AttackBool = Animator.StringToHash("Attack");
   private static readonly int Roll1 = Animator.StringToHash("Roll");
   private static readonly int Combo = Animator.StringToHash("Combo");

   private void Awake()
   {
      _animator = GetComponent<Animator>();
      
   }

   private void Update()
   {
      Attack();
      Special();
   }

   private void Special()
   {
      if (Input.GetKeyDown(KeyCode.R))
      {
         _animator.SetTrigger(Roll1);
      }

      if (Input.GetKey(KeyCode.G))
      {
         _animator.SetBool(Combo,true);
      }
      else
      {
         _animator.SetBool(Combo,false);
      }
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
