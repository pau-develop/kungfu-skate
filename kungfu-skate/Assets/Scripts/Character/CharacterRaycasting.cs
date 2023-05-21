using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRaycasting : MonoBehaviour
{
    [SerializeField] private int leftRayXPos;
    [SerializeField] private int rightRayXPos;
    private int[] allRays;
    private int[] allBotPositions;
    private int spaceBetweenRays = 4;
    private int rayLength = 20;
    private int screenBottom = -90;
    public bool leftCrash = false;
    // Start is called before the first frame update
    void Start()
    {
        createRayPositions(leftRayXPos, rightRayXPos);
    }

    private void createRayPositions(int leftRay, int rightRay){
        int distanceBetweenRayPositions = Mathf.Max(leftRay, rightRay) - Mathf.Min(leftRay, rightRay);
        int raysToBeAdded = (int)Mathf.Round(distanceBetweenRayPositions / spaceBetweenRays);
        allRays = new int[raysToBeAdded + 1];
        allBotPositions = new int[allRays.Length];
        int newDistance = 0;
        for(int i = 0; i < allRays.Length; i++){
            if(i == allRays.Length -1) allRays[i] = rightRayXPos;
            else allRays[i] = leftRayXPos + newDistance;
            newDistance += spaceBetweenRays;
        }
    }

    // Update is called once per frame
    void Update()
    {
        castRays();
    }


    private void castRays(){
        for(int i = 0; i < allBotPositions.Length; i++){
            allBotPositions[i] = (int)doTheRayCasting(allRays[i]);
        } 
        changeCharacterBottomPosition(allBotPositions);
    }

    private void changeCharacterBottomPosition(int[] allBotPositions){
        int currentHighestValue = allBotPositions[0];
        int currentHighestValueIndex = 0;
        for(int i = 0; i < allBotPositions.Length -1; i++){
            if(allBotPositions[i + 1] > allBotPositions[i]) {
                currentHighestValue = allBotPositions[i + 1];
                currentHighestValueIndex = i + 1;
            }
        }
        if(currentHighestValue == allBotPositions[0]) leftCrash = true;
        if(currentHighestValue == allBotPositions[allBotPositions.Length -1]) leftCrash = false;
        transform.GetComponent<CharacterMovement>().botLimit = currentHighestValue;
        checkTagOnHighestCollider(allRays[currentHighestValueIndex]);
    }

    private void checkTagOnHighestCollider(int rayOrigin){
        Vector2 rayPos = new Vector2(transform.position.x + rayOrigin, transform.position.y + 5);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        if(hit.collider != null && hit.collider.tag == "Grindable") GetComponent<CharacterMovement>().onGrindableObject = true;
        else GetComponent<CharacterMovement>().onGrindableObject = false;
    }

    private float doTheRayCasting(int rayOrigin){
        Vector2 rayPos = new Vector2(transform.position.x + rayOrigin, transform.position.y + 5);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        Debug.DrawRay(rayPos, Vector2.down * rayLength, Color.green);
        if(hit.collider != null){
            if(hit.collider.gameObject.tag == "Obstacle"
            || hit.collider.gameObject.tag == "Grindable")
                return getColliderPosition(hit.collider);
            else return -160;
        } 
        else return -160;
    }

    private float getColliderPosition(Collider2D collider){
        BoxCollider2D boxCollider = collider.GetComponent<BoxCollider2D>();
        return screenBottom + boxCollider.size.y;
    }

    public int castRayInvertedRamp(){
        Vector2 rayPos = new Vector2(transform.position.x + allRays[allRays.Length-1], transform.position.y + 5);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        Debug.DrawRay(rayPos, Vector2.down * rayLength, Color.blue);
        if(hit.collider.gameObject.tag == "RampDownwards") return checkCharPosComparedToRamp(hit);
        else return 5;
    }

    private int checkCharPosComparedToRamp(RaycastHit2D hit){
        int targetDistance = 1;
        Debug.Log("HIT POINT " + hit.point.y + ", PLAYERYPOS" + transform.position.y);
        int actualDistance = (int)Mathf.Abs(hit.point.y - transform.position.y); 
        return actualDistance - targetDistance;

    }
}
