using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DemonHand : MonoBehaviour
{
    enum AnimationPosition
    {
        START,
        GRAB,
        END
    }

    #region positions
    public AnimStep startPosition;
    public AnimStep intermediateNormalPositions;
    public AnimStep intermediateAngryPositions;
    public AnimStep endPosition;
    #endregion

    [Range(0.5f, 1.5f)]
    public float timeToMoveToNextStep = 0.75f;

    public bool _angry = false;
    public float delayCycle = 10f;
    [SerializeField]
    private float _handRadius = 100;

    [SerializeField]
    private Transform _pentragramLocation;

    [SerializeField]
    private List<GameObject> _nailPositionList;
    [SerializeField]
    private ParticleSystem _dragParticle;

    private AnimationPosition _animationPosition;

    private GameObject _deamonHandObject;

    private int _currentNormalAnimSteps = 1;
    private int _currentAngryAnimSteps = 0;

    private ArrayList _normalAnimSteps;
    private ArrayList _angryAnimSteps;
    private bool _gameOver = false;

    private int _totalDied = 0;
    private bool _barIsEmpty = false;

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

        EventBus.BarEmpty.AddListener(() =>
        {
            _barIsEmpty = true;
        });

        EventBus.StartGame.AddListener(() =>
        {
            _totalDied = 0;
            _barIsEmpty = false;
            _gameOver = false;
            _currentNormalAnimSteps = 1;
            _currentAngryAnimSteps = 0;
            ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps++]).DoAnim();
        });

        EventBus.EndGame.AddListener(() =>
        {
            StopAnimationCycle();
        });

        EventBus.DemonAngry.AddListener(() =>
        {
            _angry = true;
        });

        EventBus.VirginDied.AddListener((pos) =>
        {
            var ps = Instantiate(_dragParticle);
            ps.transform.position = pos;
            ps.transform.SetParent(transform);

        });

        EventBus.TotalVirginsDied.AddListener((numOfVirgins) =>
        {
            _totalDied = numOfVirgins;
            CheckFinishLevelConditions();
            _totalDied = 0;
        });

    }

    void CreateNormalBehaviour()
    {
        startPosition.DemonHand = _deamonHandObject;
        startPosition.nextStep = () =>
        {
            ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps++]).DoAnim();
        };

        intermediateNormalPositions.DemonHand = _deamonHandObject;
        intermediateNormalPositions.nextStep = () =>
        {
            AnimStep currentAnimation = (AnimStep)_normalAnimSteps[_currentNormalAnimSteps - 1];
            AnimStep nextAnimation;

            //if (_angry)
            //{
            //    currentAnimation.ClearAnimation();
            //    nextAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps++];
            //}
            //else
            //{
            nextAnimation = (AnimStep)_normalAnimSteps[_currentNormalAnimSteps++];
            _currentAngryAnimSteps = 0;
            //}
            currentAnimation.ResetAnimationStepCounters();
            EventBus.TheHandIsDown.Dispatch(_pentragramLocation.position, _handRadius);
            nextAnimation.DoAnim();
        };

        //intermediateNormalPositions.customOnUpdate = () =>
        //{
        //    if (_angry)
        //    {
        //        intermediateNormalPositions.ClearAnimation();
        //        AnimStep nextAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps++];
        //        nextAnimation.DoAnim();
        //    }
        //};


        endPosition.DemonHand = _deamonHandObject;
        endPosition.nextStep = () =>
        {
            EventBus.HandHasGrabbed.Dispatch();
            _currentAngryAnimSteps = 0;
            _currentNormalAnimSteps = 0;
            if (!_gameOver)
            {
                StartCoroutine("WaitBeforeNextCycle");
            }
            CheckFinishLevelConditions();

        };


        _normalAnimSteps.Add(startPosition);
        _normalAnimSteps.Add(intermediateNormalPositions);
        _normalAnimSteps.Add(endPosition);

    }
    void CreateAngryBehaviour()
    {
        intermediateAngryPositions.DemonHand = _deamonHandObject;
        intermediateAngryPositions.nextStep = () =>
            {
                AnimStep nextAnimation;
                AnimStep currentAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps - 1];
                EventBus.TheHandIsDown.Dispatch(_pentragramLocation.position, _handRadius);

                currentAnimation.ResetAnimationStepCounters();
                nextAnimation = (AnimStep)_angryAnimSteps[_currentAngryAnimSteps++];
                nextAnimation.DoAnim();
            };
        _angryAnimSteps.Add(intermediateAngryPositions);
        _angryAnimSteps.Add(endPosition);
    }

    public void StopAnimationCycle()
    {

        for (int i = 0; i< _normalAnimSteps.Count; i++)
        {
            ((AnimStep)_normalAnimSteps[i]).ClearAnimation();
        }

        for (int i = 0; i < _angryAnimSteps.Count; i++)
        {
            ((AnimStep)_angryAnimSteps[i]).ClearAnimation();

        }


        //if (_currentNormalAnimSteps >= _normalAnimSteps.Count)
        //    _currentNormalAnimSteps = _normalAnimSteps.Count - 1;

        //((AnimStep)_normalAnimSteps[--_currentNormalAnimSteps]).ClearAnimation();

        //if (_currentAngryAnimSteps == _angryAnimSteps.Count)
        //    _currentAngryAnimSteps = _angryAnimSteps.Count - 1;

        //((AnimStep)_angryAnimSteps[--_currentAngryAnimSteps]).ClearAnimation();
    }

    void CheckFinishLevelConditions()
    {
        if (_barIsEmpty && _totalDied == 0)
        {
            EventBus.GameLost.Dispatch();
            _gameOver = true;
        }
    }

    IEnumerator WaitBeforeNextCycle()
    {
        float delaySum = 0;

        while (!_gameOver && delaySum < delayCycle)
        {
            if (!_angry)
            {
                yield return new WaitForSeconds(1f);
                delaySum += 1;
            }
            else
            {
                break;
            }
        }
        if (!_gameOver)
        {
            _currentNormalAnimSteps = 0;
            _currentAngryAnimSteps = 0;
            if (_angry)
            {
                ((AnimStep)_angryAnimSteps[_currentAngryAnimSteps++]).DoAnim();
            }
            else
            {
                ((AnimStep)_normalAnimSteps[_currentNormalAnimSteps++]).DoAnim();
            }
        }


    }

}
