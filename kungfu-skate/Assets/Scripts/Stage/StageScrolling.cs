using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScrolling : MonoBehaviour
{
    private Dictionary<string, int[]> stageDictionary;
    public int spriteLayer;
    public GameObject[] sprites;
    public List<GameObject[]> spriteInstances;
    public GameObject[] spritesOnDisplay;
    private int initialPosition = 160;
    private int spriteWidth = 640;
    public int backgroundScrollSpeed = 100;
    private Vector2[] scrollingPiecesPos = new Vector2[2];
    private int shiftPos = -500;
    private float scrollX = 0;
    private UI ui;
    public int[] initialBackgroundPieces;
    public int spritesShifted = 0;
    private Vector2 outOfScreenPos = new Vector2(-640, 180);
    private int currentList = 0;
    private int nextPiece = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.GetSiblingIndex() <= 2) tryOutDictionary();
        ui = GameObject.Find("UI").GetComponent<UI>();
        loadAllSprites(); 
        instantiateAll();
        createScrollingPieces();
    }

    void tryOutDictionary(){
        stageDictionary = transform.parent.GetComponent<StagePieces>().dictionaries[transform.GetSiblingIndex()];
        int[] numbers;
        stageDictionary.TryGetValue("road", out numbers);
        Debug.Log(numbers.Length);
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

    private void instantiateAll(){
        spriteInstances = new List<GameObject[]>();
        for(int i = 0; i < scrollingPiecesPos.Length; i++) spriteInstances.Add(new GameObject[sprites.Length]);
        for(int i = 0; i < spriteInstances.Count; i++){
            for(int x = 0; x < spriteInstances[i].Length; x++){
                spriteInstances[i][x] = Instantiate(sprites[x], transform.position, Quaternion.identity);
                spriteInstances[i][x].transform.parent = this.transform;
                spriteInstances[i][x].transform.position = outOfScreenPos;
                spriteInstances[i][x].GetComponent<SpriteRenderer>().sortingOrder = spriteLayer;
            }
        }
    }

    private void createScrollingPieces(){ 
        spritesOnDisplay = new GameObject[scrollingPiecesPos.Length];
        for(int i = 0; i < spritesOnDisplay.Length; i++){
            spritesOnDisplay[i] = spriteInstances[i][initialBackgroundPieces[i]];
            spritesOnDisplay[i].transform.position = new Vector2(initialPosition + (spriteWidth * i), 0);
            spritesOnDisplay[i].SetActive(true);
            scrollingPiecesPos[i] = spriteInstances[i][initialBackgroundPieces[i]].transform.position;
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
        for(int i = 0; i < spritesOnDisplay.Length; i++){
            scrollingPiecesPos[i].x -= backgroundScrollSpeed * Time.deltaTime;
            spritesOnDisplay[i].transform.position = scrollingPiecesPos[i];
        }
    }

    private void shiftPieces(){
        //move front piece to the back
        scrollingPiecesPos[0].x = scrollingPiecesPos[scrollingPiecesPos.Length-1].x + spriteWidth;
        //remove piece
        spritesOnDisplay[0].transform.position = outOfScreenPos;
        //change sprite in new piece
        getNextPiece();
        spritesOnDisplay[0] = spriteInstances[currentList][nextPiece];
        if(currentList < 1) currentList++;
        else currentList = 0;
        Vector2 tempPos = scrollingPiecesPos[0];
        GameObject tempSpriteOnDisplay = spritesOnDisplay[0];
        //shiftIndexes
        for(int i = 0; i < scrollingPiecesPos.Length; i++){
            if(i < scrollingPiecesPos.Length -1){
                spritesOnDisplay[i] = spritesOnDisplay[i+1];
                scrollingPiecesPos[i] = scrollingPiecesPos[i+1];
            } else {
                spritesOnDisplay[i] = tempSpriteOnDisplay;
                scrollingPiecesPos[i] = tempPos;
            }        
        }
        spritesShifted++;
    }

    void getNextPiece(){

    }
}
