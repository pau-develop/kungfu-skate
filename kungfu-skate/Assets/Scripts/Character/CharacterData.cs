﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public bool isPlayer;
    public bool autoMove = true;
    public string bodySpriteName;
    public string armSpriteName;
    public string folder;
    public int hitPoints;
    public int explodeThreshold;
    public AudioClip shoot;
    public AudioClip swing;
    public AudioClip[] die;
    public AudioClip hitProjectile;
    public AudioClip hitObstacle;
    public AudioClip explode;
    public AudioClip melee;
    //ENEMY ONLY
    public Vector2 ninjaPos;
    public bool shouldLeave = false;
    public bool reachedInitialDestPos = false;
}
