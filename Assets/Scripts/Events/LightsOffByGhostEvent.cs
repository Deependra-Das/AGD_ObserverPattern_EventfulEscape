using UnityEngine;

public class LightsOffByGhostEvent : MonoBehaviour
{
    [SerializeField]
    private int keyRequiredToTrigger;

    [SerializeField]
    private SoundType soundType;

    private void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();

        if(playerView!=null && keyRequiredToTrigger == GameService.Instance.GetPlayerController().KeysEquipped)
        {
            EventService.Instance.OnLightsOffByGhostEvent.InvokeEvent();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
            GetComponent<Collider>().enabled = false;
        }
    }

}