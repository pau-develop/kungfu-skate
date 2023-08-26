using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContinueAnimations : MonoBehaviour
{
    private string[] headAnimationsNames = new string[]{
        "continue-head-cry",
        "continue-head-hurt",
        "continue-head-ok",
        "continue-head-die"
    };
    private Animator headAnimator;
    private float animationSpeed = 1;
    public bool pressedContinue = false;
    public bool hasDied = false;
    private UIContinueScreen continueScreen; 
    // Start is called before the first frame update
    void Start()
    {
        headAnimator = this.transform.GetChild(0).GetComponent<Animator>();
        continueScreen = transform.parent.transform.parent.GetComponent<UIContinueScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        changeAnimationSpeed();
        if(!pressedContinue && !hasDied){
            if(headAnimator.GetCurrentAnimatorStateInfo(0).IsName("null")) 
                playRandomAnimation();
        } else if(pressedContinue){ 
            if(!headAnimator.GetCurrentAnimatorStateInfo(0).IsName("continue-head-ok"))
                headAnimator.Play(headAnimationsNames[2]);
        } else if(hasDied){
            if(!headAnimator.GetCurrentAnimatorStateInfo(0).IsName("continue-head-die"))
                headAnimator.Play(headAnimationsNames[3]);
        }
    }

    private void changeAnimationSpeed(){
        int number = continueScreen.currentNumber;
        if(number >= 8) animationSpeed = 1.0f;
        else if(number >= 6) animationSpeed = 1.5f;
        else if(number >= 4) animationSpeed = 2f;
        else if(number >= 2) animationSpeed = 2.5f;
        else if(hasDied) animationSpeed = 1f;
        headAnimator.speed = animationSpeed;
    }

    private void playRandomAnimation(){
        int randomNumber = Random.Range(0, 2);
        headAnimator.Play(headAnimationsNames[randomNumber]);
    }
}
