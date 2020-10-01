Shader "Maquette/DefaultEntity/TranslucentLit_VertexColor"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags 
		{
			"LightMode" = "ForwardBase" 
			"Queue" = "Transparent" 
			"RenderType" = "Transparent"
			"IgnoreProjector" = "True" 
		}

		Pass 
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Enable gpu instancing variants.
			#pragma multi_compile_instancing

			#include "UnityCG.cginc" // for UnityObjectToWorldNormal
			#include "UnityLightingCommon.cginc" // for _LightColor0

			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
			UNITY_INSTANCING_BUFFER_END(Props)

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				fixed3 color : COLOR;

				// Need this for basic functionality.
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed3 diff : COLOR0; // diffuse lighting color
				float4 vertex : SV_POSITION;
				fixed3 color : TEXCOORD1;

				// Need this for basic functionality.
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			v2f vert(appdata v)
			{
				v2f o;

				// Need this for basic functionality.
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				// get vertex normal in world space
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);

				// dot product between normal and light direction for
				// standard diffuse (Lambert) lighting
				fixed nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));

				// factor in the light color
				o.diff = nl * _LightColor0;
				o.color = v.color;

				return o;
			}

			min16float4 frag(v2f i) : SV_Target 
			{
				// Need this for basic functionality.
				UNITY_SETUP_INSTANCE_ID(i);

				min16float4 col = tex2D(_MainTex, i.uv);

				col.rgb *= (min16float3)i.diff;
				col *= (min16float4)UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
				col.rgb *= (min16float3)i.color.rgb;

				return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}