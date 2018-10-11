Shader "Custom/Floor1Shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		[NoScaleOffset] _MainTex ("Floor Texture", 2D) = "white" {}
		_PlateSize("Plate Size", Float) = 15
		
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM		
		#pragma surface surf Lambert
				
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
				
		fixed4 _Color;
		float _PlateSize;
				
		void surf (Input IN, inout SurfaceOutput o) {			
			float2 uv = IN.uv_MainTex * _PlateSize;			
			fixed4 c = tex2D(_MainTex, uv) * _Color;
			o.Albedo = c.rgb;
			
			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
