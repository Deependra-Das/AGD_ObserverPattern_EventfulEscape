using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    private PlayerController playerController;

    [Header("Views")]
    [SerializeField] private PlayerView playerView;
    [SerializeField] private SoundView soundView;
    [SerializeField] private GameUIView gameUIView;
    [SerializeField] private InstructionView instructionView;
    [SerializeField] private AchievementView achievementView;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    private void Start() => playerController = new PlayerController(playerView, playerScriptableObject);

    //Todo - Ask Mayank -> Converting all these into properties as {get;private set;} is good or 
    // we can keep it same for now?

    public PlayerController GetPlayerController() => playerController;
    public GameUIView GetGameUI() => gameUIView;
    public InstructionView GetInstructionView() => instructionView;
    public AchievementView GetAchievementView() => achievementView;
    public SoundView GetSoundView() => soundView;


    public void GameOver()
    {
        playerController.KillPlayer();
        soundView.PlaySoundEffects(SoundType.JumpScare1);
    }
}
