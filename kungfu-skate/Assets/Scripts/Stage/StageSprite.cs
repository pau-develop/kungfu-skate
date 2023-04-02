using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSprite : MonoBehaviour
{
    private Texture2D originalTexture;
    private Sprite[] newSprites; //as many sprites as colorCycles in ColorList
    private Color32[] colorArray;
    public Color32[] colorsToChange;
    private List<List<int>> colorIndexes = new List<List<int>>();
    private ColorList newColors;
    private float spriteTimer = 0;
    public float changeSpriteDelay;
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;
    private bool movingUp = true;
    private Vector2 spriteSize;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteSize.x = spriteRenderer.sprite.rect.width;
        spriteSize.y = spriteRenderer.sprite.rect.height;
        newColors = GetComponent<ColorLister>().colorList;
        originalTexture = spriteRenderer.sprite.texture;
        newSprites = new Sprite[newColors.colorCycles.Count + 1];
        colorArray = originalTexture.GetPixels32();
        storeColorIndexes();
        createTextureWithNewColors();
    }

    private void storeColorIndexes(){
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

    private void createTextureWithNewColors(){
        //make copy of original sprite
        newSprites[0] = Sprite.Create(originalTexture, new Rect(0,0,spriteSize.x,spriteSize.y),new Vector2(0.5f,0.5f),1);
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
		if(newColors.colorCycles.Count > 0) changeSprite();
    }

    void changeSprite(){
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
        }
    }
}
