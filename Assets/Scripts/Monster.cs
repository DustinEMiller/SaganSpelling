using UnityEngine;

public class Monster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Knight knight = collision.GetComponent<Knight>();
        if(knight != null)
        {
            MonsterSpawner.Instance.RemoveMonster();
            Destroy(gameObject);
        }
    }
}