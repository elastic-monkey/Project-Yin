using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour
{
    public Transform EnemiesContainer;
    public List<EnemyBehavior> Enemies;

    private void Awake()
    {
        Enemies = new List<EnemyBehavior>(EnemiesContainer.gameObject.GetComponentsInChildren<EnemyBehavior>());
    }
}
