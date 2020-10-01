Shader "Maquette/DefaultEntity/EdgesDark"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (0.09411765,0.5882353,1,1)
		_LightSoftness("Fresnel level: Rim, Core", Vector) = (0, 0.5, 1, 1)
	}

		SubShader
		{
			Pass 
			/*
			{
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
			*/
			{
				Tags{ "RenderType" = "Geometry" }
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
				float4 _LightSoftness;

				UNITY_INSTANCING_BUFFER_START(Props)
				UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
				UNITY_INSTANCING_BUFFER_END(Props)
				
				struct appdata
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;

					// Need this for basic functionality.
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f {
					float4 pos : SV_POSITION;
					float4 uv : TEXCOORD0;
					min16float3 normal : TEXCOORD1;
					min16float3 viewDir : TEXCOORD2;

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
					o.uv.xy = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
					o.normal = v.normal;
					o.viewDir = ObjSpaceViewDir(v.vertex);

					return o;
				}

				min16float4 frag(v2f i) : COLOR
				{
					// Need this for basic functionality.
					UNITY_SETUP_INSTANCE_ID(i);

					min16float4 lightSoftness = (min16float4)_LightSoftness;

					min16float4 col = tex2D(_MainTex, i.uv.xy);

					min16float dotProduct = saturate(dot(normalize((min16float3)i.normal), normalize((min16float3)i.viewDir)));
					//col += (min16float4)UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * smoothstep((min16float)_LightSoftness, (min16float)0, dotProduct);
					min16float dim = lerp(lightSoftness.x, lightSoftness.y, dotProduct);
					col.rgb *= (min16float3)UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * dim * dim;
					//col.rgb *= (min16float3)UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * pow(dim, lightSoftness.z);

					return col;
				}
				ENDCG
			}
		}
	//To receive or cast a shadow or SSAO, shaders must implement the appropriate "Shadow Collector" or "Shadow Caster" pass.
	//If these passes are missing they will be read from a fallback shader to import the collector/caster passes used in that fallback.
	FallBack "Diffuse"
}