using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string Name;
    [TextArea(3, 10)]
    public string Sentences;

}
