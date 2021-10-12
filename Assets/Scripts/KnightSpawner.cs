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
            DespawnKnights();
        
        string letters = WordManager.Instance.GetLetters();
        
        for (var i = 0; i < 10; i++)
        {
            int num = Random.Range(0, letters.Length);
            char letter = letters[num];
            letters = letters.Remove(num, 1);
            
            Transform position = GameManager.Instance.GetWalkPoint(i);
            
            _knights.Add(Instantiate(_knightPrefab, position.position, Quaternion.identity));
            var knightInstance = _knights[_knights.Count - 1];
            knightInstance.AssignLetter(letter);

            if (i < 5)
                knightInstance.RaiseTextLabel();
        }
    }

    private void DespawnKnights()
    {
        foreach (Knight knight in _knights)
        {
            Destroy(knight.gameObject);
        }
        
        _knights.Clear();
    }
}
