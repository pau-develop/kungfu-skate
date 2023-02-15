using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBounds : MonoBehaviour
{
    public bool insideBounds = false;
    private int leftLimit = -180;
    private int rightLimit = 180;
    private int topLimit = 100;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        insideBounds = getPosition();
        destroyOutOfBoundsEnemy();
    }

    bool getPosition(){
        if(transform.position.x < leftLimit) return false;
        if(transform.position.x > rightLimit) return false;
        if(transform.position.y > topLimit) return false;
        return true;
    }

    void destroyOutOfBoundsEnemy(){
        bool reachedInitialDestPos = GetComponent<NinjaCommands>().reachedInitialDestPos;
        if(!insideBounds && reachedInitialDestPos) Destroy(this.gameObject);
    }
}
