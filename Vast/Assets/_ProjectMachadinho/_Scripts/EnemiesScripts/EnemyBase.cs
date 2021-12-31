using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyScriptableObjects enemyScriptableObjects;

    private int health;
    private int attack;

    private bool startStopped;
    private bool inRadius;
    private bool canWalk;

    private float speed;
    private float seeRadius;
    private float attackRadius;

    private LayerMask layerAttack;
    private GameObject target;
    private Animator anim => GetComponent<Animator>();

    void Start()
    {
        UpdateValues();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        inRadius = Physics2D.OverlapCircle(transform.position, seeRadius, layerAttack);

        if(inRadius)
        {
            FollowObject(target);
        }

    }

    void FollowObject(GameObject target)
    {
        if(startStopped)
        {
            anim.SetTrigger("SpawnTrigger");
        }
    }
    void UpdateValues()
    {
        health = enemyScriptableObjects.health;
        attack = enemyScriptableObjects.attack;
        startStopped = enemyScriptableObjects.startStopped;
        speed = enemyScriptableObjects.speed;
        seeRadius = enemyScriptableObjects.seeRadius;
        attackRadius = enemyScriptableObjects.attackRadius;
        layerAttack = enemyScriptableObjects.layerAttack;

    }

    public void CanWalkAnim()
    {
        canWalk = true;
    }
}
