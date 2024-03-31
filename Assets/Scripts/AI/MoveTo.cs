using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveTo : MonoBehaviour
{
    public GameObject Goal;
    
    //DI-REFACTOR - это надо будет в будущем вынести
    public Transform FollowedTransform;
    public float Speed = 0.05f;
    
    [Range(1,5)]public int TimeToCheckGoal;

    private Vector3 _direction;
    private bool _stepCoroutineLaunch;

    #region Временные переменные для редактирования

        private Transform _tempTransform;
        private Vector3 _tempPosition;

    #endregion

    private IEnumerator _makeStepRoutine;
    
    private void Start()
    {
        _tempTransform = FollowedTransform;
        _tempPosition = FollowedTransform.position;
        //_direction = Goal.transform.position - _tempPosition;

        /*position = position + _direction;
        transform1.position = position;*/

        /*position.y = WorldConstants.NPCDownPointShift;
        transform1.position = position;*/
        Debug.Log("Direction: "+_direction);
        _makeStepRoutine = MakeStep();
        StartCoroutine(CheckGoal(TimeToCheckGoal));
        StartCoroutine(_makeStepRoutine);
        _stepCoroutineLaunch = true;
    }

    private void OnDestroy()
    {
        //StopCoroutine(CheckGoal(TimeToCheckGoal));
        //StopCoroutine(MakeStep(FramesWaitForStep));
    }

    private void LateUpdate()
    {
        if (_direction.magnitude > 2f)
        {
            if (!_stepCoroutineLaunch)
            {
                StartCoroutine(_makeStepRoutine);
                _stepCoroutineLaunch = true;
            }
            //_direction = Goal.transform.position - _tempPosition;
            //Move();
        }
        else
        {
            StopCoroutine(_makeStepRoutine);
            _stepCoroutineLaunch = false;
            /*if (!_stepCoroutineLaunch)
            {
                StartCoroutine(MakeStep(FramesWaitForStep));
                _stepCoroutineLaunch = true;
            }*/
        }

        //_tempPosition = _tempPosition + _direction;
        //_tempTransform.position = _tempPosition;
    }

    private void Move()
    {
        //_direction = Goal.transform.position - _tempPosition;
        Vector3 velocity = _direction.normalized * Speed;
        FollowedTransform.position = FollowedTransform.position + velocity * Time.deltaTime;

        #region Корректировка по константе

        _tempPosition = FollowedTransform.position;
        _tempPosition.y = WorldConstants.NPCDownPointShift;
        FollowedTransform.position = _tempPosition;

        #endregion
    }

    private bool isSee()
    {
        if (Goal != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator CheckGoal(int time)
    {
        while (isSee())
        {
            _direction = Goal.transform.position - _tempPosition;
            yield return new WaitForSeconds(time);
        }
    }

    private IEnumerator MakeStep()
    {
        while (isSee())
        {

                yield return null;
                yield return null;
                yield return null;
                Move();
        }
    }

    private void Update()
    {
        
    }
}


public static class WorldConstants
{
    public const float NPCDownPointShift = 0.8f;
}


public class SimpleRotateSystem : IRotateSystem
{
    private float _speed;
    private float _permissibleAngleDeviation;
    private BlackBoard _blackBoard;

    public SimpleRotateSystem(float speed, float angleDeviation)
    {
        _speed = speed;
        _permissibleAngleDeviation = angleDeviation;
    }
}

public class OptimazedRotateSystem : IRotateSystem
{
    private float _speed;
    private float _permissibleAngleDeviation;
    private BlackBoard _blackBoard;
    
    public OptimazedRotateSystem(float speed, float angleDeviation)
    {
        _speed = speed;
        _permissibleAngleDeviation = angleDeviation;
    }
}

public interface IMoveTo
{
    
}


public interface IRotateSystem
{
    
}

public interface IGridCreater
{
    
}

class SimpleGridCreater : IGridCreater
{
}

class OptimazedGridCreater : IGridCreater
{
}

public class BlackBoard
{
    public Transform GoalObject;
    
    public class VectorData
    {
        private Vector3 _direction;
    }
    
    
    //Здесь надо хранить направление
}