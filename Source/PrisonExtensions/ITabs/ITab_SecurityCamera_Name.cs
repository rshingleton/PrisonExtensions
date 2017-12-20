using System;
using PrisonExtensions.Buildings;
using RimWorld;
using UnityEngine;
using Verse;

namespace PrisonExtensions.ITabs
{
    public class ITab_SecurityCamera_Name : ITab
    {
        private static readonly Vector2 WinSize = new Vector2(300f, 100f);

        public ITab_SecurityCamera_Name()
        {
            this.size = WinSize;
            this.labelKey = "prison.SecurityCameraTab";
            this.tutorTag = "Camera";
        }

        protected override void FillTab()
        {
            Building_SecurityCamera bpm = this.SelThing as Building_SecurityCamera;

            Text.Font = GameFont.Small;
            Rect innerRect1 = GenUI.GetInnerRect(new Rect(10.0f, 10.0f, this.size.x - 10, this.size.y - 10));
            GUI.BeginGroup(innerRect1);

            Widgets.Label(new Rect(0, 5, this.size.x / 2, 30), "Area Name:");
            bpm.markerName = Widgets.TextField(new Rect(90, 0, this.size.x / 2, 30),
                bpm.markerName.Substring(0, Math.Min(bpm.markerName.Length, 20)));

            GenUI.AbsorbClicksInRect(innerRect1);

            GUI.EndGroup();
        }
    }
}