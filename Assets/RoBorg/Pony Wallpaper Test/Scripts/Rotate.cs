using UnityEngine;

namespace RoBorg.PonyWallpaperTest
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField]
        private float xSpeed = 1f;

        [SerializeField]
        private float ySpeed = 1f;

        [SerializeField]
        private float zSpeed = 1f;

        private void Update()
        {
            transform.Rotate(new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime));
        }
    }
}
