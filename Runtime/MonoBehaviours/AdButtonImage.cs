using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MagicWebAds
{
    [AddComponentMenu("Magic WebAds/Ad Button Image")]
    public class AdButtonImage : Image
    {
        int id;
        UIAdView adView;

        [SerializeField]
        float activationDelay = 0f;

        [SerializeField]
        UnityEvent onClicked = new();

        public UnityEvent OnClicked => onClicked;

        [SerializeField]
        List<AdButtonStep> steps = new();
        int currentStep = 0;

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            color = Color.white;
            sprite = null;
            type = Type.Simple;
            material = null;
            raycastTarget = false;
            preserveAspect = false;
            fillCenter = true;
            fillMethod = FillMethod.Horizontal;
            fillAmount = 1f;
            fillClockwise = true;
            fillOrigin = 0;
        }
#endif
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);

            if (Application.isPlaying) vh.Clear();
        }

        protected override void Awake()
        {
            base.Awake();
            onClicked.AddListener(NextStep);
        }

        public void Launch(UIAdView adView)
        {
            this.adView = adView;

            id = adView.AddButton(rectTransform, sprite);

            if(activationDelay >= 1f)
            {
                adView.SetButtonActive(id, false);
            }
        }

        public void Show()
        {
            if (activationDelay >= 1f)
            {
                StartCoroutine(ActivateButtonAfterDelay(activationDelay));
            }
            else
            {
                adView.SetButtonActive(id, true);
            }
        }

        public void NextStep()
        {
            if (currentStep >= steps.Count || steps.Count == 0)
                return;

            var step = steps[currentStep];

            if (step.sprite) adView.UpdateButton(id, rectTransform, step.sprite);

            if (step.activationDelay >= 1f)
            {
                adView.SetButtonActive(id, false);
                StartCoroutine(ActivateButtonAfterDelay(step.activationDelay));
            }

            switch (step.action)
            {
                case AdButtonAction.Close:
                    adView.Close();
                    break;
                case AdButtonAction.LoadNext:
                    adView.Load();
                    break;
                default:
                    break;
            }

            step.onStepClicked.Invoke();

            currentStep++;
        }

        IEnumerator ActivateButtonAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            adView.SetButtonActive(id, true);
        }
    }

    public enum AdButtonAction
    {
        None,
        LoadNext,
        Close
    }

    [Serializable]
    public class AdButtonStep
    {
        public Sprite sprite;
        public float activationDelay = 0f;
        public AdButtonAction action;
        public UnityEvent onStepClicked = new();
    }
}