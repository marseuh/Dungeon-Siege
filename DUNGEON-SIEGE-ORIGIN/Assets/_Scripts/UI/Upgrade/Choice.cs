using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice", menuName = "ScriptableObjects/Create new Choice", order = 1)]
public class Choice : ScriptableObject
{
    public new string name;
    public string description;
}
