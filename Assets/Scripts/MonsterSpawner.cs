using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField] private Monster _monsterPrefab;
    private List<Monster> _monsters = new List<Monster>();
    
    public UnityEvent onMonsterSpawned;
    
    private float _nextSpawnTimer = 20f;
    

    private void SpawnMonster()
    {
        _monsters.Add(Instantiate(_monsterPrefab, gameObject.transform.position, Quaternion.identity));
        onMonsterSpawned.Invoke();
    }

    private void Update()
    {
        _nextSpawnTimer -= Time.deltaTime;
        if (_monsters.Count == 0 || _nextSpawnTimer <= 0)
        {
            SpawnMonster();
            
            if(_nextSpawnTimer <= 0)
                _nextSpawnTimer = 30f;
        }
    }
}
