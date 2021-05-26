using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInstanciationController : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    private GameObject playerGameObject;
    private Player playerClass;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        GameEvents.current.OnChangeInputScheme += ChangeInputScheme;
    }

    void OnDisable()
    {
        GameEvents.current.OnChangeInputScheme -= ChangeInputScheme;
    }

    public void ChangeInputScheme(string scheme)
    {
        print("change input scheme for player");
        playerInput.SwitchCurrentActionMap(scheme);
        print(playerInput.currentActionMap);
    }

    public Player handleInstanciate(int index, Vector3 spawn)
    {
        playerGameObject = Instantiate(playerPrefabs[index], spawn, Quaternion.identity);
        playerGameObject.name = playerGameObject.name.Replace("(Clone)", "").Trim();
        playerClass = playerGameObject.GetComponent<Player>();
        playerClass.index = index;
        return playerClass;
    }

    // UI, unused for now

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 inputVector2 = context.ReadValue<Vector2>();
            GameEvents.current.UINavigationTrigger(NormalizeOrientation.Normalize(inputVector2));
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEvents.current.UISubmitTrigger();
        }
    }

    // GAME
    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEvents.current.UIStartTrigger();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (playerClass)
        {
            playerClass.OnJump(context);
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (playerClass)
        {
            playerClass.OnThrow(context);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerClass)
        {
            playerClass.OnMove(context);
        }
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (playerClass)
        {
            playerClass.OnPunch(context);
        }
    }
}
