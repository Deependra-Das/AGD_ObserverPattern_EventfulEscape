using UnityEngine;

public class ObjectFloatingEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && GameService.Instance.GetPlayerController().KeysEquipped == keysRequiredToTrigger)
        {
            EventService.Instance.OnObjectFloatingEvent.InvokeEvent();
            EventService.Instance.OnLightsOffByGhostEvent.InvokeEvent();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
            GetComponent<Collider>().enabled = false;
        }
    }
}