using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSprite : MonoBehaviour
{
    private Texture2D originalTexture;
    public Sprite[] newSprites; //as many sprites as colorCycles in ColorList
    private Color32[] colorArray;
    public Color32[] colorsToChange;
    private List<List<int>> colorIndexes = new List<List<int>>();
    private ColorList newColors;
    // Start is called before the first frame update
    void Start()
    {
        newColors = GetComponent<ColorLister>().colorList;
        originalTexture = GetComponent<SpriteRenderer>().sprite.texture;
        newSprites = new Sprite[newColors.colorCycles.Count];
        colorArray = originalTexture.GetPixels32();
        storeColorIndexes();
        createTextureWithNewColors();
        // foreach(Color32 color in newColors.colorCycles[0].newColor) Debug.Log(color);
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
        for(int z = 0; z < newSprites.Length; z++){
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
            newSprites[z] = Sprite.Create(tempTexture, new Rect(0,0,640,180),new Vector2(0.5f,0.5f),1);
        }   
    }

    // Update is called once per frame
    void Update()
    {
			//change color on each sprite
    }
}
