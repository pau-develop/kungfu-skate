using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    [SerializeField] private GameObject shadowPrefab;
    private GameObject shadow;
    // Start is called before the first frame update
    void Start()
    {
        shadow = Instantiate(shadowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        updateShadowPosition();
    }

    private void updateShadowPosition(){
        int botPosition = GetComponent<CharacterMovement>().botLimit;
        shadow.transform.position = new Vector2(transform.position.x, botPosition + 3);
    }
}
