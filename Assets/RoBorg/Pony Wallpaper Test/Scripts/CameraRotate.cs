using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoBorg.PonyWallpaperTest
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;

        private void Start()
        {

        }

        private void Update()
        {
            transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}
