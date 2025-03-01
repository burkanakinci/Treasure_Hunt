﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : CustomBehaviour
{
    public static GameManager Instance { get; private set; }
    #region Fields
    public PlayerManager PlayerManager;
    public JsonConverter JsonConverter;
    public InputManager InputManager;
    public UIManager UIManager;
    public LevelManager LevelManager;
    public ObjectPool ObjectPool;
    public Entities Entities;
    public VibrationsManager VibrationsManager;
    #endregion
    #region Actions
    public event Action OnResetToMainMenu;
    public event Action OnCountdownFinished;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    #endregion

    public void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Initialize();
    }
    public override void Initialize()
    {
        JsonConverter.Initialize();
        Entities.Initialize();
        ObjectPool.Initialize();
        InputManager.Initialize();
        UIManager.Initialize();
        PlayerManager.Initialize();
        LevelManager.Initialize();
        VibrationsManager.Initialize();
    }

    private void Start()
    {
        ResetToMainMenu();
    }

    #region Events
    public void ResetToMainMenu()
    {
        OnResetToMainMenu?.Invoke();
    }

    public void CountdownFinished()
    {
        OnCountdownFinished?.Invoke();
    }

    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }

    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
    #endregion
}