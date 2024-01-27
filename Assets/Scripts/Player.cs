using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private PlayerControlScheme[] controlSchemesPerLevel;

    [Header("References")]
    public PlayerControlUI controlUI;

    private Rigidbody rigidBody;
    private Vector2 movement;
    private int level = 0;
    private float xp = 0;

    private readonly Dictionary<PlayerActionType, Action<Player>> actionConverter = new()
    {
        { PlayerActionType.Up, (Player player) => { player.UpAction(); } },
        { PlayerActionType.Down, (Player player) => { player.DownAction(); } },
        { PlayerActionType.Left, (Player player) => { player.LeftAction(); } },
        { PlayerActionType.Right, (Player player) => { player.RightAction(); } },
    };

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        controlUI.titleText.text = gameObject.name;
        UpdateUI();

        foreach (PlayerControlScheme controlScheme in controlSchemesPerLevel)
        {
            controlScheme.Setup();
        }
    }

    private void Update()
    {
        movement = Vector2.zero;

        foreach (PlayerControlScheme.PlayerAction playerAction in controlSchemesPerLevel[level].AllActions) 
        {
            if (Input.GetKey(playerAction.key))
            {
                foreach (PlayerActionType playerActionType in playerAction.actions)
                {
                    actionConverter[playerActionType](this);
                }
            }
        }

        rigidBody.velocity = moveSpeed * new Vector3(movement.x, 0, movement.y);
    }

    public void LevelUp()
    {
        if (xp < 1f) { return; }
        if (level == controlSchemesPerLevel.Length - 1) { return; }

        xp = 0;
        level++;
    }

    private void UpdateUI()
    {
        controlUI.UpText.text = controlSchemesPerLevel[level].Up.key.ToString();
        controlUI.DownText.text = controlSchemesPerLevel[level].Down.key.ToString();
        controlUI.RightText.text = controlSchemesPerLevel[level].Right.key.ToString();
        controlUI.LeftText.text = controlSchemesPerLevel[level].Left.key.ToString();

        controlUI.Special1Text.text = controlSchemesPerLevel[level].Special1.key.ToString();
        controlUI.Special2Text.text = controlSchemesPerLevel[level].Special2.key.ToString();
        controlUI.levelSlider.value = xp;
        controlUI.levelIndicator.text = level.ToString();
    }

    public void UpAction()
    {
        movement.y += 1;
    }

    public void DownAction()
    {
        movement.y -= 1;
    }

    public void RightAction()
    {
        movement.x += 1;
    }

    public void LeftAction()
    {
        movement.x -= 1;
    }
}
