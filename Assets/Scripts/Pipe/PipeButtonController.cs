using UnityEngine;
using UnityEngine.EventSystems;

public class PipeButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Pipe pipe;
    private bool isButtonPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX(AudioCollection.EngineStart, false);

        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.StopSFX(AudioCollection.EngineStart);
        AudioManager.instance.PlaySFX(AudioCollection.EngineEnd, false);
        isButtonPressed = false;
    }

    void Update()
    {
        if (isButtonPressed)
        {
            if (gameObject.name == StringList.PipeLiftingButton)
            {
                pipe.LiftingPipe();
            }
            if (gameObject.name == StringList.PipeLoweringButton)
            {
                pipe.LoweringPipe();
            }
        }
    }
}
