using System;
using UnityEngine;

[Serializable]
public class Dialog
{
    // name of person talking
    public string Name;

    // list of sentences defined as text are with min of 3 lines, max of 5
    [TextArea(3, 5)] public string[] Sentences;
}