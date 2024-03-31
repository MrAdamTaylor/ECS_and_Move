using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum RotateDimensionType
{
    TwoDimensionRotate,
    ThirdDimensionRotate
}

public class RotateSystem : MonoBehaviour
{
    public Transform Body;
    public Transform LookedObject;
    [FormerlySerializedAs("DimensionType")] public RotateDimensionType _dimensionDimensionType;
    [Range(0,10)]public float RotateSpeed = 1.5f;
    public bool IsOptimaze = false;

    private bool _autoRotate = false;
    private bool _coroutineRotate = false;

    private IEnumerator _rotateCoroutine;
    
    void Start()
    {
        _rotateCoroutine = Rotate();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //CalculateDistance();
            CalculateAngle();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _autoRotate = !_autoRotate;
            //AutoRotate();
        }

        if (_autoRotate)
        {
            if (IsOptimaze)
            {
                OptimizeAutoRotate();
            }
            else
            {
                AutoRotate();
            }

        }
        else
        {
            if (IsOptimaze)
            {
                StopCoroutine(_rotateCoroutine);
                _coroutineRotate = false;
            }
        }
    }

    private void OptimizeAutoRotate()
    {
        if (_coroutineRotate == false)
        {
            StartCoroutine(_rotateCoroutine);
            _coroutineRotate = true;
        }

    }

    private IEnumerator Rotate()
    {
        while (_autoRotate)
        {
            yield return null;
            yield return null;
            yield return null;
            CalculateAngle(true);
        }
    }

    public void AutoRotate()
    {
        CalculateAngle(true);
    }

    private void CalculateAngle(bool makeRotate = false)
    {
        var transform1 = Body.transform;
        Vector3 forwardDirection = transform1.forward;
        Vector3 goalDirection = LookedObject.transform.position - transform1.position;
        goalDirection = goalDirection.normalized;
        DebugRays(forwardDirection, goalDirection);
        float dotXZ = GeometryMath.DotProductXZ(forwardDirection, goalDirection);
        forwardDirection = forwardDirection.ExcludeY();
        goalDirection = goalDirection.ExcludeY();
        float angleXY = Mathf.Acos(dotXZ / (forwardDirection.magnitude * goalDirection.magnitude));

        int clockwise = -1;
        if (GeometryMath.CrossProduct(forwardDirection, goalDirection).y < 0)
            clockwise = 1;

        if (makeRotate)
        {
            if((angleXY * Mathf.Rad2Deg) > 10)
                Body.transform.Rotate(0,angleXY * Mathf.Rad2Deg * clockwise * RotateSpeed *Time.deltaTime,0);
        }
    }

    private void DebugRays(Vector3 forwardDirection, Vector3 goalDirection)
    {
        Debug.DrawRay(Body.transform.position, forwardDirection, Color.green, 3);
        Debug.DrawRay(Body.transform.position, goalDirection, Color.blue, 3);
    }

    /*float CalculateDistance()
    {
        float distance = 0f;
        if (_dimensionDimensionType == RotateDimensionType.TwoDimensionRotate)
        {
            distance = Mathf.Sqrt(Mathf.Pow(GeometryMath.GetHypotenuseX(LookedObject,Body),2) +
                                        Mathf.Pow(GeometryMath.GetHypotenuseZ(LookedObject,Body),2));
            Debug.Log("Distance Between: "+distance);
        }
        else if (_dimensionDimensionType == RotateDimensionType.ThirdDimensionRotate)
        {
            distance = Mathf.Sqrt(Mathf.Pow(GeometryMath.GetHypotenuseX(LookedObject,Body),2) + 
                                        Mathf.Pow(GeometryMath.GetHypotenuseY(LookedObject,Body),2)+
                                        Mathf.Pow(GeometryMath.GetHypotenuseZ(LookedObject,Body),2));
            Debug.Log("Distance Between: "+distance);
        }

        return distance;
    }*/
}