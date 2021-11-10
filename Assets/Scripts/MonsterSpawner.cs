using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField] private Monster _monsterPrefab;
    private List<Monster> _monsters = new List<Monster>();
    
    public static MonsterSpawner Instance { get; private set; }
    public UnityEvent onMonsterSpawned;
    public UnityEvent onMonsterKilled;
    
    private float _nextSpawnTimer = 20f;
    private int _monstersKilled = 0;
    private void Awake()
    {
        Instance = this;
    }
    
    private void SpawnMonster()
    {
        if (_monsters == null)
            _monsters = new List<Monster>();
        
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

    public Monster CurrentTarget()
    {
        //Debug.Log(_monsters[0].transform.position);
        if (_monsters.Count == 0)
        {
            return null;
        }
        return _monsters[0];

    }

    public void RemoveMonster()
    {
        Monster monster = _monsters[0];
        _monstersKilled++;
        onMonsterKilled.Invoke();
        Destroy(monster.gameObject);
        _monsters.RemoveAt(0);
    }

    public int GetMonstersKilled()
    {
        return _monstersKilled;
    }
}
