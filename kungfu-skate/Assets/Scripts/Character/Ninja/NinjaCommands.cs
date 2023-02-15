using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 50;
    public Vector2 initialDestPos = new Vector2(0,0);
    private Vector2 initialMoveDir;
    private bool reachedInitialDestPos = false;
    // Start is called before the first frame update
    void Start()
    {
        ninjaPos = transform.position;
        initialMoveDir = (initialDestPos - ninjaPos).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(!reachedInitialDestPos) moveToInitialDestPos();    
    }

    void moveToInitialDestPos(){
        float step = initialMoveSpeed * Time.deltaTime;
        ninjaPos = Vector2.MoveTowards(ninjaPos, initialDestPos, step);
        Debug.Log(ninjaPos);
    }
}
