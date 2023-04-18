using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;

    Scoreboard scoreboard;
    [SerializeField] int points = 10;

    [SerializeField] int hitPoints = 100;
    BoxCollider[] BoxColliders;

    private void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        InitializeEnemyCollisions();
        //find with tag gives gameobjects initially
        parent = GameObject.FindGameObjectWithTag("SpawnAtRuntime").transform;
    }

    private void InitializeEnemyCollisions()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log($"{name} was hit by {other.gameObject.name}");
        ProcessHit();

        //Quick shortcut
        //type out what we want to be done, then highlight it and press ctrl + .
        //this creates a new method from what is highlighted
        //we can also press f2 on a method to change the name of it everywhere

        if (hitPoints < 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints -= 10;
        GameObject fx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        scoreboard.increaseScore(points);
        vfx.transform.parent = parent;
        Destroy(this.gameObject);
    }



}
