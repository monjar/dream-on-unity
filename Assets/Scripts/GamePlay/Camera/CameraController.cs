using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;


        private void Update()
        {
            this.transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}