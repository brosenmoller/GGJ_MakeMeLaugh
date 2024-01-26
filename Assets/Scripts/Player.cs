using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField] private float moveSpeed;

    [Header("References")]
    public TextMeshProUGUI UpText;
    public TextMeshProUGUI DownText;
    public TextMeshProUGUI RightText;
    public TextMeshProUGUI LeftText;

    [HideInInspector] public KeyCode Up;
    [HideInInspector] public KeyCode Down;
    [HideInInspector] public KeyCode Right;
    [HideInInspector] public KeyCode Left;

    private Vector2 movement;

    private void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(Up)) { movement.x -= 1; }
        if (Input.GetKey(Down)) { movement.x += 1; }
        if (Input.GetKey(Right)) { movement.y += 1; }
        if (Input.GetKey(Left)) { movement.y -= 1; }

        transform.Translate(moveSpeed * Time.deltaTime * new Vector3(movement.x, 0, movement.y));
    }
}
