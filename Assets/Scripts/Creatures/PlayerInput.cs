using UnityEngine;
using Creatures;

[RequireComponent(typeof(Silver))]
public class PlayerInput : MonoBehaviour
{
    private PlayerAction _input;
    private Silver _player;
    private void Awake()
    {
        _input = new PlayerAction();
        _input.Enable();

        _input.Player.Grab.performed += ctx => OnGrab();
        _input.Player.Throw.performed += ctx => OnThrow();
        _input.Player.Fly.started += ctx => OnSetFlyState(true);
        _input.Player.Fly.canceled += ctx => OnSetFlyState(false);
        _player = GetComponent<Silver>();
    }
    private void Update()
    {
        OnMove();
    }
    private void OnMove()
    {
        var direction = _input.Player.Move.ReadValue<Vector2>();

        _player.SetDirection(direction);
    }
    private void OnGrab()
    {
        _player.GrabObjects();
    }
    private void OnThrow()
    {
        _player.Throw();
    }
    private void OnSetFlyState(bool state)
    {
        _player.SetFlyState(state);
    }
}
