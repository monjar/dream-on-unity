using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float _followCoef = 100;

        private void Update()
        {  
            Vector2 currentPos = transform.position ;
            Vector2 targetPos = target.position;
            transform.position = (Vector3)Vector2.Lerp(currentPos, targetPos, Time.deltaTime * _followCoef) - Vector3.forward;
        }
    }
}