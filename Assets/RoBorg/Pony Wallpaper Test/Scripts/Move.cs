using RoBorg.Utils;
using UnityEngine;

namespace RoBorg.PonyWallpaperTest
{
    public class Move : MonoBehaviour
    {
        [SerializeField]
        private float xSpeed = 0;

        [SerializeField]
        private float ySpeed = 0;

        [SerializeField]
        private float zSpeed = 0;

        [SerializeField]
        private float xDistance = 1;

        [SerializeField]
        private float yDistance = 1;

        [SerializeField]
        private float zDistance = 1;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.localPosition;
        }

        private void Update()
        {
            var xPercent = Easing.EaseInOutSine(Mathf.PingPong(Time.time, 1 / xSpeed) * xSpeed);
            var yPercent = Easing.EaseInOutSine(Mathf.PingPong(Time.time, 1 / ySpeed) * ySpeed);
            var zPercent = Easing.EaseInOutSine(Mathf.PingPong(Time.time, 1 / zSpeed) * zSpeed);

            var x = (xPercent - 0.5f) * xDistance;
            var y = (yPercent - 0.5f) * yDistance;
            var z = (zPercent - 0.5f) * zDistance;

            //var y = Easing.EaseInOutSine(Mathf.PingPong(Time.deltaTime, 1) - (yDistance / 2)) * ySpeed);
            //var z = Easing.EaseInOutSine(Mathf.PingPong(Time.deltaTime, 1) - (zDistance / 2)) * zSpeed);

            transform.localPosition = startPosition + new Vector3(x, y, z);
        }
    }
}
