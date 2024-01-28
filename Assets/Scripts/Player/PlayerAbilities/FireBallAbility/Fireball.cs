using UnityEngine;

public class Fireball : MonoBehaviour
{
    private readonly string explodeParameter = "Explode";

    [Header("FireBall Settings")]
    [SerializeField ] private float startSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationDuration;
    [SerializeField] private float aliveTime = 6;

    private float speed;

    private int damage;
    private float knockBackForce;
    private Vector3 direction;

    private float elapsedTime;
    private bool exploded = false;

    private Animator anim;
    private Rigidbody rigidBody;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        elapsedTime = 0;
        speed = startSpeed;
        Invoke(nameof(StartExplosion), aliveTime);
    }

    public void SetUpFireBall(int damage, float knockBackForce, Vector3 direction, PlayerController playerController)
    {
        this.playerController = playerController;
        this.damage = damage;
        this.knockBackForce = knockBackForce;
        this.direction = direction;
    }

    private void FixedUpdate()
    {
        if (speed != maxSpeed && !exploded)
        {
            float speed = Mathf.Lerp(startSpeed, maxSpeed, Mathf.Pow((elapsedTime / accelerationDuration), 2));
            rigidBody.velocity = speed * Time.fixedDeltaTime * direction; 
            elapsedTime += Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage, playerController, knockBackForce, direction, Color.white);
            CancelInvoke(nameof(StartExplosion));
            StartExplosion();
        }
    }

    public void StartExplosion()
    {
        anim.SetTrigger(explodeParameter);
        exploded = true;
        rigidBody.velocity = Vector3.zero;
    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}
