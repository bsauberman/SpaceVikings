Shader "Maquette/DefaultEntity/EdgesDark_VertexColor"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_Color("Color", Color) = (0.09411765,0.5882353,1,1)
		_LightSoftness("Light Softness", Float) = 2
	}

		SubShader
		{
			Pass {
				Tags{ "RenderType" = "Geometry"  }
				Cull Back
				ZWrite On

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				// Enable gpu instancing variants.
				#pragma multi_compile_instancing

				#include "UnityCG.cginc" // for UnityObjectToWorldNormal


				sampler2D _MainTex;

				float4 _MainTex_ST;

				uniform float _LightSoftness;
				UNITY_INSTANCING_BUFFER_START(Props)
				UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
				UNITY_INSTANCING_BUFFER_END(Props)

				struct appdata
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;

					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f {
					float4 pos : SV_POSITION;
					float4 uv : TEXCOORD0;
					fixed4 color : TEXCOORD1;
				};


				v2f vert(appdata v)
				{
					v2f o;

					UNITY_SETUP_INSTANCE_ID(v);

					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv.xy = TRANSFORM_TEX(v.texcoord.xy, _MainTex);

					float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));

					float dotProduct = 1 - saturate(dot(v.normal, viewDir));

					fixed4 darkColor = UNITY_ACCESS_INSTANCED_PROP(Props, _Color) *0.08;

					o.color = max(UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * smoothstep(  _LightSoftness , -1, dotProduct), darkColor);


					return o;
				}

				fixed4 frag(v2f i) : COLOR
				{
					fixed4 o = i.color;
				
					o.rgb *= 1.5;

					o.rgb += tex2D(_MainTex, i.uv.xy);

					return o;
				}

				ENDCG
			}
		}
}