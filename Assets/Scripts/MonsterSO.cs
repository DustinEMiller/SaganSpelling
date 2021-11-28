using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Monster")]
public class MonsterSO : ScriptableObject
{
    public GameObject prefab;
    public string type;
    public int maxHealth;
    public float speed;
    public int damage;
    public float atkSpeed;
    public List<string> names;
}
