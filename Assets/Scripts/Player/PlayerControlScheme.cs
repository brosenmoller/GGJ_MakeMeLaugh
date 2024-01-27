using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionType
{
    Up = 0,
    Down = 1,
    Left = 2, 
    Right = 3,

    Fireball = 4,
    Firepit = 5,
}

[Serializable]
public class PlayerControlScheme
{
    public PlayerAction Up;
    public PlayerAction Down;
    public PlayerAction Right;
    public PlayerAction Left;
    public PlayerAction Special1;
    public PlayerAction Special2;

    [HideInInspector] public List<PlayerAction> AllActions;

    public void Setup()
    {
        AllActions = new List<PlayerAction>() { Up, Down, Right, Left, Special1, Special2 };
    }

    [Serializable]
    public class PlayerAction
    {
        public KeyCode key;
        public PlayerActionType[] actions;
    }
}
