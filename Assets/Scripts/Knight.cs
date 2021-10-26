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

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0f);

        //Knights should have a state enum?
        if (_attacking)
        {
            WalkToDestination(MonsterSpawner.Instance.CurrentTarget().transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if(monster != null)
        {
            //monster.GetComponent<HealthSystem>().Damage(_damage);
            Destroy(gameObject);
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