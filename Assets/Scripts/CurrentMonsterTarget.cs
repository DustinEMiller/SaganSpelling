using System;
using UnityEngine;

public class CurrentMonsterTarget : MonoBehaviour
{
    private Monster _currentMonsterTarget;
    private HealthBar _healthBar;
    void Start()
    {
        MonsterSpawner.Instance.OnMonsterDied += MonsterSpawner_MonsterDied;
        MonsterSpawner.Instance.OnMonsterSpawned += MonsterSpawner_MonsterSpawned;
        _healthBar = GetComponent<HealthBar>();
    }

    private void MonsterSpawner_MonsterDied(object sender, EventArgs e)
    {
        _currentMonsterTarget = null;
        GetTarget();
    }
    
    private void MonsterSpawner_MonsterSpawned(object sender, EventArgs e)
    {
        GetTarget();
    }

    private void GetTarget()
    {
        Monster monster = MonsterSpawner.Instance.CurrentTarget();
        if (_currentMonsterTarget != monster || _currentMonsterTarget == null)
        {
            _currentMonsterTarget = monster;
            _healthBar.SetHealthSystem(monster.GetComponent<HealthSystem>());
        }
        else if (monster == null)
        {
            _healthBar.SetHealthSystem(null);
        }
    }
    
    
}
