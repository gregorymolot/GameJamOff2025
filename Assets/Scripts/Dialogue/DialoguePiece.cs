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
    [TextArea(3,10)]
    public string dialogueLine;
    public NextLineBehaviour lineBehaviour;
    public bool hasClue;
    public Clues clue;
    public bool isLying;
}
