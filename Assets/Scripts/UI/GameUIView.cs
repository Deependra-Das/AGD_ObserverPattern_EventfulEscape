using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIView : MonoBehaviour
{
    [Header("Player Sanity")]
    [SerializeField] GameObject rootViewPanel;
    [SerializeField] Image insanityImage;
    [SerializeField] Image redVignette;
    [SerializeField] Image greenVignette;

    [Header("Keys UI")]
    [SerializeField] TextMeshProUGUI keysFoundText;

    [Header("Game End Panel")]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TextMeshProUGUI gameEndText;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button quitButton;

    private void OnEnable()
    {
        EventService.Instance.OnKeyPickedUp.AddListener(OnKeyEquipped);
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(SetRedVignette);
        EventService.Instance.OnPlayerEscapedEvent.AddListener(OnPlayerEscaped);
        EventService.Instance.OnPlayerDeathEvent.AddListener(SetRedVignette);
        EventService.Instance.OnRatRushEvent.AddListener(SetRedVignette);
        EventService.Instance.OnSkullDrop.AddListener(SetRedVignette);
        EventService.Instance.OnPlayerDeathEvent.AddListener(OnPlayerDeath);
        EventService.Instance.OnPotionDrinkEvent.AddListener(SetGreenVignette);
        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        EventService.Instance.OnKeyPickedUp.RemoveListener(OnKeyEquipped);
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnPlayerEscapedEvent.RemoveListener(OnPlayerEscaped);
        EventService.Instance.OnPlayerDeathEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnRatRushEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnSkullDrop.RemoveListener(SetRedVignette);
        EventService.Instance.OnPotionDrinkEvent.RemoveListener(SetGreenVignette);
        EventService.Instance.OnPlayerDeathEvent.RemoveListener(OnPlayerDeath);
    }

    public void UpdateInsanity(float playerSanity) => insanityImage.rectTransform.localScale = new Vector3(1, playerSanity, 1);
    private void OnKeyEquipped(int keys) => keysFoundText.SetText($"Keys Found: {keys}/4");
    private void OnQuitButtonClicked() => Application.Quit();
    private void OnTryAgainButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void SetRedVignette()
    {
        redVignette.enabled = true;
        redVignette.canvasRenderer.SetAlpha(0.5f);
        redVignette.CrossFadeAlpha(0, 5, false);
    }

    private void OnPlayerDeath()
    {
        gameEndText.SetText("Game Over");
        gameEndPanel.SetActive(true);
    }

    private void OnPlayerEscaped()
    {
        gameEndText.SetText("You Escaped");
        gameEndPanel.SetActive(true);
    }
    private void SetGreenVignette(int potionEffect, int potionsDrank)
    {
        greenVignette.enabled = true;
        greenVignette.canvasRenderer.SetAlpha(0.5f);
        greenVignette.CrossFadeAlpha(0, 5, false);
    }
}

