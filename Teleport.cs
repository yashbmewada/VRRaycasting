/*
 * 
 * Teleportation script for 5UDE VR simulator.
 * Author: Yash Mewada
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Teleport : MonoBehaviour {


    // Inspector parameters
    [Tooltip("The tracking device used to determine absolute direction for steering.")]
    public CommonTracker tracker;

 
    [Tooltip("A button required to be pressed to activate steering.")]
    public CommonButton teleportButton;


    [Tooltip("The space that is translated by this interaction. Usually set to the physical tracking space.")]
    public CommonSpace space;

    [Tooltip("The median speed for movement expressed in meters per second.")]
    public float speed = 1.0f;



    //creates a laser for teleporting
    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;


    //creating the reticle for target to teleport to.
    [Tooltip("The target marker for the teleporting script.")]
    public GameObject teleportReticlePrefab;

    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    public bool shouldTeleport;



    // Use this for initialization
    void Start () {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
       // myText = GameObject.Find("Text").GetComponent<Text>();
       // myText.color = Color.clear;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!teleportButton.GetPress())
        {
            laser.SetActive(false);
            reticle.SetActive(false);
           

        }

        if(teleportButton.GetPress()){
            RaycastHit hit;
            if (Physics.Raycast(tracker.transform.position, tracker.transform.forward, out hit, teleportMask))
            {
                hitPoint = hit.point;
                showLaser(hit);
                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
        }

        if (shouldTeleport && !teleportButton.GetPress())
        {
            TeleportToTarget();
        }





    }

    private void showLaser(RaycastHit hit){

        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(tracker.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
            

    }

    public void TeleportToTarget(){

        shouldTeleport = false;

        reticle.SetActive(false);

      
        space.transform.position = hitPoint;

    }


}
