using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;
    [SerializeField] private float sanityDropAmountPerEvent = 10f;
    private float maxSanity;
    private PlayerController playerController;
    private bool hasPlayerEscaped;

    private void OnEnable()
    {
        EventService.Instance.OnRatRushEvent.AddListener(OnSupernaturalEvent);
        EventService.Instance.OnSkullDrop.AddListener(OnSupernaturalEvent);
        EventService.Instance.OnPotionDrinkEvent.AddListener(OnDrankPotion);
        EventService.Instance.OnPlayerEscapedEvent.AddListener(OnPlayerEscaped);
        
    }
    private void OnDisable()
    {
        EventService.Instance.OnRatRushEvent.RemoveListener(OnSupernaturalEvent);
        EventService.Instance.OnSkullDrop.RemoveListener(OnSupernaturalEvent);
        EventService.Instance.OnPotionDrinkEvent.RemoveListener(OnDrankPotion);
        EventService.Instance.OnPlayerEscapedEvent.RemoveListener(OnPlayerEscaped);
    }

    private void Start()
    {
        hasPlayerEscaped = false;
        maxSanity = sanityLevel;
        playerController = GameService.Instance.GetPlayerController();
        playerController.Insanity = (1f - sanityLevel / maxSanity) * 100;
    }
    void Update()
    {
        if (playerController.PlayerState == PlayerState.Dead)
            return;
        if (hasPlayerEscaped)
            return;

        float sanityDrop = updateSanity();

        increaseSanity(sanityDrop);
        playerController.Insanity = (1f - sanityLevel / maxSanity) * 100;
    }

    private float updateSanity()
    {
        float sanityDrop = sanityDropRate * Time.deltaTime;
        if (playerController.PlayerState == PlayerState.InDark)
        {
            sanityDrop *= 10f;
        }
        return sanityDrop;
    }

    private void increaseSanity(float amountToDecrease)
    {
        Mathf.Floor(sanityLevel -= amountToDecrease);
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameService.Instance.GameOver();
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    private void decreaseSanity(float amountToIncrease)
    {
        Mathf.Floor(sanityLevel += amountToIncrease);
        if (sanityLevel > 100)
        {
            sanityLevel = 100;
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }
    private void OnSupernaturalEvent()
    {
        increaseSanity(sanityDropAmountPerEvent);
    }

    private void OnDrankPotion(int potionEffect, int potionsDrank)
    {
        decreaseSanity(potionEffect);
    }
    private void OnPlayerEscaped()
    {
        hasPlayerEscaped = true;
    }



}