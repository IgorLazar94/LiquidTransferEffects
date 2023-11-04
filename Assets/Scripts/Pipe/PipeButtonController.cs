using UnityEngine;
using UnityEngine.EventSystems;

public class PipeButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Pipe pipe;
    private bool isButtonPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
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
