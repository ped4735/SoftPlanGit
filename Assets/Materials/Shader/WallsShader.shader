Shader "Custom/WallsShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)		
		_MainTex("Texture", 2D) = "white" {}
		_Amount("Extrude", Range(-1,1)) = 0.01
	}

	SubShader{

			Tags{ "RenderType" = "Opaque" }

		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float2 uv_MainTex;
	};

	struct appdata {
		float4 vertex: POSITION;
		float3 normal: NORMAL;
		float4 texcoord: TEXCOORD0;
	};

	float _Amount;

	void vert(inout appdata v) {
		v.vertex.xyz += v.normal * _Amount;
	}

	sampler2D _MainTex;
	fixed4 _Color;
	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 _colour = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = _colour.rgb;
		//o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
	}

	ENDCG
	}
		Fallback "Diffuse"
	}
