using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField] private List<MonsterSO> _monsterSOList = new List<MonsterSO>();
    private List<Monster> _monsters = new List<Monster>();
    
    public static MonsterSpawner Instance { get; private set; }
    public event EventHandler OnMonsterSpawned;
    public event EventHandler OnMonsterDied;
    
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

        var _monsterType = _monsterSOList[Random.Range(0, _monsterSOList.Count)];
        
        _monsters.Add(Instantiate(_monsterType.prefab.gameObject.GetComponent<Monster>(), gameObject.transform.position, Quaternion.identity));
        OnMonsterSpawned?.Invoke(this, EventArgs.Empty);
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
        
        Destroy(monster.gameObject);
        
        _monsters.RemoveAt(0);
        OnMonsterDied?.Invoke(this, EventArgs.Empty);
    }

    public int GetMonstersKilled()
    {
        return _monstersKilled;
    }
}
