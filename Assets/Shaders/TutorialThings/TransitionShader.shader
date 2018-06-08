﻿Shader "Learning/TransitionShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TransitionTex("Transition Texture", 2D) = "white" {}
		_Color("Screen Color", Color) = (0,0,0,0)
		_Cutoff("Cuttoff", Range(0,1)) = 0
		[MaterialToggle] _Distort("Distort", Float) = 0
		_Fade("Fade", Range(0,1)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _TransitionTex;
			float4 _MainTex_ST;
			float _Cutoff;
			fixed4 _Color;
			float _Distort;
			float _Fade;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 simplefrag(v2f i) : SV_Target
			{
				// Return _Color we're below the cutoff
				if(i.uv.x < _Cutoff){ return _Color; }
				return tex2D(_MainTex, i.uv);
			}

			fixed4 simpleTexture(v2f i) : SV_Target {
				fixed4 transit = tex2D(_TransitionTex, i.uv);
				if(transit.b < _Cutoff){ return _Color; }
				return tex2D(_MainTex, i.uv);
			}

			fixed4 frag(v2f i) : SV_Target {
				fixed4 transit = tex2D(_TransitionTex, i.uv);

				fixed2 direction = float2(0,0);
				if(_Distort){
					direction = normalize(float2((transit.r - 0.5) * 2, (transit.g - 0.5) * 2));
				}
				fixed4 col = tex2D(_MainTex, i.uv + _Cutoff * direction);

				if(transit.b < _Cutoff){ return lerp(col, _Color, _Fade); }
				return col;
			}
			ENDCG
		}
	}
}
