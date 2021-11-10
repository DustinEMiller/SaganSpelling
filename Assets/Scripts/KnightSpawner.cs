using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class KnightSpawner : MonoBehaviour
{
    public static KnightSpawner Instance { get; private set; }

    [SerializeField] private Knight _knightPrefab;
    private List<Knight> _knights = new List<Knight>();

    private void Awake()
    {
        Instance = this;
    }
    
    public void SpawnKnights()
    {
        if (_knights.Count > 0)
            ClearWaitingBattalion();
        
        string letters = WordManager.Instance.GetLetters();
        
        for (var i = 0; i < 10; i++)
        {
            int num = Random.Range(0, letters.Length);
            char letter = letters[num];
            letters = letters.Remove(num, 1);
            
            Transform position = GameManager.Instance.GetWalkPoint(i);
            
            _knights.Add(Instantiate(_knightPrefab, position.position, Quaternion.identity));
            Knight knightInstance = _knights[_knights.Count - 1];
            knightInstance.GetComponentInChildren<KnightLetter>().AssignLetter(letter);

            if (i < 5)
                knightInstance.GetComponentInChildren<KnightLetter>().RaiseTextLabel();
        }
    }

    private void ClearWaitingBattalion()
    {
        _knights.Clear();
    }

    public void SetDamage(float damage)
    {
        foreach (Knight knight in _knights)
        {
            knight.Damage = damage;
            knight.Attack();
        }
    }
}
