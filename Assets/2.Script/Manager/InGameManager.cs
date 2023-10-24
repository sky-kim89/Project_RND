using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    static public InGameManager Instance;

    public List<Unit> UserUnits = new List<Unit>();
    public List<Unit> EnemyUnits = new List<Unit>();


    void Awake()
    {
        Instance = this;
    }
}
