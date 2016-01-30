using UnityEngine;
using System.Collections;
using System;

public class AnimStep : MonoBehaviour
{
    public  float pauseEverySeconds;
    public  float animationPausedFor;
    public float timeToComplete;
    public bool shouldDestroyOnComplete;
    public LeanTweenType easyType;

    public  bool startedCoroutine;
    public float stepAnimationTimeSum;
    public GameObject DeamonHand { set; get; }
    public Vector2 where2Go;
    public Action nextStep { set; get; }
    public Action onUpdate;
    public LTDescr deamHandLTDesc;
    

    void Start()
    {
        this.where2Go = transform.position;
    }

    public AnimStep SetAnimStep(float pauseEverySeconds, float animationPauseForSeconds, float timeToComplete, GameObject deamonHandObject  , Action nextStep)
    {
        ResetAnimationStepCounters();
        this.DeamonHand = deamonHandObject;
        this.pauseEverySeconds = pauseEverySeconds;
        this.animationPausedFor = animationPauseForSeconds;
        this.timeToComplete = timeToComplete;
        this.nextStep = nextStep;
        return this;
    }
    

    public void OnUpdate(float floatStuff)
    {

        if (pauseEverySeconds == 0 || stepAnimationTimeSum < pauseEverySeconds)
        {
            stepAnimationTimeSum += Time.deltaTime;
        }
        else
        {
            if (!startedCoroutine)
            {
                StopAnim();
                startedCoroutine = true;
                StartCoroutine("timerCoroutine");
            }

        }
        onUpdate.SafeInvoke();
    }

    public void ClearAnimation()
    {

        deamHandLTDesc.cleanup();
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

    public void ResetAnimationStepCounters() {
        stepAnimationTimeSum = 0;
        startedCoroutine = false;
    }

    public void DoAnim()
    {
        deamHandLTDesc = LeanTween.move(DeamonHand, where2Go, timeToComplete);
        deamHandLTDesc.setDestroyOnComplete(shouldDestroyOnComplete);
        deamHandLTDesc.setOnUpdate(OnUpdate);
        deamHandLTDesc.setOnComplete(nextStep);
        deamHandLTDesc.setEase(easyType);
        deamHandLTDesc.init();
    }

    public IEnumerator timerCoroutine()
    {
        yield return new WaitForSeconds(animationPausedFor);
        stepAnimationTimeSum = 0;
        startedCoroutine = false;
        ContinueAnim();
    }

}
