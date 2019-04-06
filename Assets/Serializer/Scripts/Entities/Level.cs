using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level 
{
    public string Name;
    public int Number;

    public Status StatusState = Status.NotPlayed;

    public Difficulty DifficultyState = Difficulty.Easy;
    
    public enum Status 
    {
        NotPlayed,
        Played,
        Beaten
    }

    public enum Difficulty
    {
        Easy,
        Moderate,
        Hard,
        ExtraHard
    }
}
