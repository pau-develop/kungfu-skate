using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorSwitcher : MonoBehaviour
{
    [SerializeField] private Color32[] colorsInSprite;
    private List<int[]> colorIndexesList = new List<int[]>();
    private Texture2D spriteTexture;
    private Color32[] colorArray;
    private int totalNumberOfColors;
    private Sprite[] newSprites;
    private Vector2 spriteSize;
    // Start is called before the first frame update
    void Start()
    {
        spriteTexture = GetComponent<SpriteRenderer>().sprite.texture;
        spriteSize.x = GetComponent<SpriteRenderer>().sprite.rect.width;
        spriteSize.y = GetComponent<SpriteRenderer>().sprite.rect.height;
        colorArray = spriteTexture.GetPixels32();
        foreach(Color32 color in colorsInSprite) getColorIndexes(color);
        newSprites = new Sprite[colorsInSprite.Length];
        for(int i = 0; i < newSprites.Length; i++) generateSprites(i);
    }

    void getColorIndexes(Color32 currentColor){
        int currentIndex = 0;
        //get number of indexes for a particular color
        int numberOfIndexes = 0;
        for(int i = 0; i < colorArray.Length; i++){
            if(colorArray[i].r == currentColor.r
            && colorArray[i].g == currentColor.g
            && colorArray[i].b == currentColor.b
            && colorArray[i].a == currentColor.a){
                numberOfIndexes++;
            }
        }
        //assign colors to the indexes
        colorIndexesList.Add(new int[numberOfIndexes]);
        for(int i = 0; i < colorArray.Length; i++){
            if(colorArray[i].r == currentColor.r
            && colorArray[i].g == currentColor.g
            && colorArray[i].b == currentColor.b
            && colorArray[i].a == currentColor.a){
                colorIndexesList[colorIndexesList.Count -1][currentIndex] = i;
                currentIndex++;
            }
        }
    }

    private void generateSprites(int currentColor){
        int originalCurrentColor = currentColor;
        Color32[] tempColorArray = new Color32[colorArray.Length];
        for(int i = 0; i < colorIndexesList.Count; i++){
            for(int x = 0; x < colorIndexesList[i].Length; x++){
                tempColorArray[colorIndexesList[i][x]] = colorsInSprite[currentColor];
            }
            if(currentColor < 6) currentColor += 1;
            else currentColor = 0;
        }
        Texture2D tempTexture = new Texture2D(spriteTexture.width, spriteTexture.height, TextureFormat.RGBA32, true);
        tempTexture.SetPixels32(tempColorArray);
        tempTexture.filterMode = FilterMode.Point;
        tempTexture.Apply();
        newSprites[originalCurrentColor] = Sprite.Create(tempTexture, new Rect(0,0,spriteSize.x,spriteSize.y),new Vector2(0.5f,0.5f),1);
        GameObject tempObject = new GameObject();
        tempObject.transform.parent = this.transform;
        tempObject.AddComponent<SpriteRenderer>();
        Debug.Log(originalCurrentColor);
        tempObject.GetComponent<SpriteRenderer>().sprite = newSprites[originalCurrentColor]; 
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    
}
