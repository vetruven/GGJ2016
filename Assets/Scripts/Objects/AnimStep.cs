using UnityEngine;
using System.Collections;
using System;

public class AnimStep : MonoBehaviour
{
    public float pauseEverySeconds;
    public float animationPausedFor;
    public float timeToComplete;
    //public bool shouldDestroyOnComplete;
    public LeanTweenType easyType;

    public bool startedCoroutine;
    public float stepAnimationTimeSum;
    public GameObject DemonHand;
    public Vector2 where2Go;
    public Action nextStep { set; get; }
    public Action customOnUpdate;
    public LTDescr deamHandLTDesc;


    void Start()
    {
        this.where2Go = transform.position;
    }

    public AnimStep SetAnimStep(float pauseEverySeconds, float animationPauseForSeconds, float timeToComplete, GameObject deamonHandObject, Action nextStep)
    {
        ResetAnimationStepCounters();
        this.DemonHand = deamonHandObject;
        //this.pauseEverySeconds = pauseEverySeconds;
        this.animationPausedFor = animationPauseForSeconds;
        this.timeToComplete = timeToComplete;
        this.nextStep = nextStep;
        return this;
    }


    public void OnUpdate(float floatStuff)
    {

        if (pauseEverySeconds != 0)
        {
            Debug.Log("Stuff1");
            if (stepAnimationTimeSum < pauseEverySeconds)
            {
                stepAnimationTimeSum += Time.deltaTime;
            }
            else
            {
                Debug.Log("Stuff2");
                if (!startedCoroutine)
                {
                    StopAnim();
                    startedCoroutine = true;
                    StartCoroutine("timerCoroutine");
                }
            }
        }
        customOnUpdate.SafeInvoke();
    }

    public void ClearAnimation()
    {
        if (deamHandLTDesc != null)
            LeanTween.cancel(deamHandLTDesc.id);
        ResetAnimationStepCounters();
    }

    void StopAnim()
    {
        deamHandLTDesc.pause();
    }

    void ContinueAnim()
    {
        deamHandLTDesc.resume();
    }

    public void ResetAnimationStepCounters()
    {
        stepAnimationTimeSum = 0;
        startedCoroutine = false;
    }

    public void DoAnim()
    {
        Debug.Log("DoAnim "+gameObject.name);
        deamHandLTDesc = LeanTween.move(DemonHand, where2Go, timeToComplete);
        //deamHandLTDesc.setDestroyOnComplete(shouldDestroyOnComplete);
        deamHandLTDesc.setOnUpdate(OnUpdate);
        deamHandLTDesc.setOnComplete(nextStep);
        deamHandLTDesc.setEase(easyType);
        deamHandLTDesc.init();
    }

    public IEnumerator timerCoroutine()
    {
        Debug.Log("Stuff13");
        yield return new WaitForSeconds(animationPausedFor);
        stepAnimationTimeSum = 0;
        startedCoroutine = false;
        ContinueAnim();
    }

}
