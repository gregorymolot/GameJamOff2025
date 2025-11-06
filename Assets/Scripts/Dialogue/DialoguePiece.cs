using System;
using UnityEngine;

public enum NextLineBehaviour
{
    Normal,
    ReturningLine,
    EndLine
}

[System.Serializable]
public class DialoguePiece
{
    public string dialogueLine;
    public NextLineBehaviour lineBehaviour;
}
