using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    [SerializeField] private GameObject shadowPrefab;
    private GameObject shadow;
    private Vector2 shadowScale;
    public bool shouldDestroyShadow = false;
    // Start is called before the first frame update
    void Start()
    {
        shadow = Instantiate(shadowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        shadowScale = shadow.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        updateShadowPosition();
        if(shouldDestroyShadow) destroyShadow();
    }

    private void updateShadowPosition(){
        int botPosition = GetComponent<CharacterMovement>().botLimit;
        shadow.transform.position = new Vector2(transform.position.x, botPosition + 3);
    }

    public void destroyShadow(){
        if(shadowScale.x > 0){
            shadowScale.x -= 5 * Time.deltaTime;
            shadowScale.y -= 5 * Time.deltaTime;
        } else {
            shadowScale.x = 0;
            shadowScale.y = 0;
            Destroy(shadow);
            // Destroy(GetComponent<SpriteShadow>());
        }
        shadow.transform.localScale = shadowScale;
        
    }
}
