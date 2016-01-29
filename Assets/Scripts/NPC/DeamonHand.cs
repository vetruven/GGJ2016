using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DeamonHand : MonoBehaviour
{


    //public GameObject deamonGameObject;
    public AnimStep startPosition;

    //public Transform intermediatePosition;
    public AnimStep[] intermediateNormalPositions;
    public AnimStep[] intermediateAngryPositions;

    public AnimStep endPosition;

    [Range(0.5f, 1.5f)]
    public float timeToMoveToNextStep = 0.75f;

    public float pauseEverySeconds = 0.3f;

    public float animationPauseForSeconds;

    public bool Angry;

    public GameObject deamonHandObject;
    public ArrayList virginsCallbacks;

    //The hand is already at the index=0 position
    public int currentNormalAnimSteps = 1;
    public int currentAngryAnimSteps = 0;

    private ArrayList normalAnimSteps;
    private ArrayList angryAnimSteps;


    void Start()
    {

        normalAnimSteps = new ArrayList();
        angryAnimSteps = new ArrayList();
        deamonHandObject = gameObject;
        deamonHandObject.transform.position = startPosition.transform.position;

        CreateNormalBehaviour();
        CreateAngryBehaviour();

        ((AnimStep)normalAnimSteps[currentNormalAnimSteps]).DoAnim();

    }

    void CreateNormalBehaviour()
    {
        startPosition.DeamonHand = deamonHandObject;
        startPosition.nextStep = () =>
        {
            ((AnimStep)normalAnimSteps[currentNormalAnimSteps++]).DoAnim();
        };
        normalAnimSteps.Add(startPosition);

        foreach (AnimStep interPosition in intermediateNormalPositions)
        {
            interPosition.DeamonHand = deamonHandObject;
            interPosition.nextStep = () =>
            {
                AnimStep currentAnimation = (AnimStep)normalAnimSteps[currentNormalAnimSteps - 1];
                AnimStep nextAnimation;

                if (Angry)
                {
                    currentAnimation.ClearAnimation();
                    nextAnimation = (AnimStep)angryAnimSteps[currentAngryAnimSteps++];
                }
                else
                {
                    nextAnimation = (AnimStep)normalAnimSteps[currentNormalAnimSteps++];
                    currentAngryAnimSteps = 0;
                }

                currentAnimation.ResetAnimationStepCounters();
                nextAnimation.DoAnim();
            };

            interPosition.onUpdate = () =>
            {
                if (Angry)
                {
                    interPosition.ClearAnimation();
                    AnimStep nextAnimation = (AnimStep)angryAnimSteps[currentAngryAnimSteps++];
                    nextAnimation.DoAnim();
                }
            };

            normalAnimSteps.Add(interPosition);
        }

        endPosition.DeamonHand = deamonHandObject;
        endPosition.nextStep = () =>
        {
            currentNormalAnimSteps = 0;
            ((AnimStep)normalAnimSteps[currentNormalAnimSteps]).DoAnim();
        };
        normalAnimSteps.Add(endPosition);

    }
    void CreateAngryBehaviour()
    {
        foreach (AnimStep interPosition in intermediateAngryPositions)
        {
            interPosition.DeamonHand = deamonHandObject;
            interPosition.nextStep = () =>
            {
                AnimStep nextAnimation;
                AnimStep currentAnimation = (AnimStep)angryAnimSteps[currentAngryAnimSteps - 1];

                nextAnimation = (AnimStep)angryAnimSteps[currentAngryAnimSteps++];
                currentAnimation.ResetAnimationStepCounters();
                nextAnimation.DoAnim();
            };
            angryAnimSteps.Add(interPosition);
        }

        ((AnimStep)angryAnimSteps[angryAnimSteps.Count - 1]).nextStep = () =>
        {
            currentAngryAnimSteps = 0;
            currentNormalAnimSteps = normalAnimSteps.Count - 1;
            Angry = false;
            ((AnimStep)normalAnimSteps[currentNormalAnimSteps++]).DoAnim();
        };
    }


    public void AddVirgin(Transform virginDeathDelegate)
    {
        virginsCallbacks.Add(virginDeathDelegate);
    }

    public void SetGameOver()
    {
        ((AnimStep)normalAnimSteps[currentNormalAnimSteps]).ClearAnimation();
        ((AnimStep)angryAnimSteps[currentAngryAnimSteps]).ClearAnimation();
    }

    public int NumberOfEatensVirgins()
    {
        return 1;
    }

    /*
    DeamonHand will controll the deamon hand itself
    controll the movement from the start to the intermediate position 
    once the intermediate position is reached then the animation should go to the end poistion in a regular time 

*/


}
