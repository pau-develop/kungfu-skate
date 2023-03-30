using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScrolling : MonoBehaviour
{
    public Sprite[] sprites;
    private int initialPosition = 160;
    private int spriteWidth = 640;
    private int backgroundScrollSpeed = 100;
    private GameObject[] scrollingPieces = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        createScrollingPieces();
    }

    private void createScrollingPieces(){
        for(int i = 0; i < 3; i++){
            scrollingPieces[i] = new GameObject(i.ToString());
            scrollingPieces[i].transform.parent = this.transform;
            scrollingPieces[i].AddComponent<SpriteRenderer>().sprite = sprites[i];
            scrollingPieces[i].transform.position = new Vector2(initialPosition + (spriteWidth * i), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
