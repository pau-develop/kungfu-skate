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
            moveBodyParts();
            
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

    void moveBodyParts(){
        int currentState = checkAnimationState();
        if(currentState == 0) suspensionEffect(-1);
        else if(currentState == 1) suspensionEffect(1);
        else {
            currentFrame = 0;
            if(GetComponent<FlipSprite>().isFliped) setBodyPos(-1);
            else setBodyPos(1);
        }
        ninjaBody.transform.position = bodyDestPos;
        ninjaArms.transform.position = bodyDestPos;
    }

     void suspensionEffect(int direction){
        currentFrame+=1;
        if(currentFrame <=1) bodyDestPos = new Vector2(transform.position.x-1,transform.position.y * direction);
        else if(currentFrame > 2) bodyDestPos = new Vector2(transform.position.x-1,transform.position.y+1 * direction);
        else if(currentFrame > 3) bodyDestPos = new Vector2(transform.position.x-1,transform.position.y+2 * direction);
        else if(currentFrame > 4) bodyDestPos = new Vector2(transform.position.x-1,transform.position.y+2 * direction);
        else if(currentFrame > 5) bodyDestPos = new Vector2(transform.position.x-1,transform.position.y+1 * direction);
    }
    
    int checkAnimationState(){
        AnimatorClipInfo[] currentClip = legsAnimator.GetCurrentAnimatorClipInfo(0);
        string currentAnim = "";
        if(currentClip.Length>0) currentAnim = currentClip[0].clip.name;
        if(currentAnim == "legs-land") return 0;
        else if(currentAnim == "legs-jump") return 1;
        else return 2;
    }

    void setBodyPos(int direction){
        if(legsAnimator.GetBool("isGrounded")){
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x-1,transform.position.y+2);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+1*direction,transform.position.y+2); 
            else bodyDestPos = new Vector2(transform.position.x,transform.position.y+1);
        }
        else{
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x-2*direction,transform.position.y+1);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+1*direction,transform.position.y);
            else bodyDestPos = new Vector2(transform.position.x,transform.position.y);
        }
    }
    
}
