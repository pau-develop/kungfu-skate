using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SwapSprites : MonoBehaviour
{
    public string SpriteSheetName;
    private string LoadedSpriteSheetName;
    private Dictionary<string, Sprite> spriteSheet;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSpriteSheet();
    }

    private void LateUpdate()
    {
        if (this.LoadedSpriteSheetName != this.SpriteSheetName)
        {
            this.LoadSpriteSheet();
        }
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
    }

    
    private void LoadSpriteSheet()
    {
        var sprites = Resources.LoadAll<Sprite>("CHAR/"+this.SpriteSheetName);
        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);
        this.LoadedSpriteSheetName = this.SpriteSheetName;
    }
}
