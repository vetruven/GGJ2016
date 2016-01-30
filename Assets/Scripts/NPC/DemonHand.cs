using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DemonHand : MonoBehaviour
{

    //public AngerBar angerBar;

    #region positions
    public AnimStep startPosition;
    public AnimStep intermediateNormalPositions;
    public AnimStep intermediateAngryPositions;
    public AnimStep endPosition;
    #endregion

    [Range(0.5f, 1.5f)]
    public float timeToMoveToNextStep = 0.75f;

    public bool _angry = false;
    public float delayCycle = 30f;
    [SerializeField]
    private float _handRadius = 100;

    [SerializeField]
    private Transform _pentragramLocation;
    private GameObject _deamonHandObject;

    private int _currentNormalAnimSteps = 1;
    private int _currentAngryAnimSteps = 0;

    private ArrayList _normalAnimSteps;
    private ArrayList _angryAnimSteps;
    private bool _gameOver = false;

    void Start()
    {
        _normalAnimSteps = new ArrayList();
        _angryAnimSteps = new ArrayList();
        _deamonHandObject = gameObject;
        CreateNormalBehaviour();
        CreateAngryBehaviour();
        RegisterHandler();
    }

    void RegisterHandler()
    {
        EventBus.StartGame.AddListener(() =>
        {
            Debug.Log("Start game event received");
            ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps]).DoAnim();
        });

        EventBus.EndGame.AddListener(() =>
        {
            StopAnimationCycle();
        });

        EventBus.DemonAngry.AddListener(() =>
        {
            Debug.Log("Demon angry event received");
            _angry = true;
        });

    }

    void CreateNormalBehaviour()
    {
        startPosition.DemonHand = _deamonHandObject;
        startPosition.nextStep = () =>
        {
            ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps++]).DoAnim();
        };
        _normalAnimSteps.Add(startPosition);

        intermediateNormalPositions.DemonHand = _deamonHandObject;
        intermediateNormalPositions.nextStep = () =>
        {
            AnimStep currentAnimation = (AnimStep)_normalAnimSteps[_currentNormalAnimSteps - 1];
            AnimStep nextAnimation;

            if (_angry)
            {
                currentAnimation.ClearAnimation();
                nextAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps++];
            }
            else
            {
                nextAnimation = (AnimStep)_normalAnimSteps[_currentNormalAnimSteps++];
                _currentAngryAnimSteps = 0;
            }

            currentAnimation.ResetAnimationStepCounters();
            EventBus.TheHandIsDown.Dispatch(_pentragramLocation.position, _handRadius);
            nextAnimation.DoAnim();
        };

        intermediateNormalPositions.onUpdate = () =>
        {
            if (_angry)
            {
                intermediateNormalPositions.ClearAnimation();
                AnimStep nextAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps++];
                nextAnimation.DoAnim();
            }
        };

        _normalAnimSteps.Add(intermediateNormalPositions);

        endPosition.DemonHand = _deamonHandObject;
        endPosition.nextStep = () =>
        {
            EventBus.HandHasGrabbed.Dispatch();
            Debug.Log("At last position");
            _angry = false;
            _currentAngryAnimSteps = 0;
            CheckFinishLevelConditions();
        };
        _normalAnimSteps.Add(endPosition);

    }
    void CreateAngryBehaviour()
    {
        intermediateAngryPositions.DemonHand = _deamonHandObject;
        intermediateAngryPositions.nextStep = () =>
            {
                AnimStep nextAnimation;
                AnimStep currentAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps - 1];

                nextAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps++];
                currentAnimation.ResetAnimationStepCounters();
                nextAnimation.DoAnim();
            };
        _angryAnimSteps.Add(intermediateAngryPositions);
        _angryAnimSteps.Add(endPosition);
    }

    public void StopAnimationCycle()
    {
        ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps]).ClearAnimation();
        ((AnimStep)_angryAnimSteps[_currentAngryAnimSteps]).ClearAnimation();
    }

    public int NumberOfEatensVirgins()
    {
        return 1;
    }

    void CheckFinishLevelConditions()
    {
        if (!_gameOver)
        {
            StartCoroutine("WaitBeforeNextCycle");
        }
    }

    IEnumerator WaitBeforeNextCycle()
    {
        float delaySum = 0;

        while (delaySum < delayCycle)
        {
            if (!_angry)
            {
                yield return new WaitForSeconds(1f);
                delaySum += 1;
            }
            else
            {
                Debug.Log("Breaking on angry");
                break;
            }
        }

        _currentNormalAnimSteps = 0;
        _currentAngryAnimSteps = 0;
        if (_angry)
        {
            Debug.Log("Angry, sending angry grab");
            ((AnimStep)_angryAnimSteps[_currentAngryAnimSteps++]).DoAnim();
        }
        else
        {
            ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps++]).DoAnim();
        }


    }

}
