Shader "Custom/GlassDoor" {
	
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Amount("Alpha Amount", Range(0, 1)) = 1
	
	}
		SubShader
	{
		Tags
	{
		"Queue" = "Transparent" // D
	}
			CGPROGRAM
#pragma surface surf Lambert alpha vertex:vert
			//#pragma surface surf BlinnPhong 

			sampler2D _MainTex;
			half _mySlider;

		struct Input {
			float2 uv_MainTex;			
		};

		struct appdata { // struct para o Vertex.
			float4 vertex: POSITION;
			float3 normal: NORMAL;
			float4 texcoord: TEXCOORD0;
			float4 tangent: TANGENT;
		};

		float _Amount;
		
		void vert(inout appdata v) {
			//v.vertex.xyz = v.normal;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Alpha = _Amount;
			
		}
		ENDCG
		}
			Fallback "VertexLit"
			//Fallback "Specular"
}
