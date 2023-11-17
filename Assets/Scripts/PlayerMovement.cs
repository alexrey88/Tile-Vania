using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gun;

    Vector2 moveInput;

    Rigidbody2D playerRigidBody;
    CapsuleCollider2D playerBody;
    BoxCollider2D playerFeet;
    Animator playerAnimator;

    MovePlatform movePlatform;

    bool playerHasHorizontalSpeed;
    float gravityScaleAtStart;
    bool isAlive = true;
    bool isClimbing = false;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBody = GetComponent<CapsuleCollider2D>();
        playerFeet = GetComponent<BoxCollider2D>();

        if (playerRigidBody == null || playerAnimator == null || playerBody == null || playerFeet == null)
        {
            Debug.LogError("Player component(s) not found.");
        }

        gravityScaleAtStart = playerRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }

        CheckIfPlayerIsRunning();
        Run();
        Climb();
        FlipSprite();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive || !value.isPressed) { return; }
        Instantiate(bulletPrefab, gun.position, transform.rotation);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive || !value.isPressed) { return; }
        Jump();
    }

    void Jump()
    {
        if (playerIsInTheAir()) { return; }
        playerRigidBody.velocity += new Vector2(0f, Mathf.Sign(transform.localScale.y) * jumpSpeed);
    }

    bool playerIsInTheAir()
    {
        return !playerBody.IsTouchingLayers(LayerMask.GetMask(GameConstants.ClimbingLayerName,
                                                            GameConstants.GroundLayerName,
                                                            GameConstants.MovingPlatformLayerName));
    }

    void CheckIfPlayerIsRunning()
    {
        playerHasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
    }

    void Run()
    {
        float horizontalVelocity = runSpeed * moveInput.x + GetMovingPlatformSpeedIncrease();
        playerRigidBody.velocity = new Vector2(horizontalVelocity, playerRigidBody.velocity.y);
        playerAnimator.SetBool(GameConstants.RunningAnimationBool, playerHasHorizontalSpeed);
    }

    void Climb()
    {
        if (CannotClimb())
        {
            ResetNonClimbingState();
            return;
        }

        SetClimbingState();
    }

    bool CannotClimb()
    {
        return !playerBody.IsTouchingLayers(LayerMask.GetMask(GameConstants.ClimbingLayerName))
                || (Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon && moveInput.y == 0 && !isClimbing);
    }

    void ResetNonClimbingState()
    {
        playerRigidBody.gravityScale = Mathf.Sign(playerRigidBody.gravityScale) * gravityScaleAtStart;
        playerAnimator.SetBool(GameConstants.ClimbingAnimationBool, false);
    }

    void SetClimbingState()
    {
        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, climbSpeed * moveInput.y);
        playerRigidBody.gravityScale = 0f;

        isClimbing = moveInput.y > 0;

        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool(GameConstants.ClimbingAnimationBool, playerHasVerticalSpeed);
    }

    void FlipSprite()
    {
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), transform.localScale.y);
        }
    }

    void Die()
    {
        if (playerBody.IsTouchingLayers(LayerMask.GetMask(GameConstants.EnemyLayerName, GameConstants.HazardsLayerName)))
        {
            isAlive = false;
            GameSession gameSession = GameSession.Instance;

            if (gameSession != null)
            {
                gameSession.ProcessPlayerDeath();
            }
            else { Debug.LogError("GameSession.Instance not found."); }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.MovePlatformTag))
        {
            movePlatform = other.gameObject.GetComponent<MovePlatform>();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.MovePlatformTag))
        {
            movePlatform = null;
        }
    }

    float GetMovingPlatformSpeedIncrease()
    {
        if (movePlatform != null)
        {
            return movePlatform.GetPlatformSpeed();
        }
        return 0f;
    }
}
