using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;

    [Header("Settings")]
    [SerializeField] private float playerRotationSpeed;

    [Header("Player Control")]
    [SerializeField] private PlayerControlScheme[] controlSchemesPerLevel;

    [Header("References")]
    public PlayerControlUI controlUI;
    public Transform Visuals;
    public Animator animator;

    private Rigidbody rigidBody;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Vector3 lastDirection = Vector3.forward;
    [HideInInspector] public bool CanMove = true;
    private int level = 0;
    private float xp = 0;
    private float health;

    private PlayerDash dash;
    private PlayerStab stab;
    private PlayerSlowZone slow;
    private PlayerFireFieldAbility fireHole;
    private FireBallAbility fireBall;

    private readonly Dictionary<PlayerActionType, Action<PlayerController>> actionConverter = new()
    {
        { PlayerActionType.Up, (PlayerController player) => { player.UpAction(); } },
        { PlayerActionType.Down, (PlayerController player) => { player.DownAction(); } },
        { PlayerActionType.Left, (PlayerController player) => { player.LeftAction(); } },
        { PlayerActionType.Right, (PlayerController player) => { player.RightAction(); } },
        { PlayerActionType.Dash, (PlayerController player) => { player.DashAction();} },
        { PlayerActionType.Stab, (PlayerController player) => { player.StabAction();} },
        { PlayerActionType.Slow, (PlayerController player) => { player.SlowAction();} },
        { PlayerActionType.Firepit, (PlayerController player) => { player.FireFieldSpawnAction();} },
        { PlayerActionType.Fireball, (PlayerController player) => { player.FireBallAction();} },
        
    };

    private void Awake()
    {
        health = maxHealth;
        rigidBody = GetComponent<Rigidbody>();
        controlUI.titleText.text = gameObject.name;
        UpdateUI();

       dash = GetComponent<PlayerDash>();
       stab = GetComponent<PlayerStab>();
       slow = GetComponent<PlayerSlowZone>();
       fireHole = GetComponent<PlayerFireFieldAbility>();
       fireBall = GetComponent<FireBallAbility>();

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

        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (movement != Vector2.zero)
        {
            lastDirection = direction;
            if (animator != null) { animator.SetBool("Walking", true); }
        }
        else
        {
            if (animator != null) { animator.SetBool("Walking", false); }
        }

        if (CanMove)
        {
            rigidBody.velocity = moveSpeed * direction;
        }

        Visuals.forward = Vector3.Slerp(
            Visuals.forward,
            lastDirection,
            Time.deltaTime * playerRotationSpeed
        );
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateUI();

        if (health > maxHealth) 
        {
            health = maxHealth; 
        }

        if (health < 0)
        {
            Debug.Log("Player Death");
        }
    }

    public void LevelUp()
    {
        if (xp < 1f) { return; }
        if (level == controlSchemesPerLevel.Length - 1) { return; }

        xp = 0;
        level++;
        UpdateUI();
    }

    public void GiveXp(float xp)
    {
        this.xp += xp;
        UpdateUI();
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
        controlUI.healthSlider.value = health / maxHealth;
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
    
    public void DashAction() => dash.Activate();
    public void StabAction() => stab.Activate();
    public void SlowAction() => slow.Activate();

    public void FireFieldSpawnAction() => fireHole.Activate();
    public void FireBallAction() => fireBall.Activate();
}
