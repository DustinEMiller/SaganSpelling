using System;
using TMPro;
using UnityEngine;

public class CurrentMonsterTarget : MonoBehaviour
{
    private Monster _currentMonsterTarget;
    private HealthBar _healthBar;
    private TextMeshProUGUI _monsterName;
    void Start()
    {
        MonsterSpawner.Instance.OnMonsterDied += MonsterSpawner_MonsterDied;
        MonsterSpawner.Instance.OnMonsterSpawned += MonsterSpawner_MonsterSpawned;
        _healthBar = GetComponent<HealthBar>();
        _monsterName = GetComponentInChildren<TextMeshProUGUI>();
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
            _monsterName.text = monster.GetName();
        }
        else if (monster == null)
        {
            _healthBar.SetHealthSystem(null);
            _monsterName.text = "Monster";
        }
    }
    
    
}
