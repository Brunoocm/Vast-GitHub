using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObjects : ScriptableObject
{
    public int health;
    public int attack;

    public bool startStopped;

    public float speed;
    public float seeRadius;
    public float attackRadius;

    public LayerMask layerAttack;
}
