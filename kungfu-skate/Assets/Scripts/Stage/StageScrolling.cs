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
    public float backgroundScrollSpeed;
    public float currentBackgroundScrollSpeed;
    private Vector2[] scrollingPiecesPos = new Vector2[2];
    private int shiftPos = -500;
    public int[] initialBackgroundPieces;
    public int spritesShifted = 0;
    private Vector2 outOfScreenPos = new Vector2(-640, 180);
    private int currentList = 0;
    private int nextPiece = 0;
    public int[] eventSprites;
    public string[] eventType;
    private int currentEventIndex = 0;
    private int originalScrollSpeed;
    private int fastScrollSpeed;
    private BackgroundEvents events;
    public int decreaseSpeedFactor;
    // Start is called before the first frame update
    void Start()
    {
        currentBackgroundScrollSpeed = backgroundScrollSpeed;
        events = transform.parent.GetComponent<BackgroundEvents>();
        originalScrollSpeed = (int)backgroundScrollSpeed;
        fastScrollSpeed = (int)backgroundScrollSpeed * 2;
        getDictionaryInfo();
        loadAllSprites(); 
        instantiateAll();
        createScrollingPieces();
    }

    void getDictionaryInfo(){
        stageDictionary = transform.parent.GetComponent<StagePieces>().dictionaryList[transform.GetSiblingIndex()];
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
    }

    public void changeBackgroundSpeed(bool accelerate){
        if(!accelerate) {
            if(currentBackgroundScrollSpeed > 0) currentBackgroundScrollSpeed -= decreaseSpeedFactor * Time.deltaTime;
            else currentBackgroundScrollSpeed = 0;
        } else {
            if(currentBackgroundScrollSpeed < backgroundScrollSpeed) currentBackgroundScrollSpeed += decreaseSpeedFactor * Time.deltaTime;
            else currentBackgroundScrollSpeed = backgroundScrollSpeed;
        }
    }

    private void moveScrollingPieces(){
        for(int i = 0; i < spritesOnDisplay.Length; i++){
            scrollingPiecesPos[i].x -= currentBackgroundScrollSpeed * Time.deltaTime;
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
        if(eventSprites.Length == 0) {
            if(nextPiece + 1 < sprites.Length) nextPiece += 1;
            else nextPiece = 0;
        } else{
            if(eventSprites[currentEventIndex] == spritesShifted){
                int[] numbers;
                stageDictionary.TryGetValue(eventType[currentEventIndex], out numbers);
                if(numbers.Length == 1) nextPiece = numbers[0];
                else getRandomPiece(numbers);
                if(currentEventIndex + 1 < eventSprites.Length) currentEventIndex++; 
            }
        }
    }
    void getRandomPiece(int[] pieces){
        int randomPiece = Random.Range(0,pieces.Length);
        nextPiece = pieces[randomPiece];
    }
}
