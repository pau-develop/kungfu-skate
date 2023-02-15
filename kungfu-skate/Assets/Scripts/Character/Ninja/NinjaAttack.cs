using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAttack : MonoBehaviour
{
    private int attackCooldown = 2;
    private float cooldownTimer = 0;
    private float attackDelay = 1;
    private float delayTimer = 0;
    private int ammunition = 5;
    private CharacterMovement ninja;
    private Animator ninjaArmAnimator; 
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        ninjaArmAnimator = transform.Find("arms").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ninja.isAlive) dealWithAttack();
    }

    void dealWithAttack(){
        cooldownTimer += 1 * Time.deltaTime;
        if(cooldownTimer > attackCooldown) prepareShot();
    }

    void prepareShot(){ 
        ninjaArmAnimator.SetBool("isRaising",true);
        delayTimer += 1 * Time.deltaTime;
        if(delayTimer > attackDelay) ninjaShoot(); 
    }

    void ninjaShoot(){
        ninjaArmAnimator.SetBool("isRaising",false);
        cooldownTimer = 0;
        delayTimer = 0;
    }
}
