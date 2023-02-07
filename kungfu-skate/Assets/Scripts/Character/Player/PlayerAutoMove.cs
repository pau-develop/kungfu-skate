using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    private Vector2 destPos;
    private Vector2 playerPos;
    // Start is called before the first frame update
    void Start()
    {

        destPos = new Vector2(transform.position.x+60, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<CharacterData>().autoMove) autoMovePlayer();
        else Destroy(GetComponent<PlayerAutoMove>());   
    }

    void autoMovePlayer(){
        if(transform.position.x < destPos.x) playerPos.x += 100 * Time.deltaTime;
        else GetComponent<CharacterData>().autoMove =  false;
    }
}
