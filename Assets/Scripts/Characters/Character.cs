using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    public CharacterType CharacterType;
    public string characterName;
    public GameObject characterPrefab;
    public float characterSpped;
    public bool isLocked;
}
