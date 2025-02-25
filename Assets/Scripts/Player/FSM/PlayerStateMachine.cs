using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }
    public PlayerState previousState { get; private set; }

    public bool stateLocked;

    public Deflectable trigger { get; set; }

    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
        stateLocked = false;
    }

    public void ChangeState(PlayerState _newState)
    {
        previousState = currentState;
        currentState.Exit();
        trigger = null;
        currentState = _newState;
        currentState.Enter();
    }
    public void ChangeState(PlayerState _newState, Deflectable _trigger)
    {
        previousState = currentState;
        currentState.Exit();
        currentState = _newState;
        trigger = _trigger;
        currentState.Enter();
    }

    public void ChangeToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }

}
