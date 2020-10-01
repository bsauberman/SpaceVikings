using UnityEngine;
using System;
using System.Collections.Generic;

namespace Maquette.Unity
{
    public class MqBrushAssetList : ScriptableObject
    {
        public static string AssetFileName = "MaquetteBrushAssetList";
        public List<MqBrushAssetItem> brushes;
    }

    [Serializable]
    public class MqBrushAssetItem
    {
        public string name;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")]
        public Material material;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")]
        public Material material_VertexColor;

        public string tooltipText;
    }
}