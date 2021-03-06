﻿using UnityEngine;
using System.Collections;

public class ScreenFlash : MonoBehaviour {

	private Texture2D pixel;
	public Color color = Color.red;
     public float startAlpha=0.0f;
     public float maxAlpha=0.5f;
     public float rampUpTime=0.5f;
     public float holdTime=0.5f;
     public float rampDownTime=0.5f;
 
	public enum FLASHSTATE {OFF,UP,HOLD,DOWN}
	Timer timer = new Timer(0.5f);
     public FLASHSTATE state = FLASHSTATE.OFF;
 
     
     // Use this for initialization
     void Start(){
         pixel = new Texture2D(1,1);
         color.a = startAlpha;
         pixel.SetPixel(0,0,color);
         pixel.Apply();
     }
 
     public void Update() {
         switch(state){
             case FLASHSTATE.UP:
                 if (timer.UpdateAndTest()){
                     //state =FLASHSTATE.HOLD;
                     //timer = new Timer(holdTime);
                 }
                 break;
             case FLASHSTATE.HOLD:
                 if (timer.UpdateAndTest()){
                     state =FLASHSTATE.DOWN;
                     timer = new Timer(rampDownTime);
                 }
             break;
             case FLASHSTATE.DOWN:
                 if (timer.UpdateAndTest()){
                     state =FLASHSTATE.OFF;
                     timer = null;
                 }
             break;
         }
     }
 
     private void SetPixelAlpha(float a){
         color.a = a;
         pixel.SetPixel(0,0,color);
         pixel.Apply();
     }
 
     public void OnGUI(){
         switch(state){
             case FLASHSTATE.UP:
                 SetPixelAlpha(Mathf.Lerp(startAlpha,maxAlpha,timer.Elapsed));
                 break;
             case FLASHSTATE.DOWN:
                 SetPixelAlpha(Mathf.Lerp(maxAlpha,startAlpha,timer.Elapsed));
                 break;
         }
         GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), pixel);
     }
 
}
