/* *  * Teleportation script for 5UDE VR simulator. * Author: Yash Mewada *  */











using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class RayCast : MonoBehaviour {    // Inspector parameters    [Tooltip("The tracking device used to determine absolute direction for steering.")]    public CommonTracker tracker;     [Tooltip("A button required to be pressed to activate steering.")]    public CommonButton raycastButton;        [Tooltip("The space that is translated by this interaction. Usually set to the physical tracking space.")]    public CommonSpace space;    [Tooltip("The median speed for movement expressed in meters per second.")]    public float speed = 1.0f;    //creates a laser for teleporting    public GameObject laserPrefab;    private GameObject laser;    private Transform laserTransform;    private Vector3 hitPoint;    //creating the reticle for target to teleport to.    [Tooltip("The target marker for the teleporting script.")]    public GameObject teleportReticlePrefab;    private GameObject reticle;    private Transform raycastReticleTransform;    public Vector3 raycastReticleOffset;    public LayerMask rayCastMask;



    //CanvasUIPrefab
    //public GameObject canvasUIPrefab;    private GameObject canvasUI;    private Text objectNameText;    private Text foodSource1Text;
    private Text foodSource2Text;
    private Text foodSource3Text;


    // Use this for initialization
    void Start () {        laser = Instantiate(laserPrefab);        laserTransform = laser.transform;        reticle = Instantiate(teleportReticlePrefab);        raycastReticleTransform = reticle.transform;        canvasUI = GameObject.Find("FoodSourceUICanvas").gameObject;        foreach(Text text in canvasUI.GetComponentsInChildren<Text>()){            Debug.Log("Working fine");            Debug.Log(text);            if(text.name == "ObjectNameText"){                objectNameText = text;            }else if(text.name == "foodsource1text"){                Debug.Log("Found text 1");                foodSource1Text = text;                foodSource1Text.text = "";            }
            else if (text.name == "foodsource2text")            {                Debug.Log("Found text 2");                foodSource2Text = text;                foodSource2Text.text = "";            }            else if (text.name == "foodsource3text")            {                Debug.Log("Found text 3");                foodSource3Text = text;                foodSource3Text.text = "";            }        }               //Debug.Log(canvasUI.GetComponentInChildren<Text>().name);      	}		// Update is called once per frame	void FixedUpdate () {        if (!raycastButton.GetPress())        {            laser.SetActive(false);            reticle.SetActive(false);            canvasUI.SetActive(false);        }        if(raycastButton.GetPress()){            RaycastHit hit;            if (Physics.Raycast(tracker.transform.position, tracker.transform.forward, out hit, rayCastMask))            {                hitPoint = hit.point;                GameObject hitObject = hit.transform.gameObject;                if(hitObject.CompareTag("animal")){                    Debug.Log(hitObject.name);                    string selectedObjectName = hitObject.name.ToLower();
                    Vector3 selectedObjectPosition = hitObject.transform.position;
                    string animalName = getObjectName(selectedObjectName);
                    Debug.Log(selectedObjectPosition);
                   // canvasUI.transform.localEulerAngles = new Vector3(tracker.transform.rotation.x, tracker.transform.rotation.y, tracker.transform.rotation.z);                    //canvasUI.transform.position = new Vector3(tracker.transform.position.x, tracker.transform.position.y + 1f, tracker.transform.position.z-2f);                    if(animalName != ""){
                       string[] infoAboutAnimal = getFoodSources(animalName);                        setUIDetails(animalName, infoAboutAnimal);                        //myText.color = Color.Lerp(myText.color, Color.white, 6f * Time.deltaTime);
                    }                }
                else{
                    //myText.color = Color.Lerp(myText.color, Color.clear, 5f * Time.deltaTime);
                    canvasUI.SetActive(false);                }
                showLaser(hit);                reticle.SetActive(true);                raycastReticleTransform.position = hitPoint + raycastReticleOffset;            }        }           }

    private string[] getFoodSources(string name){        if(name == "pig"){            return new string[] { "Pork", "Bacon", "Ham" };        }else if(name == "cow"){            return new string[] { "Milk", "Cheese" , "Beef" };        }else if(name == "chicken"){            return new string[] { "Egg", "Meat" , "Wings" };        }else if(name == "wheat"){            return new string[] { "Bread", "Flour", "Cereals" };        }else if( name == "corn"){            return new string[] { "tortillas", "tacos", "Sweetener" };        }else{            return new string[0];        }    }

    private void showLaser(RaycastHit hit){        laser.SetActive(true);        laser.transform.position = Vector3.Lerp(tracker.transform.position, hitPoint, .5f);        laserTransform.LookAt(hitPoint);        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);                }    private void setUIDetails(string name, string[] foodSourceList){       // myText.text = "Hit a " + name;        objectNameText.text = name;        foodSource1Text.text = foodSourceList[0];        foodSource2Text.text = foodSourceList[1];        foodSource3Text.text = foodSourceList[2];        canvasUI.SetActive(true);    }    private string getObjectName(string selectedObjectName){        string textResult = "";        if(selectedObjectName.Contains("pig")){            textResult = "pig";        }else if(selectedObjectName.Contains("chicken")){
            textResult = "chicken";        }
        else if(selectedObjectName.Contains("cow")){
            textResult = "cow";        }
        else if(selectedObjectName.Contains("wheat")){
            textResult = "wheat";        }
        else if(selectedObjectName.Contains("corn")){
            textResult = "corn";        }        return textResult;    }     }