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
    // Start is called before the first frame update
    void Start()
    {
        headAnimator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void playRandomAnimation(){
        int randomNumber = Random.Range(0, 2);
        headAnimator.Play(headAnimationsNames[randomNumber]);
    }
}
