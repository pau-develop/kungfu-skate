using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    private float blinkTimer = 0;
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
}
