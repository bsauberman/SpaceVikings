// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Maquette/DefaultEntity/Fresnel"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (0.09411765,0.5882353,1,1)
		_Fresnel("Fresnel level: Rim, Core", Vector) = (0, 0.5, 1, 1)
	}

	SubShader 
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM

		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert 
		// Use Shader model 3.0 target
		#pragma target 3.0

		sampler2D _MainTex;
		float4 _Fresnel;

		struct Input 
		{
			float2 uv_MainTex;
			min16float3 normal;
			min16float3 viewDir;

			// Need this for basic functionality.
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert (inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

			o.normal = mul(unity_ObjectToWorld, v.normal);
			o.viewDir = WorldSpaceViewDir(v.vertex);
		}

		void surf(Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = 0;

			min16float4 fresnel = (min16float4)_Fresnel;
			min16float dotProduct = saturate(dot(normalize((min16float3)IN.normal), normalize((min16float3)IN.viewDir)));
			min16float dim = lerp(fresnel.x, fresnel.y, dotProduct);
			c.rgb *= (min16float3)UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * dim * dim;

			o.Emission = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	
	//To receive or cast a shadow or SSAO, shaders must implement the appropriate "Shadow Collector" or "Shadow Caster" pass.
	//If these passes are missing they will be read from a fallback shader to import the collector/caster passes used in that fallback.
	FallBack "Diffuse"
}