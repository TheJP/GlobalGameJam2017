using UnityEngine;
using System.Collections;
using System;

public interface MainScreenInterface
{
    void Sync();
    void SlideIn();
    void SlideOut(SlideDirection to);
    void Enable();
    void Disable(bool keepAlive);
}

public class SuperScreenClass : MonoBehaviour, MainScreenInterface
{
    public ScreenState CurrentState;
    public GameObject Screen;
    public GameObject ParentCanvas;

    public float SlideSpeed = 3.75f;

    private GameObject ScreenSlotRight;
    private GameObject ScreenSlotLeft;
    private GameObject ScreenSlotTop;
    private GameObject ScreenSlotBottom;
    private GameObject ScreenSlotCenter;


    public virtual void Sync()
    {
        // Override
    }

    public virtual void Reset()
    {
        // Override
    }

    public void Disable(bool keepAlive)
    {
        if (keepAlive)
        {
            Screen.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            ParentCanvas.transform.FindChild(Screen.name).gameObject.SetActive(false);
        }
        CurrentState = ScreenState.inactive;
    }

    public void Enable()
    {
        CurrentState = ScreenState.active;
        Screen.GetComponent<CanvasGroup>().alpha = 1;
        ParentCanvas.transform.FindChild(Screen.name).gameObject.SetActive(true);
    }

    public void SlideIn()
    {
        ScreenSlotCenter = GameObject.Find("SlotCenter");
        if (Application.isPlaying)
        {
            StartCoroutine(MoveScreenFromTo(Screen.transform.position, ScreenSlotCenter.transform.position, .75f));
        }
        else
        {
            Screen.transform.position = ScreenSlotCenter.transform.position;
        }
    }

    public void SlideOut(SlideDirection to)
    {
        ScreenSlotRight = GameObject.Find("SlotRight");
        ScreenSlotLeft = GameObject.Find("SlotLeft");
        //ScreenSlotTop = GameObject.Find("SlotTop");
        //ScreenSlotBottom = GameObject.Find("SlotBottom");

        switch (to)
        {
            case SlideDirection.left:
                if (Application.isPlaying)
                {
                    StartCoroutine(MoveScreenFromTo(Screen.transform.position, ScreenSlotLeft.transform.position, .75f));
                }
                else
                {
                    Screen.transform.position = ScreenSlotLeft.transform.position;
                }
                break;
            case SlideDirection.right:
                if (Application.isPlaying)
                {
                    StartCoroutine(MoveScreenFromTo(Screen.transform.position, ScreenSlotRight.transform.position, .75f));
                }
                else
                {
                    Screen.transform.position = ScreenSlotRight.transform.position;
                }
                break;
            //case SlideDirection.top:
            //    if (Application.isPlaying)
            //    {
            //        StartCoroutine(MoveScreenFromTo(Screen.transform.position, ScreenSlotTop.transform.position, .75f));
            //    }
            //    else
            //    {
            //        Screen.transform.position = ScreenSlotTop.transform.position;
            //    }
            //    break;
            //case SlideDirection.bottom:
            //    if (Application.isPlaying)
            //    {
            //        StartCoroutine(MoveScreenFromTo(Screen.transform.position, ScreenSlotBottom.transform.position, .75f));
            //    }
            //    else
            //    {
            //        Screen.transform.position = ScreenSlotBottom.transform.position;
            //    }
            //    break;
        }
        
    }

    private IEnumerator MoveScreenFromTo(Vector3 a, Vector3 b, float duration)
    {
        var timeRemaining = duration;
        while(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            Screen.transform.position = Vector3.Lerp(a, b, Mathf.InverseLerp(duration, 0, Mathf.SmoothStep(0.0f, 1.0f, timeRemaining)));
            yield return null;
        }
        Screen.transform.position = b;
    }
}

public enum SlideDirection { right, left, top, bottom}
public enum ScreenState { active, inactive }
