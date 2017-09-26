using UnityEngine;

namespace RoBorg.PonyWallpaperTest
{
    public class Rotate : MonoBehaviour
    {
        public float xSpeed = 1f;

        public float ySpeed = 1f;

        public float zSpeed = 1f;

        private void Update()
        {
            transform.Rotate(new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime));
        }
    }
}
