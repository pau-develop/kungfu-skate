using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private CharacterMovement player;

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
    
    private int currentPos = 0;
    private int randomNumber;
    public bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterMovement>();
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
        } else { 
            if(isPlayer) bodyAnimator.SetBool("isAlive",false);
            else playRandomDeadAnim();
            if(player.isGrounded) bodyAnimator.SetBool("isGrounded",true);
            if(player.isExploded) bodyAnimator.Play("body-explode");
        }
    }

    void playRandomDeadAnim(){
        if(randomNumber == 0){
            randomNumber = Random.Range(1,3);
            if(randomNumber==1) bodyAnimator.Play("body-die-1");
            else bodyAnimator.Play("body-die-2");
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
}
