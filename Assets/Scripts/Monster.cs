using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private int _currentWaypoint = 0;
    private HealthSystem _healthSystem;
    private MonsterSOHolder _monsterTypeHolder;
    private string _name;
    private bool _coolDown = false;
    private float _coolDownTimer;
    private bool _attacking = false;
    private Gate _gate;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _monsterTypeHolder = GetComponent<MonsterSOHolder>();
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetHealthAmountMax(_monsterTypeHolder.monster.maxHealth, true);
        _name =_monsterTypeHolder.monster.names[Random.Range(0, _monsterTypeHolder.monster.names.Count)];
        _coolDownTimer = _monsterTypeHolder.monster.atkSpeed;
    }

    void Start()
    {
        _navMeshAgent.speed = _monsterTypeHolder.monster.speed;
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        _healthSystem.OnDied += HealthSystem_OnDied;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Knight knight = collision.gameObject.GetComponent<Knight>();
        Gate gate = collision.gameObject.GetComponent<Gate>();

        if (knight != null)
        {
            _healthSystem.Damage(knight.Damage);
            Destroy(knight.gameObject);
        }

        //Move to own file
        if(gate != null)
        {
            _gate = gate;
            _attacking = true;
            _navMeshAgent.SetDestination(gameObject.transform.position);
            _animator.SetBool("Walk", false);
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
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f && !_attacking)
        {
            _animator.SetBool("Walk", true);
            NextWayPoint();
        }

        if (_attacking)
        {
            
            if (!_coolDown) {
                //This needs to be moved into a different file or some sort of logic for getting hit and not tightly coupled
                _animator.SetTrigger("Attack");
                HealthSystem gateHealthSystem = _gate.GetComponent<HealthSystem>();
                gateHealthSystem.Damage(_monsterTypeHolder.monster.damage);
                _coolDown = true;
            } 
            else {
                _coolDownTimer -= Time.deltaTime;
                if (_coolDownTimer <= 0)
                {
                    _coolDown = false;
                    _coolDownTimer = _monsterTypeHolder.monster.atkSpeed;
                }
            }
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

    public string GetName()
    {
        return _name;
    }
}