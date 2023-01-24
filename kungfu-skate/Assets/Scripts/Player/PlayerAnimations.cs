using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement player;

    private GameObject playerBody;
    private GameObject playerArms;
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
        player = GetComponent<PlayerMovement>();
        playerBody = transform.Find("body").gameObject;
        playerArms = transform.Find("arms").gameObject;
        bodyAnimator = playerBody.GetComponent<Animator>();
        armsAnimator = playerArms.GetComponent<Animator>();
        legsAnimator = transform.Find("legs").GetComponent<Animator>();
        armsPosition = playerArms.transform.position;
        bodyPosition = playerBody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.isAlive){
            checkGrounded();
            checkDirection();
            checkAction();
            moveBodyParts();
        } else {
            
            bodyAnimator.SetBool("isAlive",false);
            if(player.isGrounded) bodyAnimator.SetBool("isGrounded",true);
            if(player.isExploded) bodyAnimator.Play("body-explode");
        }
    }

    void checkAction(){
        if(player.isShooting) {
            armsAnimator.SetBool("isShooting",true);
            armsAnimator.SetBool("isSwinging",false);
            return;
        }
        
        if(player.isSwinging) {
            armsAnimator.SetBool("isSwinging",true);
            armsAnimator.SetBool("isShooting",false);
            return;
        }
        armsAnimator.SetBool("isSwinging",false);
        armsAnimator.SetBool("isShooting",false);
    }

    void checkGrounded(){
        if(player.isGrounded) {
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
        if(player.movingLeft) {
            legsAnimator.SetBool("movingBack",true);
            legsAnimator.SetBool("movingForward",false);
            bodyAnimator.SetBool("movingForward",false);
            return;
        }
        if(player.movingRight) { 
            legsAnimator.SetBool("movingForward",true);
            legsAnimator.SetBool("movingBack",false);
            bodyAnimator.SetBool("movingForward",true);
            return;
        }
        else {
            legsAnimator.SetBool("movingForward",false);
            legsAnimator.SetBool("movingBack",false);
            bodyAnimator.SetBool("movingForward",true);

        }
    }

  

    

    

    void moveBodyParts(){
        int currentState = checkAnimationState();
        if(currentState == 0) suspensionEffect(-1);
        else if(currentState == 1) suspensionEffect(1);
        else {
            currentFrame = 0;
            setBodyPos();
        }
        playerBody.transform.position = bodyDestPos;
        playerArms.transform.position = bodyDestPos;
    }


    void suspensionEffect(int direction){
        currentFrame+=1;
        if(currentFrame <=1) bodyDestPos = new Vector2(transform.position.x,transform.position.y+1 * direction);
        else if(currentFrame > 2) bodyDestPos = new Vector2(transform.position.x,transform.position.y+2 * direction);
        else if(currentFrame > 3) bodyDestPos = new Vector2(transform.position.x,transform.position.y+3 * direction);
        else if(currentFrame > 4) bodyDestPos = new Vector2(transform.position.x,transform.position.y+2 * direction);
        else if(currentFrame > 5) bodyDestPos = new Vector2(transform.position.x,transform.position.y+1 * direction);
    }

    int checkAnimationState(){
        AnimatorClipInfo[] currentClip = legsAnimator.GetCurrentAnimatorClipInfo(0);
        string currentAnim = currentClip[0].clip.name;
        if(currentAnim == "legs-land") return 0;
        else if(currentAnim == "legs-jump") return 1;
        else return 2;
    }
    

    void setBodyPos(){
        if(legsAnimator.GetBool("isGrounded")){
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x-1,transform.position.y+1);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+1,transform.position.y+1); 
            else bodyDestPos = new Vector2(transform.position.x,transform.position.y+1);
        }
        else{
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x-2,transform.position.y+1);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+1,transform.position.y);
            else bodyDestPos = new Vector2(transform.position.x,transform.position.y);
        }
    }
}
