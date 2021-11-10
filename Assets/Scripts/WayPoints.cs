using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> wayPoints = new List<GameObject>();
    public static WayPoints Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    public static List<GameObject> GetWaypoints { get { return Instance.wayPoints; } }
}
