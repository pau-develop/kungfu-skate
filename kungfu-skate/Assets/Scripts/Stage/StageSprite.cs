using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSprite : MonoBehaviour
{
    private Texture2D originalTexture;
    private Color32[] colorArray;
    public Color32[] colorsToChange;
    private List<List<int>> colorIndexes = new List<List<int>>();
    // Start is called before the first frame update
    void Start()
    {
        originalTexture = GetComponent<SpriteRenderer>().sprite.texture;
		colorArray = originalTexture.GetPixels32();
        storeColorIndexes();
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

    // Update is called once per frame
    void Update()
    {
			//change color on each sprite
    }
}
