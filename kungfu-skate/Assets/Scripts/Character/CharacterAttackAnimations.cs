using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackAnimations : MonoBehaviour
{
    private CharacterMovement player;
    private Animator armsAnimator;
    private GameObject playerArms;
    private float swingTimer = 0;
    private float waveTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<CharacterMovement>();
        armsAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isAlive) checkAction();
    }

    void checkAction(){
        if(player.isShooting) {
            armsAnimator.SetBool("isShooting",true);
            armsAnimator.SetBool("isSwinging",false);
            return;
        }   
        if(player.isSwinging) {
            swingTimer += Time.deltaTime;
            if(swingTimer > waveTime) GetComponent<PlayerMelee>().shouldSpawnWave = true;
            armsAnimator.SetBool("isSwinging",true);
            armsAnimator.SetBool("isShooting",false);
            return;
        }
        swingTimer = 0;
        armsAnimator.SetBool("isSwinging",false);
        armsAnimator.SetBool("isShooting",false);
    }
}
