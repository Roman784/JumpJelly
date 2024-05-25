using System;
using System.Collections.Generic;

public class PlayerStateHandler
{
    private Dictionary<Type, PlayerState> _statesMap;
    private PlayerState _currentState;

    private bool _canDetermined;

    private Player _player;
    private CollisionHandler _collisionHandler;

    public PlayerStateHandler(Player player, CollisionHandler collisionHandler)
    {
        _player = player;
        _collisionHandler = collisionHandler;

        InitStates();
        DefaultState();

        _canDetermined = true;
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, PlayerState>();

        _statesMap[typeof(PlayerMovementState)] = new PlayerMovementState(_player);
        _statesMap[typeof(PlayerFlightState)] = new PlayerFlightState(_player);
        _statesMap[typeof(PlayerWallSlidingState)] = new PlayerWallSlidingState(_player);
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    public void DetermineState()
    {
        PlayerState state = null;

        if (_collisionHandler.IsInAir)
        {
            state = GetState<PlayerFlightState>();
        }
        else if (_collisionHandler.OnGround)
        {
            state = GetState<PlayerMovementState>();
        }
        else if (_collisionHandler.IsTouchingWall)
        {
            state = GetState<PlayerWallSlidingState>();
        }

        if (state != null && state.GetType() != _currentState.GetType())
            SetState(state);
    }

    public void Enable()
    {
        _canDetermined = true;
        DetermineState();
    }

    public void Disable()
    {
        _canDetermined = false;
        SetState(null);
    }

    private void DefaultState()
    {
        PlayerState state = GetState<PlayerFlightState>();
        SetState(state);
    }

    private PlayerState GetState<T>() where T : PlayerState
    {
        return _statesMap[typeof(T)];
    }

    private void SetState(PlayerState state)
    {
        _currentState?.Exit();

        _currentState = state;
        _currentState?.Enter();
    }
}
