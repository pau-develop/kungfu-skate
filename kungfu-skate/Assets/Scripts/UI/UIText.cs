using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : MonoBehaviour
{
    [SerializeField] private Sprite[] dotSprites;
    private float blinkTimer = 0;
    private float dotBlinkTimer = 0;
    private Color32 originalColor = new Color32(0, 108, 250, 255);
    private Color32 flashingColor = new Color32(255, 255, 255, 255);
    private bool isFlashing = false;
    public void textBlinkEffect(TextMeshProUGUI text){
        blinkTimer += Time.unscaledDeltaTime;
        if(blinkTimer >= 0.1f){
            isFlashing = !isFlashing;
            blinkTimer = 0;
        }
        if(!isFlashing) text.color = originalColor;
        else text.color = flashingColor;
    }

    public void stopTextBlinkEffect(TextMeshProUGUI text){
        text.color = originalColor;
    }

    public void dotBlinkEffect(RectTransform[] menuDots){
        dotBlinkTimer += Time.unscaledDeltaTime;
        if(blinkTimer >= 0.1f){
            isFlashing = !isFlashing;
            dotBlinkTimer = 0;
        }
        if(!isFlashing) {
            menuDots[0].GetComponent<Image>().sprite = dotSprites[0];
            menuDots[1].GetComponent<Image>().sprite = dotSprites[0];
        } else {
            menuDots[0].GetComponent<Image>().sprite = dotSprites[1];
            menuDots[1].GetComponent<Image>().sprite = dotSprites[1];
        }
    }

    public void stopDotBlinkEffect(RectTransform[] menuDots){
        menuDots[0].GetComponent<Image>().sprite = dotSprites[0];
        menuDots[1].GetComponent<Image>().sprite = dotSprites[0];
    }
}
