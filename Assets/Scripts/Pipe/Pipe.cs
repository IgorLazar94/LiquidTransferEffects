using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pipe : MonoBehaviour
{
    [field: SerializeField] public bool IsOpen { get; private set; }
    [SerializeField] private UIController uIController;
    [SerializeField] private PipeConnectZone pipeConnectZoneLeft, pipeConnectZoneRight;
    [SerializeField] private StorageTank storageTankLeft, storageTankRight;

    private float maxHeight = 20f;
    private float minHeight = 1.5f;
    private float moveSpeed = 5f;

    private void Start()
    {
        IsOpen = false;
        pipeConnectZoneLeft.SetPipe(this);
        pipeConnectZoneRight.SetPipe(this);
    }

    public void TransferFluid(PipeConnectZone enterPipeConnectZone, LiquidElement liquidElement)
    {
        switch (enterPipeConnectZone.GetTypeOfConnectZone())
        {
            case TypeOfConnectZone.Left:
                enterPipeConnectZone.TransferFluidToPos(pipeConnectZoneRight.transform.position, liquidElement);
                liquidElement.TransferLiquid(TypeOfConnectZone.Left);
                storageTankRight.AddLiquidToList(liquidElement);
                storageTankLeft.RemoveLiquidFromList(liquidElement);
                liquidElement.SetTank(storageTankRight);
                if (storageTankRight.WaterLimit())
                {
                    uIController.OpenPipe(false);
                }
                break;
            case TypeOfConnectZone.Right:
                enterPipeConnectZone.TransferFluidToPos(pipeConnectZoneLeft.transform.position, liquidElement);
                liquidElement.TransferLiquid(TypeOfConnectZone.Right);
                storageTankLeft.AddLiquidToList(liquidElement);
                storageTankRight.RemoveLiquidFromList(liquidElement);
                liquidElement.SetTank(storageTankLeft);
                if (storageTankLeft.WaterLimit())
                {
                    uIController.OpenPipe(false);
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            LiftingPipe();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            LoweringPipe();
        }
    }

    public void LiftingPipe()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        CheckBorder();
    }

    public void LoweringPipe()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        CheckBorder();
    }

    private void CheckBorder()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = clampedPosition;
    }

    public void ActivateValve(bool isActivate)
    {
        IsOpen = isActivate;
        if (isActivate)
        {
            pipeConnectZoneLeft.transform.DOLocalMoveX(-0.3f, 0.1f).OnComplete(() => pipeConnectZoneLeft.transform.DOLocalMoveX(-0.5f, 0.1f));
            pipeConnectZoneRight.transform.DOLocalMoveX(0.3f, 0.1f).OnComplete(() => pipeConnectZoneRight.transform.DOLocalMoveX(0.5f, 0.1f));
        }
    }
}
