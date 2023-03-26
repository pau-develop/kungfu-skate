using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private int direction = 1;
    private Vector2 wavePos; 
    private int waveSpeed = 150;
    private int leftLimit = -175;
    private int rightLimit = 175;
    // Start is called before the first frame update
    void Start()
    {
        wavePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveWave();
        destroyWave();
    }

    void moveWave(){
        wavePos.x += (waveSpeed * direction) * Time.deltaTime; 
        transform.position = wavePos;
    }

    void destroyWave(){
        if(direction == 1 && wavePos.x > rightLimit) Destroy(this.gameObject);
        if(direction == -1 && wavePos.x < leftLimit) Destroy(this.gameObject);
    }
}
