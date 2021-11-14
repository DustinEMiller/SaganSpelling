using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Monster")]
public class MonsterSO : ScriptableObject
{
    public Transform prefab;
    public string name;
    public int maxHealth;
    public float speed;
}
