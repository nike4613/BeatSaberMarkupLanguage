﻿using BeatSaberMarkupLanguage.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Tags
{
    public class ScrollViewTag : BSMLTag
    {
        public override string[] Aliases { get; } = new[] { "scroll-view", "scroll" };

        public override GameObject CreateObject(Transform parent)
        {
            GameObject go = new GameObject("BSMLScrollView");
            go.SetActive(false);

            RectTransform transform = go.AddComponent<RectTransform>();

            GameObject vpgo = new GameObject("Viewport");
            RectTransform viewport = vpgo.AddComponent<RectTransform>();
            viewport.SetParent(transform, false);
            viewport.localPosition = Vector2.zero;
            viewport.anchorMin = Vector2.zero;
            viewport.anchorMax = Vector2.one;
            viewport.anchoredPosition = Vector2.zero;
            viewport.sizeDelta = Vector2.zero;

            Mask vpmask = vpgo.AddComponent<Mask>();
            Image vpimage = vpgo.AddComponent<Image>(); // a Mask needs an Image to work
            vpmask.showMaskGraphic = false;
            vpimage.color = Color.white;
            vpimage.sprite = Utilities.ImageResources.WhitePixel;
            vpimage.material = Utilities.ImageResources.NoGlowMat;

            GameObject contentgo = new GameObject("Content Wrapper");
            RectTransform content = contentgo.AddComponent<RectTransform>();
            content.SetParent(viewport, false);
            content.localPosition = Vector2.zero;
            content.anchorMin = new Vector2(0f, 1f);
            content.anchorMax = new Vector2(1f, 1f);
            content.anchoredPosition = Vector2.zero;

            var contentLayout = contentgo.AddComponent<VerticalLayoutGroup>();
            var contentFitter = contentgo.AddComponent<ContentSizeFitter>();
            contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentLayout.childControlHeight = true;
            contentLayout.childControlWidth = false;
            contentLayout.childForceExpandHeight = false;
            contentLayout.childForceExpandWidth = true;
            contentLayout.childAlignment = TextAnchor.UpperCenter;
            contentLayout.spacing = 0f;

            BSMLScrollViewElement scrollView = go.AddComponent<BSMLScrollViewElement>();
            scrollView.ContentRect = content;
            scrollView.Viewport = viewport;

            BSMLScrollViewContent contentType = contentgo.AddComponent<BSMLScrollViewContent>();
            contentType.ScrollView = scrollView;

            go.SetActive(true);
            return contentgo;
        }
    }
}