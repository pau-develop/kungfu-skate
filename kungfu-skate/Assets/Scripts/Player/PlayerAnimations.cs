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
        checkGrounded();
        checkDirection();
        checkAction();
        moveBodyParts();
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
            return;
        }
        if(player.movingRight) { 
            legsAnimator.SetBool("movingForward",true);
            legsAnimator.SetBool("movingBack",false);
            return;
        }
        else {
            legsAnimator.SetBool("movingForward",false);
            legsAnimator.SetBool("movingBack",false);
        }
    }

  

    

    

    void moveBodyParts(){
        setBodyPos(); 
        playerBody.transform.position = bodyDestPos;
        playerArms.transform.position = bodyDestPos;
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
