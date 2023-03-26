using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private int direction = 1;
    private Vector2 wavePos; 
    private int waveSpeed = 150;
    // Start is called before the first frame update
    void Start()
    {
        wavePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveWave();
    }

    void moveWave(){
        wavePos.x += (waveSpeed * direction) * Time.deltaTime; 
        transform.position = wavePos;
    }
}
