using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyPosition : MonoBehaviour
{
    private CharacterMovement player;
    private GameObject playerArms;
    private GameObject playerBody;
    private Vector2 bodyDestPos;
    private Animator legsAnimator;
    public float suspensionYLand;
    public float suspensionYJump;
    public int[] groundPositions; //xForward, yForward, xBack, yBack
    public int[] airPositions; //xForward, yForward, xBack, yBack
 
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterMovement>();
        legsAnimator = transform.Find("legs").GetComponent<Animator>();
        playerArms = transform.Find("arms").gameObject;
        playerBody = transform.Find("body").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isAlive) moveBodyParts();
    }

    void moveBodyParts(){
        setBodyPosHorizontal();
        int currentState = checkAnimationState();
        if(currentState == 0) suspensionEffect(suspensionYLand);
        else if(currentState == 1) suspensionEffect(suspensionYJump);
        else setBodyPosVertical();
        playerBody.transform.position = bodyDestPos;
        playerArms.transform.position = bodyDestPos;
    }


    void suspensionEffect(float suspensionY){
        bodyDestPos.y = transform.position.y+suspensionY;
    }

    int checkAnimationState(){
        AnimatorClipInfo[] currentClip = legsAnimator.GetCurrentAnimatorClipInfo(0);
        string currentAnim = "";
        if(currentClip.Length > 0) currentAnim = currentClip[0].clip.name;
        if(currentAnim == "legs-land") return 0;
        else if(currentAnim == "legs-jump") return 1;
        else return 2;
    }

    void setBodyPosHorizontal(){
        int direction = getDirection();
        if(legsAnimator.GetBool("isGrounded")){
            if(legsAnimator.GetBool("movingBack")) bodyDestPos.x = transform.position.x+groundPositions[2]*direction;
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos.x = transform.position.x+groundPositions[0]*direction; 
            else bodyDestPos.x = transform.position.x*direction;
        }
        else{
            if(legsAnimator.GetBool("movingBack")) bodyDestPos.x = transform.position.x+airPositions[2]*direction;
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos.x = transform.position.x+airPositions[0]*direction;
            else bodyDestPos.x = transform.position.x;
        }
    }

    void setBodyPosVertical(){
        int direction = getDirection();
        if(legsAnimator.GetBool("isGrounded")){
            if(legsAnimator.GetBool("movingBack")) bodyDestPos.y = transform.position.y+groundPositions[3];
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos.y = transform.position.y+groundPositions[1]; 
            else bodyDestPos.y = transform.position.y+1;
        }
        else{
            if(legsAnimator.GetBool("movingBack")) bodyDestPos.y = transform.position.y+airPositions[3];
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos.y = transform.position.y+airPositions[1];
            else bodyDestPos.y = transform.position.y;
        }
    }

    int getDirection(){
        if(GetComponent<FlipSprite>()){
            if(GetComponent<FlipSprite>().isFliped) return -1;
            else return 1;
        }
        return 1;
    }

    
}
