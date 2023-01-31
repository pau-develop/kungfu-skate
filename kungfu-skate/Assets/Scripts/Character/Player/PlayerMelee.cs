using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private BoxCollider2D meleeBox; 
    private AudioFX audioFX;
    // Start is called before the first frame update
    void Start()
    {
        meleeBox = GetComponent<BoxCollider2D>();
        meleeBox.enabled = false;
        audioFX = GameObject.Find("audio-fx").GetComponent<AudioFX>();
    }

    void meleeAttack(){
        meleeBox.enabled = true;
        audioFX.playSound(transform.parent.GetComponent<CharacterData>().melee);
    }

    void disableBox(){
        meleeBox.enabled = false;
    }
}
