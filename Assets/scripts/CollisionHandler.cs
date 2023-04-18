using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LoadDelay = 1f;
    [SerializeField] ParticleSystem ExplosionVFX;

    void OnTriggerEnter(Collider other)
    {
        //string interpolation - fstrings in python but called string interpolation in c#
        Debug.Log($"{this.name} ** triggered by ** {other.gameObject.name}");
        startCrashSequence();
        ExplosionVFX.Play();

    }

    private void startCrashSequence()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        StartCoroutine(ReloadScene());
        
        //Invoke("ReloadLevel", LoadDelay);//this is a simple way of calling a method
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator ReloadScene() {
        yield return new WaitForSeconds(LoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
