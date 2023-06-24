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
    private Vector2 bloodPos;
    private bool reachedInitialDestPos = false;
    private float count = 0;
    public bool isFliped = false;
    private bool hasFallen = false;
    private int botPosition;
    private float backgroundSpeed;
    private int xLimit = -170;
    private float rayTimer = 0;
    private float rayTimerLimit = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        bloodRenderer = GetComponent<SpriteRenderer>();
		fallSpeed = Random.Range(10,40) * 10;
		dropSize = Random.Range(1.0f,2.0f);
        transform.localScale = new Vector3(dropSize, dropSize, 0);
        setColor(Random.Range(0.5f,1f));
        initialOriginPos = transform.position;
        initialDestPos = getInitialDestPos();
        height = initialOriginPos +(initialDestPos - initialOriginPos)/2 + Vector2.up *Random.Range(1.0f,40.0f);
        botPosition = getBottomPosition();
        
    }

    private int getBottomPosition(){
        //PENDING
        int rayLength = 180;
        Vector2 rayPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        if(hit.collider != null)
            if(hit.collider.tag == "Obstacle"||hit.collider.tag == "Grindable"){
                float screenBottom = -90;
                BoxCollider2D boxCollider = hit.collider.GetComponent<BoxCollider2D>();
                int originalBottomPosition = (int)(screenBottom + boxCollider.size.y + 3);
                int threshold = 10;
                bloodRenderer.sortingOrder = hit.transform.Find("shadow-mask").GetComponent<SpriteMask>().frontSortingOrder;
                return Random.Range(originalBottomPosition + threshold, originalBottomPosition - threshold);
            } else if(hit.collider.tag == "RampUpwards"){
                float screenBottom = -90;
                PolygonCollider2D polyCollider = hit.collider.GetComponent<PolygonCollider2D>();
                //get lowest and highest point
                int[] points = getLowestAndHighestPoint(polyCollider);
                int midPoint = Mathf.Abs(points[0] - points[1]);
                int originalBottomPosition = (int)(screenBottom + midPoint + 3);
                int threshold = 10;
                bloodRenderer.sortingOrder = hit.transform.Find("shadow-mask").GetComponent<SpriteMask>().frontSortingOrder;
                return Random.Range(originalBottomPosition + threshold, originalBottomPosition - threshold);
            }
            else return -180;
        else return -180;
    }

    private int[] getLowestAndHighestPoint(PolygonCollider2D polyCollider){
        float lowestPoint = 0;
        float highestPoint = 0;
        for(int i = 0; i < polyCollider.points.Length; i++){
            if(polyCollider.points[i].y < lowestPoint) lowestPoint = polyCollider.points[i].y;
            if(polyCollider.points[i].y > highestPoint) highestPoint = polyCollider.points[i].y;
        }
        return new int[]{(int)lowestPoint, (int)highestPoint};
    }

    private void setColor(float value){
        Color32 bloodColor = new Color(value,0,0);
		bloodRenderer.color = bloodColor;
    }

    private Vector2 getInitialDestPos(){
        int direction;
        if(isFliped) direction = -1;
        else direction = 1;
        return new Vector2(transform.position.x - Random.Range(5.0f * direction, 30.0f * direction),transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(!reachedInitialDestPos) particleInitialMovement();
        if(reachedInitialDestPos && !hasFallen) {
            particleFallMovement();
            castRays();
        }
        if(hasFallen) moveAlongGround();
        destroyParticle();
    }

    private void castRays(){
        rayTimer += Time.deltaTime;
        if(rayTimer > rayTimerLimit){
            botPosition = getBottomPosition();
            rayTimer = 0;
        }
    }

    private void destroyParticle(){
        if(transform.position.x < xLimit) Destroy(this.gameObject);
    }

    private void moveAlongGround(){
        backgroundSpeed = GameObject.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed;
        bloodPos.x -= backgroundSpeed * Time.deltaTime;
        transform.position = bloodPos;
    }

    private void particleFallMovement(){
		if(transform.position.y > botPosition){
			bloodPos = transform.position;
			bloodPos.y -= fallSpeed * Time.deltaTime;
			bloodPos.x -= fallSpeed/10* Time.deltaTime;
			transform.position = bloodPos;
		} else {
            bloodRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            bloodRenderer.sortingLayerName = "RightLayer";
            Destroy(GetComponent<SpriteLayer>());
            hasFallen = true;
            setColor(0.75f);
            bloodPos = new Vector2((int)bloodPos.x, (int)bloodPos.y);
        }
    }

    private void particleInitialMovement(){
		if (count < 0.75f) {
			count += 1.0f *Time.deltaTime*4f;
			Vector2 m1 = Vector2.Lerp( initialOriginPos, height, count );
			Vector2 m2 = Vector2.Lerp( height, initialDestPos, count );
			transform.position = Vector2.Lerp(m1, m2, count);
		} else reachedInitialDestPos = true;
    }
}
