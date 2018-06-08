
Shader "Learning/Displacement"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0,0.1)) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite OFf ZTest Always

		Pass
		{
			// Blend modes https://docs.unity3d.com/Manual/SL-Blend.html
			//Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			// Input docs https://docs.unity3d.com/Manual/SL-VertexProgramInputs.html
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _DisplaceTex;
			float _Magnitude;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 disp = tex2D(_DisplaceTex, i.uv).xy;
				// Move range from 0-1 to -1-1 * Mag
				disp = ((disp * 2) -1 ) * _Magnitude;
				fixed4 col = tex2D(_MainTex, i.uv + disp);
				return col;
			}
			ENDCG
		}
	}
}
