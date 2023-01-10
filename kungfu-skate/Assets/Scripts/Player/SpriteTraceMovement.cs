using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTraceMovement : MonoBehaviour
{
    private Vector2 tracePos;
    private int traceSpeed = 50;
    public SpriteRenderer[] sprites = new SpriteRenderer[3];

    private int fadeSpeed =8;

    byte alpha = 255;
    // Start is called before the first frame update
    void Start()
    {
        tracePos = transform.position;
        for(int i=0; i<sprites.Length;i++){
            sprites[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveTrace();
        fadeAway();
        destroyTrace();
    }

    void destroyTrace(){
        if(transform.position.x <= -180) Destroy(this.gameObject);
    }
    void fadeAway(){
        for(int i=0; i<sprites.Length;i++){
            Color32 currentColor =  sprites[i].color;
            int alpha = currentColor.a;
            alpha-=fadeSpeed;
            if(alpha<=0) Destroy(this.gameObject);
            currentColor.a = (byte)alpha;
            sprites[i].color = currentColor;
            
        }
    }

    void moveTrace(){
        tracePos.x -= traceSpeed * Time.deltaTime;
        transform.position = tracePos;
    }

    void faceTrace(){

    }
}
