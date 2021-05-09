using UnityEngine.InputSystem;

public interface IPlayerMovement
{
    public void OnMove(InputValue value);
    public void OnJump(InputValue value);
    public void CancelJumpInput();
}