using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerPos : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        checkForPlayerOnScene();
        if(player != null) trackPlayerPos();
    }

    void checkForPlayerOnScene(){
        if(GameObject.FindWithTag("Player")!=null) player =  GameObject.FindWithTag("Player");
        else player = null;
    }

    void trackPlayerPos(){
        if(transform.position.x > player.transform.position.x) GetComponent<FlipSprite>().isFliped = true;
        else GetComponent<FlipSprite>().isFliped = false;
    }
}
