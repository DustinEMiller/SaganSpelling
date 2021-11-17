using System;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private int _currentWaypoint = 0;
    private HealthSystem _healthSystem;
    private MonsterSOHolder _monsterTypeHolder;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _monsterTypeHolder = GetComponent<MonsterSOHolder>();
    }

    void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _navMeshAgent.speed = _monsterTypeHolder.monster.speed;
        _healthSystem.SetHealthAmountMax(_monsterTypeHolder.monster.maxHealth, true);
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        _healthSystem.OnDied += HealthSystem_OnDied;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Knight knight = collision.gameObject.GetComponent<Knight>();
        
        if(knight != null)
        {
            _healthSystem.Damage(knight.Damage);
            Destroy(knight.gameObject);
        }
    }
    
    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        //SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
        //SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        //Instantiate(GameAssets.Instance.BuildingDestroyedParticles, transform.position, Quaternion.identity);
        MonsterSpawner.Instance.RemoveMonster();
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