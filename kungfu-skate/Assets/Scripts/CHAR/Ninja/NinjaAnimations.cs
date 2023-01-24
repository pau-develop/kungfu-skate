using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAnimations : MonoBehaviour
{
    private CharacterMovement ninja;

    private GameObject ninjaBody;
    private GameObject ninjaArms;
    private Animator bodyAnimator;
    private Animator legsAnimator;
    private Animator armsAnimator;

    private Vector2 bodyPosition;
    private Vector2 armsPosition;

    private Vector2 bodyDestPos;

    private bool justJumped = false;
    private bool justGrounded = false;
    private bool landing = false;
    private bool jumping = false;
    private int currentFrame = 0;
    private int currentPos = 0;
    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        ninjaBody = transform.Find("body").gameObject;
        ninjaArms = transform.Find("arms").gameObject;
        bodyAnimator = ninjaBody.GetComponent<Animator>();
        armsAnimator = ninjaArms.GetComponent<Animator>();
        legsAnimator = transform.Find("legs").GetComponent<Animator>();
        armsPosition = ninjaArms.transform.position;
        bodyPosition = ninjaBody.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ninja.isAlive){
            checkGrounded();
            checkDirection();      
            
            
        } else {
            if(randomNumber == 0){
                randomNumber = Random.Range(1,3);
                if(randomNumber==1) bodyAnimator.Play("body-die-1");
                else bodyAnimator.Play("body-die-2");
            }
            if(ninja.isGrounded) bodyAnimator.SetBool("isGrounded",true);
            if(ninja.isExploded) bodyAnimator.Play("body-explode");
        }
    }

    void checkGrounded(){
        if(ninja.isGrounded) {
            legsAnimator.SetBool("isGrounded",true);
            if(!justGrounded){
                legsAnimator.Play("legs-land");
                justGrounded = true;
                justJumped = false;
            }
        }
        else {
            legsAnimator.SetBool("isGrounded",false);
            if(!justJumped){
                legsAnimator.Play("legs-jump");
                justJumped = true;
                justGrounded = false;
            }
        }
    }

    void checkDirection(){
        if(ninja.movingLeft) {
            legsAnimator.SetBool("movingBack",false);
            legsAnimator.SetBool("movingForward",true);
            bodyAnimator.SetBool("movingForward",true);
            return;
        }
        if(ninja.movingRight) { 
            legsAnimator.SetBool("movingForward",false);
            legsAnimator.SetBool("movingBack",true);
            bodyAnimator.SetBool("movingForward",false);
            return;
        }
        else {
            legsAnimator.SetBool("movingForward",false);
            legsAnimator.SetBool("movingBack",true);
            bodyAnimator.SetBool("movingForward",false);
            
        }
    }

   
    
}
