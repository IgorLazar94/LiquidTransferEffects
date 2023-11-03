using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTank : MonoBehaviour
{
    public bool DrainIsOpen { get; private set; }
    private int waterCounter;
    [SerializeField] private Transform leftDrain;
    [SerializeField] private Transform rightDrain;
    private float timeToOpenDrain = 0.5f;
    private float openDrainPos = 5f;
    private float closeDrainPos = 1.5f;

    private void Start()
    {
        DrainIsOpen = false;
    }

    private void OpenDrain()
    {
        rightDrain.DOLocalMoveX(openDrainPos, timeToOpenDrain);
        leftDrain.DOLocalMoveX(-openDrainPos, timeToOpenDrain);
    }

    private void CloseDrain()
    {
        rightDrain.DOLocalMoveX(closeDrainPos, timeToOpenDrain);
        leftDrain.DOLocalMoveX(-closeDrainPos, timeToOpenDrain);
    }

    public void SwitchDrainState()
    {
        if (!DrainIsOpen)
        {
            OpenDrain();
            DrainIsOpen = true;
        }
        else
        {
            CloseDrain();
            DrainIsOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDrain();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CloseDrain();
        }
    }
}
