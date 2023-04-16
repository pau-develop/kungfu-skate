using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSprite : MonoBehaviour
{
    private BackgroundEvents events;
    private Texture2D originalTexture;
    
    private Color32[] colorArrayOriginal;
    private Color32[] colorArrayTransition;
    private Color32[] colorArrayAfterTransition;
    public Color32[] originalColorsToChange;
    public Color32[] transitionColorsToChange;
    public Color32[] afterTransitionColorsToChange;
    public Sprite[] newSpritesOriginal; //as many sprites as colorCycles in ColorList
    public Sprite[] newSpritesTransition; //as many sprites as colorCycles in ColorList
    public Sprite[] newSpritesAfterTransition; //as many sprites as colorCycles in ColorList
    private List<List<int>> originalColorIndexes = new List<List<int>>();
    private List<List<int>> transitionColorIndexes = new List<List<int>>();
    private List<List<int>> afterTransitionColorIndexes = new List<List<int>>();
    private ColorList newColorsOriginal;
    private ColorList newColorsTransition;
    private ColorList newColorsAfterTransition;
    private float spriteTimer = 0;
    public float changeSpriteDelay;
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;
    private bool movingUp = true;
    private Vector2 spriteSize;
    private bool hasTransitioned = false;
    private int transitionCycles;
    private bool transitionFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        events = GameObject.Find("Stage").GetComponent<BackgroundEvents>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteSize.x = spriteRenderer.sprite.rect.width;
        spriteSize.y = spriteRenderer.sprite.rect.height;
        originalTexture = spriteRenderer.sprite.texture;
        newColorsOriginal = GetComponent<ColorLister>().orginalColorCycles;
        newColorsTransition = GetComponent<ColorLister>().transitionColorCycles;
        newColorsAfterTransition = GetComponent<ColorLister>().afterTransitionColorCycles;
        newSpritesOriginal = new Sprite[newColorsOriginal.colorCycles.Count + 1];
        newSpritesTransition = new Sprite[newColorsTransition.colorCycles.Count + 1];
        newSpritesAfterTransition = new Sprite[newColorsAfterTransition.colorCycles.Count + 1];
        colorArrayOriginal = originalTexture.GetPixels32();
        colorArrayTransition = originalTexture.GetPixels32();
        originalColorsToChange = GetComponent<ColorLister>().originalColors;
        transitionColorsToChange = GetComponent<ColorLister>().transitionColors;
        afterTransitionColorsToChange = GetComponent<ColorLister>().afterTransitionColors;
        storeColorIndexes(originalColorsToChange, originalColorIndexes, colorArrayOriginal);
        createTextureWithNewColors(newSpritesOriginal, originalColorIndexes, newColorsOriginal, colorArrayOriginal);
        storeColorIndexes(transitionColorsToChange, transitionColorIndexes, colorArrayTransition);
        createTextureWithNewColors(newSpritesTransition, transitionColorIndexes, newColorsTransition, colorArrayTransition);
        colorArrayAfterTransition = newSpritesTransition[newSpritesTransition.Length - 1].texture.GetPixels32();
        storeColorIndexes(afterTransitionColorsToChange, afterTransitionColorIndexes, colorArrayAfterTransition);
        createTextureWithNewColors(newSpritesAfterTransition, afterTransitionColorIndexes, newColorsAfterTransition, colorArrayAfterTransition,true);
        transitionCycles = newSpritesTransition.Length;
    }

    private void storeColorIndexes(Color32[] colorsToChange, List<List<int>> colorIndexes, Color32[] colorArray){
        for(int i = 0; i < colorArray.Length; i++){
            for(int x = 0; x < colorsToChange.Length; x++){
                colorIndexes.Add(new List<int>());
                if(
                    colorArray[i].r == colorsToChange[x].r
                    &&colorArray[i].g == colorsToChange[x].g
                    &&colorArray[i].b == colorsToChange[x].b
                )   {
                    colorIndexes[x].Add(i);
                }
            }
        }
    }

    private void createTextureWithNewColors(Sprite[] newSprites, List<List<int>> colorIndexes, ColorList newColors, Color32[] colorArray, bool afterTransition = false){
        //make copy of original sprite
        if(!afterTransition) newSprites[0] = Sprite.Create(originalTexture, new Rect(0,0,spriteSize.x,spriteSize.y),new Vector2(0.5f,0.5f),1);
        else newSprites[0] = Sprite.Create(newSpritesTransition[newSpritesTransition.Length - 1].texture, new Rect(0,0,spriteSize.x,spriteSize.y),new Vector2(0.5f,0.5f),1);
        //add rest of sprites
        for(int z = 0; z < newSprites.Length-1; z++){
            for(int i = 0; i < colorIndexes.Count; i++){
                for(int y = 0; y < colorIndexes[i].Count; y++){
                    colorArray[colorIndexes[i][y]].r = newColors.colorCycles[z].newColor[i].r;
                    colorArray[colorIndexes[i][y]].g = newColors.colorCycles[z].newColor[i].g;
                    colorArray[colorIndexes[i][y]].b = newColors.colorCycles[z].newColor[i].b;
                }
            }
            Texture2D tempTexture;
            tempTexture =  new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, true);
            tempTexture.SetPixels32(colorArray);
            tempTexture.filterMode = FilterMode.Point;
            tempTexture.Apply();
            newSprites[z+1] = Sprite.Create(tempTexture, new Rect(0,0,spriteSize.x,spriteSize.y),new Vector2(0.5f,0.5f),1);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if(events.shouldTransition){
            if(!transitionFlag){
                currentSpriteIndex = 0;
                movingUp = true;
                transitionFlag = true;
            } else {
                if(!hasTransitioned) {
                    if(newColorsTransition.colorCycles.Count > 0) changeSprite(newSpritesTransition, true);
            } else {
                if(newColorsAfterTransition.colorCycles.Count > 0) changeSprite(newSpritesAfterTransition);

            }
            }
            
        } else {
            if(newColorsOriginal.colorCycles.Count > 0) changeSprite(newSpritesOriginal);
        }
		
    }

    void changeSprite(Sprite[] newSprites, bool inTransition = false){
        if(!inTransition) changeSpriteDelay = 0.1f;
        else changeSpriteDelay = 1;
        spriteTimer += Time.deltaTime;
        if(spriteTimer >= changeSpriteDelay){
            spriteRenderer.sprite = newSprites[currentSpriteIndex];
            if(movingUp){
                if(currentSpriteIndex < newSprites.Length -1) currentSpriteIndex++;
                else {
                    currentSpriteIndex--;
                    movingUp = false;
                }
            } else {
                if(currentSpriteIndex > 0) currentSpriteIndex--;
                else {
                    currentSpriteIndex++;
                    movingUp = true;
                }
            }
            spriteTimer = 0;
            if(inTransition && transitionCycles > 0) transitionCycles--;
            if(inTransition && transitionCycles == 0) {
                currentSpriteIndex = 0;
                movingUp = true;
                hasTransitioned = true;
            }
        }
    }
}
