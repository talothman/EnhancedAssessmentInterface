using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public class SelectItem3DTutorial : SelectItem3D
    {
        public Transform lightTranform;
        bool transitioning = false;
        Coroutine sunTransitionCoroutine;
        Dictionary<int, float> answerIndexToFloat;

        protected override void Start()
        {
            base.Start();

            answerIndexToFloat = new Dictionary<int, float>()
            {
                {0, 50f},
                {1, 0f},
                {2, 310f},
                {3, 187f}
            };

            lightTranform = GameObject.FindGameObjectWithTag("light").transform;
        }

        public override void SetSelectedAnswer(GameObject selectedObject)
        {
            base.SetSelectedAnswer(selectedObject);
            float sunAngle;

            for(int i = 0; i < selectAnswers3D.Length; i++)
            {
                if(selectAnswers3D[i].gameObject == selectedObject)
                {
                    answerIndexToFloat.TryGetValue(i, out sunAngle);
                    SetDirectionalLigthAngle(sunAngle);
                    break;
                }
            }
        }

        public void SetDirectionalLigthAngle(float angle)
        {
            if (!transitioning)
                sunTransitionCoroutine = StartCoroutine(LerpSun(angle));
            else
            {
                StopCoroutine(sunTransitionCoroutine);
                transitioning = false;
                sunTransitionCoroutine = StartCoroutine(LerpSun(angle));
            }
        }

        private IEnumerator LerpSun(float targetAngle)
        {
            transitioning = true;

            Vector3 targetRotation = lightTranform.rotation.eulerAngles;
            targetRotation.x = targetAngle;
            Quaternion targetRotationQuat = Quaternion.Euler(targetRotation);

            if (Vector3.Distance(lightTranform.rotation.eulerAngles, targetRotation) > 0.01f)
            {
                while (lightTranform.rotation != targetRotationQuat)
                {
                    lightTranform.transform.rotation = Quaternion.Slerp(lightTranform.transform.rotation, targetRotationQuat, Time.deltaTime);
                    yield return null;
                }
            }
            transitioning = false;
        }

    }
}

