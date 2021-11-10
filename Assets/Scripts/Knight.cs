using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Knight : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private bool _attacking = false;
    private float _damage;

    [SerializeField] private TextMeshPro _letterLabel;
    
    public float Damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
        }
    }

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0f);

        //Knights should have a state enum?
        if (_attacking)
        {
            var monster = MonsterSpawner.Instance.CurrentTarget();

            if (monster != null)
                WalkToDestination(monster.transform);
            else
                _navMeshAgent.ResetPath();
            
        }
    }

    public void WalkToDestination(Transform destination)
    {
        _navMeshAgent.SetDestination(destination.position);
    }

    public void Attack()
    {
        _attacking = true;
        _letterLabel.enabled = false;
    }
}