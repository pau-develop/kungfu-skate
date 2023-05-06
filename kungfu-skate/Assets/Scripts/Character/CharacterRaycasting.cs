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
        for(int i = 0; i < allBotPositions.Length -1; i++){
            if(allBotPositions[i + 1] > allBotPositions[i]) currentHighestValue = allBotPositions[i + 1];
        }
        transform.GetComponent<CharacterMovement>().botLimit = currentHighestValue;
    }

    private float doTheRayCasting(int rayOrigin){
        Vector2 rayPos = new Vector2(transform.position.x + rayOrigin, transform.position.y + 5);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayLength);
        Debug.DrawRay(rayPos, Vector2.down * rayLength, Color.green);
        if(hit.collider != null) return getColliderPosition(hit.collider);
        else return -160;
    }

    private float getColliderPosition(Collider2D collider){
        BoxCollider2D boxCollider = collider.GetComponent<BoxCollider2D>();
        return screenBottom + boxCollider.size.y;
    }
}
