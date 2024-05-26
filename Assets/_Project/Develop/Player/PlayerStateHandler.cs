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
        SetDefaultState();

        _canDetermined = true;
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, PlayerState>();

        _statesMap[typeof(PlayerMovementState)] = new PlayerMovementState(_player);
        _statesMap[typeof(PlayerFlightState)] = new PlayerFlightState(_player);
        _statesMap[typeof(PlayerWallSlidingState)] = new PlayerWallSlidingState(_player);
        _statesMap[typeof(PlayerDestroyState)] = new PlayerDestroyState(_player);
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
        if (!_canDetermined) return;

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

    public void SetDestroyState()
    {
        _canDetermined = false;

        var state = GetState<PlayerDestroyState>();
        SetState(state);
    }

    private void SetDefaultState()
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
