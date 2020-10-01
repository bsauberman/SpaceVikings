using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Maquette.Unity
{
    public class MqMaterialAssetList : ScriptableObject
    {
        public static string AssetFileName = "MaquetteMaterialAssetList";
        public List<MqMaterialAssetItem> materials;
    }

    [Serializable]
    public class MqMaterialAssetItem
    {
        public string name;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")]
        public Material material;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")]
        public Material material_VertexColor;

        public string tooltipText;

        public bool isCastShadowDefaultOn;
    }
}