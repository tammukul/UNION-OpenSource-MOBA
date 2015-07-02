using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour
{
    /// <summary>
    /// Player manager reference.
    /// </summary>
    PlayerManager playerManager;
    /// <summary>
    /// Shooter manager reference.
    /// </summary>
    ShooterManager shooterManager;

    /// <summary>
    /// Left touch finger ID.
    /// </summary>
    int leftTouchfingerID;
    /// <summary>
    /// Right touch finger ID.
    /// </summary>
    int rightTouchfingerID;

    /// <summary>
    /// Initializes all necessary references.
    /// </summary>
    /// <param name="playerControlled">Boolean containing if the player is local and is not a bot.</param>
    public void StartMovementManager(bool playerControlled)
    {
        playerManager = this.GetComponent<PlayerManager>();
        shooterManager = this.GetComponent<ShooterManager>();

        if (playerControlled)
        {
            InputManager.instance.AddListener(OnTouchBegan, OnTouchMove, OnTouchEnd);
        }

        leftTouchfingerID = -1;
        rightTouchfingerID = -1;
    }

    /// <summary>
    /// Touch began callback.
    /// </summary>
    /// <param name="touch">Touch info.</param>
    void OnTouchBegan(CustomTouch touch)
    {
        switch (touch.side)
        {
            case TouchSides.Left:
                leftTouchfingerID = touch.fingerID;
                break;
            case TouchSides.Right:
                playerManager.StartShoot();
                rightTouchfingerID = touch.fingerID;
                break;
        }
    }

    /// <summary>
    /// Touch moved callback.
    /// </summary>
    /// <param name="touch">Touch info.</param>
    /// <param name="deltaPosition">Touch position difference.</param>
    void OnTouchMove(CustomTouch touch, Vector2 deltaPosition)
    {
        if (touch.fingerID == leftTouchfingerID)
        {
            Vector3 direction = new Vector3(deltaPosition.x, 0, deltaPosition.y).normalized;
            playerManager.Move(direction,true);
        }
        if (touch.fingerID == rightTouchfingerID)
        {
            this.RotateMeAndShoot(deltaPosition);
        }
    }

    /// <summary>
    /// Touch ended callback.
    /// </summary>
    /// <param name="touch">Touch info.</param>
    void OnTouchEnd(CustomTouch touch)
    {
        switch (touch.side)
        {
            case TouchSides.Left:
                leftTouchfingerID = -1;
                playerManager.StopMoving();
                break;
            case TouchSides.Right:
                rightTouchfingerID = -1;
                playerManager.StopShooting();
                break;
        }
    }

    /// <summary>
    /// Rotates player, and updates shot, based on touch movement.
    /// </summary>
    /// <param name="deltaPosition">Position difference.</param>
    public void RotateMeAndShoot(Vector3 deltaPosition)
    {
        playerManager.childTransform.LookAt(this.transform.position + new Vector3(deltaPosition.x, 0, deltaPosition.y));

        shooterManager.UpdateShoot();
    }
}