using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovementHandler : MonoBehaviour, IPlayerMovement
{
    public Vector2 moveInputs = Vector2.zero;
    public bool JumpOnMoveUp { get; set; }
    public bool ForceFallOnMoveDown { get; set; }

    private float MIN_AXIS_INPUT_VALUE = 0.3f;

    public void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        //Debug.Log("On move" + v.ToString());
        UpdateMoveInputs(v);
    }

    public void OnJump(InputValue value)
    {
        //Debug.Log("On Jump" + value.Get().ToString() + " " + value.isPressed);
        moveInputs.Set(moveInputs.x, value.Get<float>());
    }

    public void CancelJumpInput()
    {
        moveInputs.Set(moveInputs.x, 0);
    }

    private void UpdateMoveInputs(Vector2 v)
    {
        // Check for down rush
        bool isRushingDown = v.y < -MIN_AXIS_INPUT_VALUE;
        if (isRushingDown)
        {
            moveInputs.Set(v.x, v.y);
            return;
        }

        // Check for moving up
        bool isMovingUp = v.y > MIN_AXIS_INPUT_VALUE;
        if (isMovingUp && JumpOnMoveUp)
        {
            moveInputs.Set(v.x, 1);
            return;
        }

        // use new X and old Y
        float prevY = moveInputs.y;
        moveInputs.Set(v.x, prevY);
        return;
    }
}