using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    [SerializeField] private SoundType soundType;
    private SwitchState currentState;
    bool masterShadowTriggered;
    private Coroutine timerCoroutine;
    private float elapsedTime;
    private float triggerTime;
    private void OnEnable()
    {
        EventService.Instance.OnLightSwitchToggled.AddListener(onLightsToggled);
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(onLightsOffByGhostEvent);
    }

    private void OnDisable()
    {
        EventService.Instance.OnLightSwitchToggled.RemoveListener(onLightsToggled);
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(onLightsOffByGhostEvent);
    }

    private void Start()
    {
        elapsedTime = 0f;
        masterShadowTriggered = false;
        currentState = SwitchState.Off;
        TimerWhenDark(currentState==SwitchState.Off);
        triggerTime = GameService.Instance.GetAchievementView().SecondsRequiredToTrigger;

    }
    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        EventService.Instance.OnLightSwitchToggled.InvokeEvent();
    }

    private void Update()
    {
        if (elapsedTime >= triggerTime && !masterShadowTriggered)
        {
            EventService.Instance.OnMasterShadow.InvokeEvent();
            masterShadowTriggered = true;
        }
    }

    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
        TimerWhenDark(currentState == SwitchState.Off);
    }

    private void setLights(bool lights)
    {
        if (lights)
            currentState = SwitchState.On;
        else
            currentState = SwitchState.Off;

        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
        TimerWhenDark(currentState == SwitchState.Off);
    }
    private void onLightsOffByGhostEvent()
    {
        GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
        setLights(false);
    }
    private void onLightsToggled()
    {
        toggleLights();
        GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
    }

    void TimerWhenDark(bool isOn)
    {
        if (isOn)
        {
            if (timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(TimerCoroutine());
            
            }
        }
        else
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
        }
    }

    IEnumerator TimerCoroutine()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime; 
            Debug.Log("Time: " + elapsedTime.ToString("F2") + "s"); 

            yield return null;
        }
    }
}
