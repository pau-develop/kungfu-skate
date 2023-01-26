using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDestroy : MonoBehaviour
{
    private CharacterMovement player;
  

    void destroyPlayer(){
        if(transform.parent == null) Destroy(this.gameObject);
        else Destroy(this.transform.parent.gameObject);
    }
}
