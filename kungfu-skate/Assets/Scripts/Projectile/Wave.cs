using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private int direction = 1;
    private Vector2 wavePos; 
    private int leftLimit = -175;
    private int rightLimit = 175;
    private int waveHits = 4;
    private int[] waveSpeed = {50, 75, 100, 125, 150};
    private float[] waveScale = {0.2f, 0.4f, 0.6f, 0.8f, 1.0f};
    private SwapSprites swapSprites;
    private Vector2 currentScale;
    // Start is called before the first frame update
    void Start()
    {
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
            currentScale.x-= 0.5f * Time.deltaTime;
            currentScale.y-= 0.5f * Time.deltaTime;
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
        wavePos.x += (waveSpeed[waveHits] * direction) * Time.deltaTime; 
        transform.position = wavePos;
    }

    void destroyWave(){
        if(direction == 1 && wavePos.x > rightLimit) Destroy(this.gameObject);
        if(direction == -1 && wavePos.x < leftLimit) Destroy(this.gameObject);
    }

    void testWave(){
        if(Input.GetKeyUp(KeyCode.Q) && waveHits > 0) waveHits--;
    }
}
