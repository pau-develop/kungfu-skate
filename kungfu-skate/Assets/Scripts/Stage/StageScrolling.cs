using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScrolling : MonoBehaviour
{
    public int spriteLayer;
    public GameObject[] sprites;
    private int initialPosition = 160;
    private int spriteWidth = 640;
    public int backgroundScrollSpeed = 100;
    private GameObject[] scrollingPieces = new GameObject[3];
    private Vector2[] scrollingPiecesPos = new Vector2[3];
    private int shiftPos = -500;
    private float scrollX = 0;
    private UI ui;
    public int[] initialBackgroundPieces;
    public int spritesShifted = 0;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UI>();
        loadAllSprites(); 
        createScrollingPieces();
    }

    private void loadAllSprites(){
        Object[] allSprites = Resources.LoadAll("Sprites/STAGE1OBJ",typeof(GameObject));
        int spriteCounter = 0;
        sprites = new GameObject[allSprites.Length];
        for(int i = 0; i < allSprites.Length; i++) if(allSprites[i].name[3] == this.gameObject.name[5]) spriteCounter++;
        sprites = new GameObject[spriteCounter];
        int currentSprite = 0;
        for(int i = 0; i < allSprites.Length; i++){
            if(allSprites[i].name[3] == this.gameObject.name[5]){
                sprites[currentSprite] = allSprites[i] as GameObject;
                currentSprite++;
            } 
        }  
    }

    private void createScrollingPieces(){
        for(int i = 0; i < scrollingPieces.Length; i++){
            scrollingPieces[i] = Instantiate(sprites[initialBackgroundPieces[i]], transform.position, Quaternion.identity);
            scrollingPieces[i].transform.parent = this.transform;
            scrollingPieces[i].transform.position = new Vector2(initialPosition + (spriteWidth * i), 0);
            scrollingPiecesPos[i] = scrollingPieces[i].transform.position;
            scrollingPieces[i].GetComponent<SpriteRenderer>().sortingOrder = spriteLayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
       moveScrollingPieces();
       if(scrollingPiecesPos[0].x <= shiftPos) shiftPieces();
       if(this.gameObject.name == "Layer1") countXScroll();
    }

    private void countXScroll(){
       scrollX += backgroundScrollSpeed * Time.deltaTime;
       ui.scrollX = ((int)scrollX/40);
    }

    private void moveScrollingPieces(){
        for(int i = 0; i < scrollingPieces.Length; i++){
            scrollingPiecesPos[i].x -= backgroundScrollSpeed * Time.deltaTime;
            scrollingPieces[i].transform.position = scrollingPiecesPos[i];
        }
    }

    private void shiftPieces(){
        //move front piece to the back
        scrollingPiecesPos[0].x = scrollingPiecesPos[2].x + spriteWidth;
        //hold onto that index
        GameObject tempPiece = scrollingPieces[0];
        Vector2 tempPos = scrollingPiecesPos[0];
        //shiftIndexes
        for(int i = 0; i < scrollingPieces.Length; i++){
            if(i < scrollingPieces.Length -1){
                scrollingPieces[i] = scrollingPieces[i+1];
                scrollingPiecesPos[i] = scrollingPiecesPos[i+1];
            } else {
                scrollingPieces[i] = tempPiece;
                scrollingPiecesPos[i] = tempPos;
            }        
        }
        spritesShifted++;
    }
}
