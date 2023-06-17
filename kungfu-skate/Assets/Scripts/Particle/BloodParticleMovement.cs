using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleMovement : MonoBehaviour
{
    private Color32[] bloodColors = new Color32[1];
    private SpriteRenderer bloodRenderer;
    private int fallSpeed;
    private float dropSize;
    private Vector2 initialDestPos;
    // Start is called before the first frame update
    void Start()
    {
        bloodRenderer = GetComponent<SpriteRenderer>();
		fallSpeed = Random.Range(10,40) * 10;
		dropSize = Random.Range(1.0f,2.0f);
        setColor();
        initialDestPos = getInitialDestPos();
    }

    private void setColor(){
        Color32 bloodColor = new Color(Random.Range(0.5f,1f),0,0);
		bloodRenderer.color = bloodColor;
    }

    private Vector2 getInitialDestPos(){
        bool isFliped = GetComponent<FlipSprite>().isFliped;
        int direction;
        if(isFliped) direction = 1;
        else direction = -1;
        return new Vector2(transform.position.x-Random.Range(5.0f * direction, 10.0f * direction),transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
