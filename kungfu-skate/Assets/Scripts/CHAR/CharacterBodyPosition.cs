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
    public int backXGround;
    public int backXAir;
    public int forwardXGround;
    public int forwardXAir;
    public int backYAir;
    public int backYGround;
    public int forwardYAir;
    public int forwardYGround;
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
        if(legsAnimator.GetBool("isGrounded")){
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x+backXGround,transform.position.y+backYGround);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+forwardXGround,transform.position.y+forwardYGround); 
            else bodyDestPos = new Vector2(transform.position.x,transform.position.y+1);
        }
        else{
            if(legsAnimator.GetBool("movingBack")) bodyDestPos = new Vector2(transform.position.x+backXAir,transform.position.y+backYAir);
            else if(legsAnimator.GetBool("movingForward")) bodyDestPos = new Vector2(transform.position.x+forwardXAir,transform.position.y+forwardYAir);
            else bodyDestPos = new Vector2(transform.position.x,transform.position.y);
        }
    }
}
