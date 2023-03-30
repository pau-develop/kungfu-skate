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
    private Vector2[] scrollingPiecesPos = new Vector2[3];
    // Start is called before the first frame update
    void Start()
    {
        createScrollingPieces();
    }

    private void createScrollingPieces(){
        for(int i = 0; i < scrollingPieces.Length; i++){
            scrollingPieces[i] = new GameObject(i.ToString());
            scrollingPieces[i].transform.parent = this.transform;
            scrollingPieces[i].AddComponent<SpriteRenderer>().sprite = sprites[i];
            scrollingPieces[i].transform.position = new Vector2(initialPosition + (spriteWidth * i), 0);
            scrollingPiecesPos[i] = scrollingPieces[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
       moveScrollingPieces();
    }

    private void moveScrollingPieces(){
        for(int i = 0; i < scrollingPieces.Length; i++){
            scrollingPiecesPos[i].x -= backgroundScrollSpeed * Time.deltaTime;
            scrollingPieces[i].transform.position = scrollingPiecesPos[i];
        }
    }
}
