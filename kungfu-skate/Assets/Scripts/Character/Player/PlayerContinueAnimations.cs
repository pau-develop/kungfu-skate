using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContinueAnimations : MonoBehaviour
{
    private string[] headAnimationsNames = new string[]{
        "continue-head-cry",
        "continue-head-hurt",
    };
    private Animator headAnimator;
    private float animationSpeed = 1;
    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        headAnimator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(headAnimator.GetCurrentAnimatorStateInfo(0).IsName("null")) playRandomAnimation();
    }

    private void playRandomAnimation(){
        int randomNumber = Random.Range(0, headAnimationsNames.Length);
        headAnimator.Play(headAnimationsNames[randomNumber]);
    }
}
