using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerController))]
public abstract class PlayerAbility : MonoBehaviour
{
    public Rigidbody RigidBody { get; private set; }
    public PlayerController PlayerController { get; private set; }

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        PlayerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize() { }
    public abstract void Activate();
}

