using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyPosition : MonoBehaviour
{
    private CharacterMovement player;
    private GameObject playerArms;
    private int currentFrame = 0;
    private GameObject playerBody;
    private Vector2 bodyDestPos;
    private Animator legsAnimator;
    public int[] suspensionX;
    public int[] suspensionY;
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
        if(currentFrame <=1) bodyDestPos = new Vector2(transform.position.x+suspensionX[0],transform.position.y+suspensionY[1] * direction);
        else if(currentFrame > 2) bodyDestPos = new Vector2(transform.position.x+suspensionX[1],transform.position.y+suspensionY[2] * direction);
        else if(currentFrame > 3) bodyDestPos = new Vector2(transform.position.x+suspensionX[2],transform.position.y+suspensionY[3] * direction);
        else if(currentFrame > 4) bodyDestPos = new Vector2(transform.position.x+suspensionX[3],transform.position.y+suspensionY[2] * direction);
        else if(currentFrame > 5) bodyDestPos = new Vector2(transform.position.x+suspensionX[4],transform.position.y+suspensionY[1] * direction);
    }

    int checkAnimationState(){
        AnimatorClipInfo[] currentClip = legsAnimator.GetCurrentAnimatorClipInfo(0);
        string currentAnim = "";
        if(currentClip.Length > 0) currentAnim = currentClip[0].clip.name;
        if(currentAnim == "legs-land") return 0;
        else if(currentAnim == "legs-jump") return 1;
        else return 2;
    }
    

    void setBodyPos(){
        int direction = getDirection();
        if(legsAnimator.GetBool("isGrounded")){
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x+groundPositions[2]*direction,transform.position.y+groundPositions[3]);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+groundPositions[0]*direction,transform.position.y+groundPositions[1]); 
            else bodyDestPos = new Vector2(transform.position.x*direction,transform.position.y+1);
        }
        else{
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x+airPositions[2]*direction,transform.position.y+airPositions[3]);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+airPositions[0]*direction,transform.position.y+airPositions[1]);
            else bodyDestPos = new Vector2(transform.position.x,transform.position.y);
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
