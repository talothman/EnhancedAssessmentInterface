using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public class SortItem3DTutorial : SortItem3D
    {
        public Transform lightTransform;
        bool transitioning = false;
        Coroutine sunTransitionCoroutine;
        Dictionary<int, float> answerIndexToFloat;

        public override void Start()
        {
            base.Start();

            answerIndexToFloat = new Dictionary<int, float>()
            {
                {0, 50f},
                {1, 0f},
                {2, 310f},
                {3, 187f}
            };

            lightTransform = GameObject.FindGameObjectWithTag("light").transform;
        }

        public override void CheckSelectedAnswers()
        {
            StartCoroutine(StaggeredCheckSelectedAnswers());
        }

        IEnumerator StaggeredCheckSelectedAnswers()
        {
            SortAnswer3D[] sortAnswers = sortAnswerGroup3D.threeDSortAnswers;

            bool sorted = true;
            float sunAngle;

            for (int i = 0; i < sortAnswers.Length; i++)
            {
                answerIndexToFloat.TryGetValue(i, out sunAngle);
                SetDirectionalLigthAngle(sunAngle);

                while (transitioning)
                {
                    yield return null;
                }

                if (sortAnswers[i].correctOrder == sortAnswers[i].currentOrder)
                {
                    sortAnswers[i].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    sortAnswers[i].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    sorted = false;
                }
            }

            if (sorted)
            {
                answeredCorreclty = true;
            }
            else
            {
                answeredCorreclty = false;
            }
        }

        public void SetDirectionalLigthAngle(float angle)
        {
            if (!transitioning)
            {
                sunTransitionCoroutine = StartCoroutine(LerpSun(angle));
            }
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

            Vector3 targetRotation = lightTransform.rotation.eulerAngles;
            targetRotation.x = targetAngle;
            Quaternion targetRotationQuat = Quaternion.Euler(targetRotation);

            while (Vector3.Distance(lightTransform.rotation.eulerAngles, targetRotation) > 5f)
            {
                lightTransform.transform.rotation = Quaternion.Slerp(lightTransform.transform.rotation, targetRotationQuat, Time.deltaTime);
                yield return null;
            }

            transitioning = false;
        }
    }
}
