//using RoBorg.Pegasi.HelperExtensions;
using RoBorg.Pegasi.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RoBorg.Pegasi
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pegasus : MonoBehaviour
    {
        private Transform leftWingTarget;
        private Transform rightWingTarget;
        private Transform tailTarget;

        private new Rigidbody rigidbody;
        private float wingZSpeed;
        private Vector2 eyePosition = new Vector2(0.5f, 0.5f);
        private Vector2 eyeTarget = new Vector2(0.5f, 0.5f);
        private Random.RandomGeneratorInterface eyeRandom = new Random.RandomGaussian(0.1f, 0.5f);

        private Renderer body;
        private int leftEyeMaterialIndex;
        private int rightEyeMaterialIndex;
        private Vector2 leftEyeZero;
        private Vector2 rightEyeZero;

        private float maxVeloxity = 200; // Rainboom velocity

        public float flappingSpeed = 200;
        private bool gliding = false;
        private bool inGlide = false;
        private RandomGeneratorInterface random = new RandomLinear(0, 1);

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            leftWingTarget = transform.FindInChildren("Left Wing Target");
            rightWingTarget = transform.FindInChildren("Right Wing Target");
            tailTarget = transform.FindInChildren("Tail Target");

            var found = false;

            foreach (var renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                for (var i = 0; i < renderer.sharedMaterials.Length; i++)
                {
                    var material = renderer.sharedMaterials[i];

                    if (material.name == "eyeball_l")
                    {
                        leftEyeMaterialIndex = i;
                        found = true;
                    }
                    else if (material.name == "eyeball_r")
                    {
                        rightEyeMaterialIndex = i;
                        found = true;
                    }
                }

                if (found)
                {
                    body = renderer;
                    break;
                }
            }

            Assert.IsTrue(found, "Body / eyes not found on " + name);
            Assert.IsNotNull(rigidbody, "Rigidbody not found on " + name);
            Assert.IsNotNull(leftWingTarget, "Left Wing Target not found on " + name);
            Assert.IsNotNull(rightWingTarget, "Right Wing Target found set on " + name);
            Assert.IsNotNull(tailTarget, "Tail Target not found on " + name);
            Assert.IsNotNull(body, "Body not found on " + name);

            leftEyeZero = body.sharedMaterials[leftEyeMaterialIndex].GetTextureOffset("_MainTex");
            rightEyeZero = body.sharedMaterials[rightEyeMaterialIndex].GetTextureOffset("_MainTex");

            Destroy(leftWingTarget.parent.gameObject.GetComponent<PonyWallpaperTest.Rotate>());
            Destroy(rightWingTarget.parent.gameObject.GetComponent<PonyWallpaperTest.Rotate>());

            // Start flapping at a random offset
            leftWingTarget.parent.transform.Rotate(new Vector3(0, 0, 360 * UnityEngine.Random.value));

            random.SetSeed(UnityEngine.Random.value);
        }

        private void OnEnable()
        {
            gliding = false;
            inGlide = false;
        }

        private void Update()
        {
            var vPercent = 0.5f;// Mathf.Clamp01(0.2f + (rigidbody.velocity.magnitude / maxVeloxity));

            if (!gliding && (random.Get() < 0.001f))
            {
                gliding = true;
                inGlide = false;
                StartCoroutine(StopGliding(2 + (random.Get() * 5)));
            }

            if (gliding)
            {
                Glide(vPercent);
            }
            else
            {
                Flap(vPercent);
            }

            MoveEyes();
        }

        private void Flap(float vPercent)
        {
            leftWingTarget.parent.transform.Rotate(new Vector3(0, 0, flappingSpeed * 2 * vPercent * Time.deltaTime));

            var leftWingRotation = leftWingTarget.parent.transform.localRotation.eulerAngles;
            rightWingTarget.parent.transform.localRotation = Quaternion.Euler(leftWingRotation.x, leftWingRotation.y, -leftWingRotation.z);
        }

        private void Glide(float vPercent)
        {
            var targetZRotation = 200;
            var targetRotation = Quaternion.Euler(0, 0, targetZRotation);
            var leftWingRotationAngles = leftWingTarget.parent.transform.localRotation.eulerAngles;

            // Only enter glide on downward flap (and not once we're past horizontal)
            if (!inGlide && ((leftWingRotationAngles.z > targetZRotation) || (leftWingRotationAngles.z < targetZRotation - 90)))
            {
                Flap(vPercent);
                return;
            }

            inGlide = true;
            leftWingTarget.parent.transform.localRotation = Quaternion.Lerp(leftWingTarget.parent.transform.localRotation, targetRotation, 0.05f);

            leftWingRotationAngles = leftWingTarget.parent.transform.localRotation.eulerAngles;
            rightWingTarget.parent.transform.localRotation = Quaternion.Euler(leftWingRotationAngles.x, leftWingRotationAngles.y, -leftWingRotationAngles.z);
        }

        private IEnumerator StopGliding(float delay)
        {
            yield return new WaitForSeconds(delay);

            gliding = false;
        }

        private void MoveEyes()
        {
            var leftEye = body.materials[leftEyeMaterialIndex];
            var rightEye = body.materials[rightEyeMaterialIndex];

            if (random.Get() < 0.02f)
            {
                eyeTarget = new Vector2(Mathf.Clamp01(eyeRandom.Get()), Mathf.Clamp01(eyeRandom.Get()));
            }

            eyePosition = Vector2.Lerp(eyePosition, eyeTarget, 0.5f);

            var rightEyeOffset = rightEyeZero - leftEyeZero;
            var horizontalRange = 0.3f;
            var verticalRange = 0.35f;

            var xPosition = (eyePosition.x * horizontalRange) + (leftEyeZero.x - (horizontalRange / 2));
            var yPosition = (eyePosition.y * verticalRange) + (leftEyeZero.y - (verticalRange / 2));

            var leftPosition = new Vector2(xPosition, yPosition);
            var rightPosition = new Vector2(xPosition + rightEyeOffset.x, yPosition + rightEyeOffset.y);

            leftEye.SetTextureOffset("_MainTex", leftPosition);
            leftEye.SetTextureOffset("_DetailAlbedoMap", leftPosition);

            rightEye.SetTextureOffset("_MainTex", rightPosition);
            rightEye.SetTextureOffset("_DetailAlbedoMap", rightPosition);
        }
    }
}
