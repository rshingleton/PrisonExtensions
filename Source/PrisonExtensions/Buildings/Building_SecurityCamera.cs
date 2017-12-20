using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace PrisonExtensions.Buildings
{
    public class Building_SecurityCamera : Building
    {
        private static List<IntVec3> prisonField = new List<IntVec3>();
        private static readonly Color PrisonFieldColor = new Color(1f, 0.7f, 0.2f);

        public String markerName = "";

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.markerName, "markerName", "");
        }

        public override void TickRare()
        {
            //I don't want prison markers to conflict with beds functionality
            var r = this.GetRoom();
            if (r == null || r.TouchesMapEdge)
            {
                this.DeSpawn();
                return;
            }

            if (r.ContainedBeds.Any())
            {
                var b = r.ContainedBeds.ElementAt(0);
                if (!b.ForPrisoners)
                {
                    this.DeSpawn();
                    return;
                }
            }

            if (!r.isPrisonCell)
            {
                r.isPrisonCell = true;
            }
        }

        public override void DeSpawn()
        {
            this.GetRoom().Notify_RoofChanged();
            base.DeSpawn();
        }

        public override void DrawExtraSelectionOverlays()
        {
            base.DrawExtraSelectionOverlays();
            var room = this.GetRoom();
            if (room == null || room.RegionCount >= 20 || room.TouchesMapEdge) return;
            foreach (IntVec3 current in room.Cells)
            {
                prisonField.Add(current);
            }
            var prisonFieldColor = PrisonFieldColor;
            prisonFieldColor.a = Pulser.PulseBrightness(1f, 0.6f);
            GenDraw.DrawFieldEdges(prisonField, prisonFieldColor);
            prisonField.Clear();
        }

        public override void DrawGUIOverlay()
        {
            if (Find.CameraDriver.CurrentZoom != CameraZoomRange.Closest) return;
            if (!markerName.Trim().Equals(""))
            {
                GenMapUI.DrawThingLabel(this, markerName, new Color(1f, 1f, 1f, 0.75f));
            }
        }
    }
}