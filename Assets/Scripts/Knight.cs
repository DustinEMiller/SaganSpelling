using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Knight : MonoBehaviour
{
    [SerializeField] private TextMeshPro _letterLabel;
    [SerializeField] private CapsuleCollider _letterCollider;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private char _letter;
    private bool _selected = false;
    private int _index = -1;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0f);
        _letterLabel.transform.LookAt(Camera.main.transform);
        _letterLabel.transform.localScale = new Vector3(-1, 1,1);
    }

    private void OnMouseDown()
    {
        if (_selected)
        {
            _selected = false;
            WordManager.Instance.RemoveKnight(this);
            ChangeTextColor(Color.white);
        }
        else
        {
            _selected = true;
            WordManager.Instance.SelectKnight(this);
            ChangeTextColor(Color.green);
        }
    }

    private void ChangeTextColor(Color color)
    {
        _letterLabel.color = color;
    }

    public void AssignLetter(char letter)
    {
        _letterLabel.text = letter.ToString();
        _letter = letter;
    }
    
    public char GetLetter()
    {
        return _letter;
    }

    public void WalkToDestination(Transform destination)
    {
        _navMeshAgent.SetDestination(destination.position);
    }

    public void RaiseTextLabel()
    {
        Vector3 position = new Vector3(0,1 , 0); 
        _letterLabel.transform.position += position;
        _letterCollider.center += position;
    }

    public void SetIndex(int index)
    {
        _index = index;
    }
    
    public int GetIndex()
    {
        return _index;
    }
}