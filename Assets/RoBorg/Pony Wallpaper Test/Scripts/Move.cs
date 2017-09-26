using RoBorg.Utils;
using UnityEngine;

namespace RoBorg.PonyWallpaperTest
{
    public class Move : MonoBehaviour
    {
        public float xSpeed = 0;

        public float ySpeed = 0;

        public float zSpeed = 0;

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
            var xPercent = xSpeed == 0 ? 0.5f : Easing.EaseInOutSine(Mathf.PingPong(Time.time, 1 / xSpeed) * xSpeed);
            var yPercent = ySpeed == 0 ? 0.5f : Easing.EaseInOutSine(Mathf.PingPong(Time.time, 1 / ySpeed) * ySpeed);
            var zPercent = zSpeed == 0 ? 0.5f : Easing.EaseInOutSine(Mathf.PingPong(Time.time, 1 / zSpeed) * zSpeed);

            var x = (xPercent - 0.5f) * xDistance;
            var y = (yPercent - 0.5f) * yDistance;
            var z = (zPercent - 0.5f) * zDistance;

            transform.localPosition = startPosition + new Vector3(x, y, z);
        }
    }
}
