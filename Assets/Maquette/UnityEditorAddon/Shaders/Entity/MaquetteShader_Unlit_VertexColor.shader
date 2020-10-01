// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Maquette/DefaultEntity/Unlit_VertexColor" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	SubShader 
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM

		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		// Use Shader model 3.0 target
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			min16float4 color : Color;
		};

		half _Glossiness;
		half _Metallic;
		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert (inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

			o.color = v.color;
		}

		void surf(Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
			o.Albedo = 0;
			o.Metallic = 1;
			o.Smoothness = 0;

			c.rgb *= (min16float3)IN.color.rgb;

			o.Emission = c.rgb;
			o.Alpha = c.a * IN.color.a;
		}
		ENDCG
	}
}
/*
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Pass 
		{
			Name "DEFAULT"

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Enable gpu instancing variants.
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _MainTex_ST;

			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
			UNITY_INSTANCING_BUFFER_END(Props)

			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				fixed3 color : COLOR;

				// Need this for basic functionality.
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
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

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;

				return o;
			}		

			min16float4 frag(v2f i) : SV_Target 
			{
				// Need this for basic functionality.
				UNITY_SETUP_INSTANCE_ID(i);

				min16float4 col = tex2D(_MainTex, i.uv);
				col *= (min16float4)UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
				col.rgb *= (min16float3)i.color.rgb;

				return col;
			}
			ENDCG
		}
	}
	//To receive or cast a shadow or SSAO, shaders must implement the appropriate "Shadow Collector" or "Shadow Caster" pass.
	//If these passes are missing they will be read from a fallback shader to import the collector/caster passes used in that fallback.
	FallBack "Diffuse"
}
*/