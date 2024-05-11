using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerJumping), typeof(PlayerWallSliding))]
[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    public PlayerMovement Movement { get; private set; }
    public PlayerJumping Jumping { get; private set; }
    public PlayerWallSliding WallSliding { get; private set; }
    public CollisionHandler CollisionHandler { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        Jumping = GetComponent<PlayerJumping>();
        WallSliding = GetComponent<PlayerWallSliding>();
        CollisionHandler = GetComponent<CollisionHandler>();
    }
}
