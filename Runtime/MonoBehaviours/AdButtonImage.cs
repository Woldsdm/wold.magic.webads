using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

            StartCoroutine(HandleLaunch());
        }

        IEnumerator HandleLaunch()
        {
            yield return null;

            id = adView.AddButton(rectTransform, sprite);

            if (activationDelay >= 1f)
            {
                SetButtonActive(false);
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
                SetButtonActive(true);
            }
        }

        public void SetButtonActive(bool active) => adView.SetButtonActive(id, active);

        void NextStep()
        {
            if (currentStep >= steps.Count || steps.Count == 0)
                return;

            StartCoroutine(HandleStep(steps[currentStep]));
            currentStep++;
        }

        IEnumerator HandleStep(AdButtonStep step)
        {
            yield return null;

            if (step.activationDelay >= 1f) SetButtonActive(false);

            if (step.sprite != null) adView.UpdateButton(id, step.sprite);

            switch (step.action)
            {
                case AdButtonAction.LoadNextAd:
                    adView.Load();
                    break;
                case AdButtonAction.CloseAd:
                    adView.Close();
                    break;
                case AdButtonAction.DisableButton:
                    SetButtonActive(false);
                    break;
                default:
                    break;
            }

            step.onStepClicked.Invoke();

            if (step.activationDelay >= 1f)
            {
                yield return new WaitForSeconds(step.activationDelay);
                SetButtonActive(true);
            }
        }

        IEnumerator ActivateButtonAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SetButtonActive(true);
        }
    }

    public enum AdButtonAction
    {
        None,
        LoadNextAd,
        CloseAd,
        DisableButton
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