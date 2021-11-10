using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private int _currentWaypoint = 0;
    
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Knight knight = collision.gameObject.GetComponent<Knight>();
        if(knight != null)
        {
            //monster.GetComponent<HealthSystem>().Damage(_damage);
            MonsterSpawner.Instance.RemoveMonster();
            Destroy(knight.gameObject);
        }
    }
    
    public void WalkToDestination(Transform destination)
    {
        _navMeshAgent.SetDestination(destination.position);
    }
    
    void Update()
    {
        _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0f);
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
        {
            NextWayPoint();
        }
    }
    
    void NextWayPoint()
    {
        if (_currentWaypoint == WayPoints.GetWaypoints.Count - 1)
        {
            _navMeshAgent.ResetPath();
        }
        else
        {
            // set destination to waypoint
            _currentWaypoint++;
            var target = WayPoints.GetWaypoints[_currentWaypoint];
            WalkToDestination(target.transform);
        }
    }
}