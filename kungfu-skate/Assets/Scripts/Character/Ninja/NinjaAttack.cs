﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAttack : MonoBehaviour
{
    public float attackCooldown = 2;
    private float cooldownTimer = 0;
    private float attackDelay = 0.5f;
    private float delayTimer = 0;
    public int ammunition = 5;
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
        GameObject player = GameObject.FindWithTag("Player");
        if(ninja.isAlive && player!=null) dealWithAttack();
        if(player == null && ninja.isAlive) cancelAttack();
    }

    void cancelAttack(){
        ninjaArmAnimator.SetBool("isRaising",false);
        ninjaArmAnimator.Play("arms-idle");
    }

    void dealWithAttack(){
        cooldownTimer += 1 * Time.deltaTime;
        if(cooldownTimer > attackCooldown && ammunition > 0) prepareShot();
    }

    void prepareShot(){ 
        ninjaArmAnimator.SetBool("isRaising",true);
        delayTimer += 1 * Time.deltaTime;
        if(delayTimer > attackDelay) ninjaShoot(); 
    }

    void ninjaShoot(){
        ninjaArmAnimator.SetBool("isRaising",false);
        ammunition--;
        cooldownTimer = 0;
        delayTimer = 0;
    }
}
