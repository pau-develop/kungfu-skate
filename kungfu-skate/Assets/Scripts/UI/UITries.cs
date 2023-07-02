using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITries : MonoBehaviour
{
    private TextMeshProUGUI triesText;
    private int latestTries;
    private Color32 originalTriesFontColor = new Color32(0, 108, 250, 255);
    private Color32 altTriesFontColor = new Color32(255, 255, 255, 255);
    [SerializeField] private Sprite[] playerIcons;
    [SerializeField] private TMP_FontAsset font;
    private SpriteRenderer iconRenderer;
    // Start is called before the first frame update
    void Start()
    {
        latestTries = GlobalData.playerOneTries;
        triesText = transform.Find("tries").GetComponent<TextMeshProUGUI>();
        iconRenderer = transform.Find("player-icon").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displayPlayerIcon();
        updateTries();
    }

    private void updateTries(){
        triesText.text = "x " + GlobalData.playerOneTries.ToString();
    }
    
    private void displayPlayerIcon(){
        int currentPlayer = GlobalData.playerOneCharacter;
        iconRenderer.sprite = playerIcons[currentPlayer];   
    }
}
