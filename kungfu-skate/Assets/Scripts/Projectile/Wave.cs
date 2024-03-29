using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public bool isPlayerWave;
    private int direction = 1;
    private Vector2 wavePos; 
    private int leftLimit = -175;
    private int rightLimit = 175;
    public int waveHits = 9;
    private int[] waveSpeed = {50, 50, 75, 75, 100, 100, 125, 125, 150, 150};
    private float[] waveScale = {0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f};
    private SwapSprites swapSprites;
    private Vector2 currentScale;
    private bool stoppedWave = false;
    private PolygonCollider2D waveCollider;
    // Start is called before the first frame update
    void Start()
    {
        waveCollider = GetComponent<PolygonCollider2D>();
        wavePos = transform.position;
        currentScale = transform.localScale;
        swapSprites = GetComponent<SwapSprites>();
    }

    // Update is called once per frame
    void Update()
    {
        moveWave();
        destroyWave();
        blinkWave();
        changeWaveScale();
        testWave();
    }

    void changeWaveScale(){
        if(currentScale.x > waveScale[waveHits]){
            currentScale.x-= 1f * Time.deltaTime;
            currentScale.y-= 1f * Time.deltaTime;
        } else {
            currentScale.x = waveScale[waveHits];
            currentScale.y = waveScale[waveHits];
        }
        transform.localScale = currentScale;
    }

    void blinkWave(){
        if(!swapSprites.isBlinking) swapSprites.isBlinking = true; 
    }

    void moveWave(){
        if(!stoppedWave) wavePos.x += (waveSpeed[waveHits] * direction) * Time.deltaTime; 
        transform.position = wavePos;
    }

    void destroyWave(){
        if(direction == 1 && wavePos.x > rightLimit) Destroy(this.gameObject);
        if(direction == -1 && wavePos.x < leftLimit) Destroy(this.gameObject);
    }

    void testWave(){
        if(Input.GetKeyUp(KeyCode.Q) && waveHits > 0) waveHits--;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(!stoppedWave){
            if(collider.gameObject.tag =="Enemy" && isPlayerWave) dealWithCollision(collider, 2);
            if(collider.gameObject.tag =="EnemyBullet" && isPlayerWave) dealWithCollision(collider, 1);
        }
    }

    void dealWithCollision(Collider2D collider, int amount){
        if(waveHits == 1) waveHits--;
        if(waveHits > 1) waveHits -= amount;
        StartCoroutine(waveCollisionRoutine());
    }

    IEnumerator waveCollisionRoutine(){
        stoppedWave = true;
        waveCollider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(waveCollisionRoutine());
        stoppedWave = false;
        waveCollider.enabled = true;
    }
}
