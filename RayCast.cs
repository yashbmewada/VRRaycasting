﻿/*











using System;
using System.Collections;



    //CanvasUIPrefab
    //public GameObject canvasUIPrefab;
    private Text foodSource2Text;
    private Text foodSource3Text;


    // Use this for initialization
    void Start () {
            else if (text.name == "foodsource2text")
                    Vector3 selectedObjectPosition = hitObject.transform.position;
                    string animalName = getObjectName(selectedObjectName);
                    Debug.Log(selectedObjectPosition);
                   // canvasUI.transform.localEulerAngles = new Vector3(tracker.transform.rotation.x, tracker.transform.rotation.y, tracker.transform.rotation.z);
                       string[] infoAboutAnimal = getFoodSources(animalName);
                    }
                else{
                    //myText.color = Color.Lerp(myText.color, Color.clear, 5f * Time.deltaTime);
                    canvasUI.SetActive(false);
                showLaser(hit);

    private string[] getFoodSources(string name){

    private void showLaser(RaycastHit hit){
            textResult = "chicken";
        else if(selectedObjectName.Contains("cow")){
            textResult = "cow";
        else if(selectedObjectName.Contains("wheat")){
            textResult = "wheat";
        else if(selectedObjectName.Contains("corn")){
            textResult = "corn";