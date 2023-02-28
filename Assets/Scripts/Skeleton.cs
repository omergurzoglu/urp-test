
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Skeleton : Enemy

{
    private NavMeshAgent _agent;
    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsIdle = Animator.StringToHash("IsIdle");
    private CapsuleCollider _collider;
    private Coroutine _patrolCoroutine,_attackCoroutine;
    private static readonly int Attack = Animator.StringToHash("Attack");
    [SerializeField]private ParticleSystem _fx;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
        _agent = GetComponent<NavMeshAgent>();
        _patrolCoroutine=StartCoroutine(PatrolCoroutine());
    }

    private void Start()
    {
        SetRigidBodyStates(true);
        SetColliderStates(false);
    }

    public void PlayerFound()
    {
        
        StopCoroutine(_patrolCoroutine);
        _agent.ResetPath();
        _attackCoroutine = StartCoroutine(AttackCoroutine());
        
    }
    
    
    private IEnumerator PatrolCoroutine()
    {
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 5;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 5, NavMesh.AllAreas);
            
            _agent.SetDestination(hit.position);
            SwitchToWalkAnim();
            
            yield return new WaitUntil(() => !_agent.pathPending && _agent.remainingDistance < 2.6f);
            
            SwitchToIdleAnim();
            yield return new WaitForSeconds(7f);
        }
    }

    private void SwitchToWalkAnim()
    {
        _animator.SetBool(IsMoving, true);
        _animator.SetBool(IsIdle,false);
    }

    private void SwitchToIdleAnim()
    {
        _animator.SetBool(IsMoving, false);
        _animator.SetBool(IsIdle,true);
    }

    private void StopAllAnim()
    {
        _animator.SetBool(IsMoving,false);
        _animator.SetBool(IsIdle,false);
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            SwitchToWalkAnim();
            while (Vector3.Distance(transform.position, Antagonist.transform.position) > _agent.stoppingDistance)
            {
                _agent.SetDestination(Antagonist.transform.position);
                yield return null;
            }
            
            _agent.isStopped = true;
            _animator.SetTrigger(Attack);
            yield return new WaitForSeconds(4f);
            _agent.isStopped = false;
            SwitchToIdleAnim();
            yield return new WaitForSeconds(1f);
            

        }
    }
    
    public override void TakeDamage()
    {
        base.TakeDamage();
        _fx.Play();
        if (Health < 1)
        {
            Kill();
        }
    }

    private void Kill()
    {
        StopCoroutine(_attackCoroutine);
        _animator.enabled = false;
        _agent.isStopped = true; 
        SetRigidBodyStates(false);
        SetColliderStates(true);
        Destroy(gameObject,5f);
    }

    private void SetRigidBodyStates(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies )
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }
    private void SetColliderStates(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders )
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

   
}