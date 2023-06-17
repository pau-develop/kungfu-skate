using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleMovement : MonoBehaviour
{
    private Color32[] bloodColors = new Color32[1];
    private SpriteRenderer bloodRenderer;
    private int fallSpeed;
    private float dropSize;
    private Vector2 initialOriginPos;
    private Vector2 initialDestPos;
    private Vector2 height;
    private bool reachedInitialDestPos = false;
    private float count = 0;
    public bool isFliped = false;
    // Start is called before the first frame update
    void Start()
    {
        bloodRenderer = GetComponent<SpriteRenderer>();
		fallSpeed = Random.Range(10,40) * 10;
		dropSize = Random.Range(1.0f,2.0f);
        setColor();
        initialOriginPos = transform.position;
        initialDestPos = getInitialDestPos();
        height = initialOriginPos +(initialDestPos - initialOriginPos)/2 + Vector2.up *Random.Range(1.0f,40.0f);
    }

    private void setColor(){
        Color32 bloodColor = new Color(Random.Range(0.5f,1f),0,0);
		bloodRenderer.color = bloodColor;
    }

    private Vector2 getInitialDestPos(){
        int direction;
        if(isFliped) direction = -1;
        else direction = 1;
        return new Vector2(transform.position.x - Random.Range(5.0f * direction, 20.0f * direction),transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(!reachedInitialDestPos) moveParticle();
    }

    private void moveParticle(){
		if (count < 0.75f) {
			count += 1.0f *Time.deltaTime*4f;
			Vector2 m1 = Vector2.Lerp( initialOriginPos, height, count );
			Vector2 m2 = Vector2.Lerp( height, initialDestPos, count );
			transform.position = Vector2.Lerp(m1, m2, count);
		} else reachedInitialDestPos = true;
    }
}
