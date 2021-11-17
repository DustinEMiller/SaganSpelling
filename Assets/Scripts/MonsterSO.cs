using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Monster")]
public class MonsterSO : ScriptableObject
{
    public GameObject prefab;
    public string name;
    public int maxHealth;
    public float speed;
    public int damage;
    public float atkSpeed;
}
