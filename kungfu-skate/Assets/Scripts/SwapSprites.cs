using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SwapSprites : MonoBehaviour
{
    public string folder;
    public string spriteSheetName;
    private string LoadedSpriteSheetName;
    private Dictionary<string, Sprite> spriteSheet;
    private SpriteRenderer spriteRenderer;
    public string currentColor = "_1";
    private int blinkCounter = 0;
    public bool isBlinking = false;
    private int blinkDuration = 1;
    
    private void Start()
    {
        if(transform.parent) getSpriteSheetName();  
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSpriteSheet();
    }

    void getSpriteSheetName(){
        folder = transform.parent.GetComponent<CharacterData>().folder;
        if(this.gameObject.name=="arms")
            spriteSheetName = transform.parent.GetComponent<CharacterData>().armSpriteName;
        else  spriteSheetName = transform.parent.GetComponent<CharacterData>().bodySpriteName;
    }

    private void Update(){
        if(isBlinking) blinkEffect();
    }

    private void blinkEffect(){
        if(blinkCounter<blinkDuration){
            if(currentColor=="_1" && blinkCounter%2==0) currentColor="_2";
            else currentColor="_1";
            blinkCounter+=1;
            this.LoadSpriteSheet();
        } else {
            currentColor="_1";
            blinkCounter = 0;
            isBlinking=false;
            this.LoadSpriteSheet();
        }
    }

    private void LateUpdate()
    {
        if (this.LoadedSpriteSheetName != this.spriteSheetName)
        {
            this.LoadSpriteSheet();
        }
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
    }

    
    private void LoadSpriteSheet()
    {
        var sprites = Resources.LoadAll<Sprite>(folder+this.spriteSheetName+currentColor);
        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);
        this.LoadedSpriteSheetName = this.spriteSheetName;
    }
}
