using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    public Player Player = new Player();
    public Level[] Levels = new Level[] { new Level {} };
}