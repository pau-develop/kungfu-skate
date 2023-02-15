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
    
    private int randomNumber;
    private bool isPlayer;

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
        isPlayer = GetComponent<CharacterData>().isPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isAlive){
            checkGrounded();
            checkDirection();
        } else { 
            if(isPlayer) {
                bodyAnimator.SetBool("isAlive",false);
            }
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
        bool fliped = false;
        if(GetComponent<FlipSprite>()){
            if(GetComponent<FlipSprite>().isFliped) fliped = true;
            else fliped = false;
        } 
        if(fliped) flipedAnimations();
        else commonAnimations();
    }

    void flipedAnimations(){
        if(player.movingLeft) {
            legsAnimator.SetBool("movingBack",false);
            legsAnimator.SetBool("movingForward",true);
            bodyAnimator.SetBool("movingForward",true);
            return;
            }
            if(player.movingRight) { 
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

    void commonAnimations(){
        if(player.movingLeft) {
            legsAnimator.SetBool("movingForward",false);
            legsAnimator.SetBool("movingBack",true);
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
            legsAnimator.SetBool("movingForward",true);
            legsAnimator.SetBool("movingBack",false);
            bodyAnimator.SetBool("movingForward",true);
        }
    }
}
