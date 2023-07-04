using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContinueScreen : MonoBehaviour
{
    private AudioController audioFx;
    [SerializeField] private Sprite[] numbersShadows;
    [SerializeField] private Sprite[] numbersMasks;
    [SerializeField] private AudioClip[] numbersSounds;
    private Vector2 numberPosition = new Vector2(70, -18);
    private GameObject currentNumberObject;
    private Vector2 numberScale = new Vector2(0, 1);
    private bool scalingUp = true;
    private int currentNumber = 9;
    // Start is called before the first frame update
    void Start(){
        audioFx = GameObject.Find("audio").GetComponent<AudioController>();
    }

    void OnEnable(){
        currentNumber = 9;
        scalingUp = true;
        numberScale = new Vector2(0, 1);
        generateNumber(currentNumber);
    }

    void onDisable(){
        Destroy(currentNumberObject);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(currentNumberObject != null) doTheCountDown();
        if(Input.GetKeyUp(KeyCode.Space)) dealWithContinue();
    }

    private void dealWithContinue(){
        Destroy(currentNumberObject);
        GlobalData.inContinueScreen = false;
    }

    private void generateNumber(int numberIndex){
        currentNumberObject = new GameObject();
        currentNumberObject.transform.parent = this.gameObject.transform; 
        currentNumberObject.gameObject.name = numberIndex.ToString();
        currentNumberObject.transform.position = numberPosition;
        SpriteRenderer currentRenderer = currentNumberObject.AddComponent<SpriteRenderer>();
        currentRenderer.sprite = numbersShadows[numberIndex - 1];
        currentRenderer.sortingLayerName = "LeftLayer";
        currentRenderer.sortingOrder = 1000;
        SpriteMask currentMask = currentNumberObject.AddComponent<SpriteMask>();
        currentMask.sprite = numbersMasks[numberIndex - 1];
        audioFx.playSound(numbersSounds[numberIndex - 1]);
    }

    private void doTheCountDown(){
        if(scalingUp){
            if(numberScale.x < 1) numberScale.x += 1 * Time.unscaledDeltaTime;
            else scalingUp = false;
        } else{
            if(numberScale.x > 0) numberScale.x -= 1 * Time.unscaledDeltaTime;
            else switchNumber();
        }

        currentNumberObject.transform.localScale = numberScale;    
    }

    private void switchNumber(){
        scalingUp = true;
        Destroy(currentNumberObject);
        if(currentNumber > 1){
            currentNumber--;
            generateNumber(currentNumber);
        }
    }
}
