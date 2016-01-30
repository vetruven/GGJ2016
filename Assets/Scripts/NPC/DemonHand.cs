﻿using UnityEngine;
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

    public float delayCycle = 30f;
    [SerializeField]
    private float _handRadius = 100;

    private bool _angry;

    private GameObject _deamonHandObject;

    private int _currentNormalAnimSteps = 1;
    private int _currentAngryAnimSteps = 0;

    private ArrayList _normalAnimSteps;
    private ArrayList _angryAnimSteps;

    private bool _gameOver = false;
    //private bool _virginDied = false;

    void Start()
    {
        _normalAnimSteps = new ArrayList();
        _angryAnimSteps = new ArrayList();
        _deamonHandObject = gameObject;
        _deamonHandObject.transform.position = startPosition.transform.position;

        CreateNormalBehaviour();
        CreateAngryBehaviour();

        ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps]).DoAnim();

    }

    void RegisterHandler()
    {
        //EventBus.VirginDied.AddListener(() =>
        //{
        //    angerBar.VirginEaten(virginEatingCost);
        //    _virginDied = true;
        //});



        EventBus.FinishLevel.AddListener(() =>
        {
            StopAnimationCycle();
        });

        EventBus.DemonAngry.AddListener(() =>
        {
            _angry = true;
        });

    }

    void CreateNormalBehaviour()
    {
        startPosition.DeamonHand = _deamonHandObject;
        startPosition.nextStep = () =>
        {
            ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps++]).DoAnim();
        };
        _normalAnimSteps.Add(startPosition);

        intermediateNormalPositions.DeamonHand = _deamonHandObject;
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
            EventBus.TheHandIsDown.Dispatch(transform.position, _handRadius);
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

        endPosition.DeamonHand = _deamonHandObject;
        endPosition.nextStep = () =>
        {
            EventBus.HandHasGrabbed.Dispatch();
            CheckFinishLevelConditions();
        };
        _normalAnimSteps.Add(endPosition);

    }
    void CreateAngryBehaviour()
    {
        intermediateAngryPositions.DeamonHand = _deamonHandObject;
        intermediateAngryPositions.nextStep = () =>
            {
                AnimStep nextAnimation;
                AnimStep currentAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps - 1];

                nextAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps++];
                currentAnimation.ResetAnimationStepCounters();
                nextAnimation.DoAnim();
            };
        _angryAnimSteps.Add(intermediateAngryPositions);

        ((AnimStep)_angryAnimSteps[_angryAnimSteps.Count - 1]).nextStep = () =>
        {
            _angry = false;
            _currentAngryAnimSteps = 0;
            CheckFinishLevelConditions();
        };
    }

    //public void AddVirgin(Transform virginDeathDelegate)
    //{
    //    _virginsCallbacks.Add(virginDeathDelegate);
    //}

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
        yield return new WaitForSeconds(delayCycle);
        _currentNormalAnimSteps = 0;
        ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps]).DoAnim();
    }

}
