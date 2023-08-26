using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIContinueScreen : MonoBehaviour
{   
    [SerializeField] private GameObject[] continueCharacter;
    private Vector2 characterLocation = new Vector2(-55, 10);
    private GameObject currentCharacter;
    public AudioClip continueMusic;
    private AudioClip stageMusic;
    private AudioController audioController;
    private AudioSource audioMusic;
    [SerializeField] private Sprite[] numbersShadows;
    [SerializeField] private Sprite[] numbersMasks;
    [SerializeField] private AudioClip[] numbersSounds;
    private Vector2 numberPosition = new Vector2(64, -13);
    private GameObject currentNumberObject;
    private Vector2 numberScale = new Vector2(0, 1);
    private bool scalingUp = true;
    public int currentNumber = 10;
    private Vector2 continueScreenScale;
    private bool scalingUpMenu = true;
    private bool scalingDownMenu = false;
    private int scaleSpeed = 1;
    private bool isOpen = false;
    private bool noNumber = true;
    private GameObject continueBox;
    private GameObject continueShadow;
    private GameObject gameOverShadow;
    private GameObject letsGoShadow;
    private GameObject noCreditsShadow;
    private bool isGameOver = false;
    private int numberSpeed = 1;
    // Start is called before the first frame update
    void Start(){
        gameOverShadow = transform.Find("game-over-shadow").gameObject;
        gameOverShadow.SetActive(false);
        continueBox = transform.Find("continue-box").gameObject;
        continueShadow = continueBox.transform.Find("continue-shadow").gameObject;
        letsGoShadow = continueBox.transform.Find("lets-go-shadow").gameObject;
        letsGoShadow.SetActive(false);
        noCreditsShadow = continueBox.transform.Find("no-credits-shadow").gameObject;
        noCreditsShadow.SetActive(false);
        audioController = GameObject.Find("audio").GetComponent<AudioController>();
        stageMusic = GameObject.Find("Stage").GetComponent<BackgroundEvents>().stageMusic;
        continueBox.transform.localScale = new Vector2(0, 0);
        continueScreenScale = new Vector2(0, 0);
    }

    void OnEnable(){
        if(continueShadow != null) continueShadow.SetActive(true);
        if(letsGoShadow != null) letsGoShadow.SetActive(false);
        if(gameOverShadow != null) gameOverShadow.SetActive(false);
        if(noCreditsShadow != null) noCreditsShadow.SetActive(false);
        if(audioController != null) audioController.playMusic(continueMusic);
        numberScale = new Vector2(0, 1);
        isOpen = false;
        noNumber = true;
        scalingDownMenu = false;
        scalingUpMenu = true;
        currentNumber = 10;
        numberSpeed = 1;
    }
    

    // Update is called once per frame
    void Update()
    {
        dealWithMenuScale();
        if(isOpen){
            if(noNumber) {
                generateNumber(currentNumber);
                generateCharacter();
            }
            if(currentNumberObject != null) doTheCountDown();
            dealWithInput();
        }
    }

    private void dealWithInput(){
        if(Input.GetKeyUp(KeyCode.Space) && !isGameOver){
            if(GlobalData.playerOneCredits > 0) StartCoroutine(delayBeforeClosingRoutine());
            else displayNoCreditsMessage();
        }
        if(Input.GetKeyUp(KeyCode.M) && !isGameOver) numberSpeed = 8;
    }

    private void displayNoCreditsMessage(){
        continueShadow.SetActive(false);
        noCreditsShadow.SetActive(true);
        numberSpeed = 8;
    }

    private IEnumerator delayBeforeClosingRoutine(){
        GlobalData.playerOneCredits--;
        continueShadow.SetActive(false);
        letsGoShadow.SetActive(true);
        Destroy(currentNumberObject);
        currentCharacter.GetComponent<PlayerContinueAnimations>().pressedContinue = true;
        yield return new WaitForSecondsRealtime(1);
        scalingDownMenu = true;
        StopCoroutine(delayBeforeClosingRoutine());
    }

    private IEnumerator delayBeforeGameOverRoutine(){
        isGameOver = true;
        continueShadow.SetActive(false);
        letsGoShadow.SetActive(false);
        noCreditsShadow.SetActive(false);
        currentCharacter.GetComponent<PlayerContinueAnimations>().hasDied = true;
        yield return new WaitForSecondsRealtime(1);
        gameOverShadow.SetActive(true);
        scalingDownMenu = true;
        StopCoroutine(delayBeforeClosingRoutine());
    }

    private void generateCharacter(){
        currentCharacter = Instantiate(continueCharacter[0], characterLocation, Quaternion.identity);
        currentCharacter.transform.parent = continueBox.transform;
    }

    private void dealWithMenuScale(){
        if(scalingUpMenu) {
            if(continueScreenScale.x < 1){
                continueScreenScale.x += scaleSpeed * Time.unscaledDeltaTime;
                continueScreenScale.y += scaleSpeed * Time.unscaledDeltaTime;
            } else {
                continueScreenScale.x = 1;
                continueScreenScale.y = 1;
                isOpen = true;
                scalingUpMenu = false;
            }
        } else if(scalingDownMenu){
            if(continueScreenScale.x > 0){
                continueScreenScale.x -= scaleSpeed * Time.unscaledDeltaTime;
                continueScreenScale.y -= scaleSpeed * Time.unscaledDeltaTime;
            } else {
                continueScreenScale.x = 0;
                continueScreenScale.y = 0;
                if(!isGameOver) dealWithContinue();
                else StartCoroutine(dealWithGameOverRoutine());
            }
        }
        continueBox.transform.localScale = continueScreenScale;
    }

    private void dealWithContinue(){
        Destroy(currentCharacter);
        audioController.playMusic(stageMusic);
        GlobalData.inContinueScreen = false;
    }

    private IEnumerator dealWithGameOverRoutine(){
        Destroy(currentCharacter);
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(0);
    }

    private void generateNumber(int numberIndex){
        currentNumberObject = new GameObject();
        currentNumberObject.transform.parent = continueBox.transform; 
        currentNumberObject.gameObject.name = numberIndex.ToString();
        currentNumberObject.transform.position = numberPosition;
        SpriteRenderer currentRenderer = currentNumberObject.AddComponent<SpriteRenderer>();
        currentRenderer.sprite = numbersShadows[numberIndex - 1];
        currentRenderer.sortingLayerName = "LeftLayer";
        currentRenderer.sortingOrder = 1000;
        SpriteMask currentMask = currentNumberObject.AddComponent<SpriteMask>();
        currentMask.sprite = numbersMasks[numberIndex - 1];
        audioController.playSound(numbersSounds[numberIndex - 1]);
        noNumber = false;
    }

    private void doTheCountDown(){
        if(scalingUp){
            if(numberScale.x < 1) numberScale.x += numberSpeed * Time.unscaledDeltaTime;
            else scalingUp = false;
        } else{
            if(numberScale.x > 0) numberScale.x -= numberSpeed * Time.unscaledDeltaTime;
            else switchNumber();
        }
        currentNumberObject.transform.localScale = numberScale;    
    }

    private void switchNumber(){
        if(!isGameOver) numberSpeed = 1;
        scalingUp = true;
        Destroy(currentNumberObject);
        if(currentNumber > 1){
            currentNumber--;
            generateNumber(currentNumber);
        } else {
            StartCoroutine(delayBeforeGameOverRoutine());
        }
    }
}
