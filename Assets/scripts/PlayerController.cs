using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    

    [Tooltip("How fast the player moves across the screen")][SerializeField] float sensitivity = 1f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3.5f;

    [Header("Screen Position based Tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -10f;
    [SerializeField] float controlYawFactor = -10f;

    Component[] particleSystems;

    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * sensitivity;
        float yOffset = yThrow * Time.deltaTime * sensitivity;


        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToControlThrow + pitchDueToPosition;
        
        //yaw is position based
        float yaw = transform.localPosition.x * controlYawFactor;

        //roll is control based
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring() {
        particleSystems = GetComponentsInChildren<ParticleSystem>();


        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else {
            SetLasersActive(false);
        }
    }
    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.gameObject.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
            //laser.SetActive(true);
        }
    }
}
