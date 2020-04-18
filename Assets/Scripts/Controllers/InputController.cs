using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedLeft = delegate { };
    public event Action PressedRight = delegate { };

    public event Action Pressed1 = delegate { };
    public event Action Pressed2 = delegate { };
    public event Action Pressed3 = delegate { };
    public event Action Pressed4 = delegate { };

    void Update()
    {
        DetectConfirm();
        DetectCancel();
        DetectLeft();
        DetectRight();

        Detect1();
        Detect2();
        Detect3();
        Detect4();
    }

    private void Detect1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Pressed1.Invoke();
        }
    }

    private void Detect2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Pressed2.Invoke();
        }
    }

    private void Detect3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Pressed3.Invoke();
        }
    }

    private void Detect4()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Pressed4.Invoke();
        }
    }

    private void DetectRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PressedRight?.Invoke();
        }
    }

    private void DetectLeft()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressedLeft?.Invoke();
        }
    }

    private void DetectCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedCancel?.Invoke();
        }
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressedConfirm?.Invoke();
        }
    }
}
