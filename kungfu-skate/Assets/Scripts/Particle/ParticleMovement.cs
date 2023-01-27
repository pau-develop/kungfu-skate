using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    public Vector2[] directions;
    public Vector2 direction;
    private Vector2 originPos;
    private Vector2 destPos;
    private Vector2 moveDir;
    private Vector2 currentPos;
    public int flipDirection;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        originPos = transform.position;
        destPos = new Vector2(transform.position.x+direction.x*flipDirection, transform.position.y+direction.y);
        moveDir = (originPos-destPos).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        moveParticle();
        resizeParticle();
    }

    void moveParticle(){
		currentPos += moveDir * 100 *  Time.deltaTime;
		transform.Rotate(Vector3.forward * 1500 * Time.deltaTime);
		transform.position = currentPos;
	}

    private void resizeParticle(){
		if(transform.localScale.x > 0)
			transform.localScale += new Vector3(-5*Time.deltaTime,-5*Time.deltaTime,1);
		else Destroy(this.gameObject);
	}
}
