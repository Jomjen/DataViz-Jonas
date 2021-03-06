﻿Shader "Custom/03UvAsColor"
{

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex Vert
			#pragma fragment Frag
		
			struct ToVert
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0; // UV set 0
			};
			
			struct ToFrag
			{
				float4 vertex : SV_POSITION; // Required
				float2 uv : TEXCOORD0;
			};

			
			ToFrag Vert( ToVert v )
			{
				ToFrag o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				o.uv = v.uv; // Copy uv to output that will be forwarded to frag function
				return o;
			}
			
			half4 Frag( ToFrag i ) : SV_Target
			{

				return half4(i.uv,0,1); // same as half4(i.uv.x, i.uv.y, 0,1)
			}

			ENDCG
		}
	}
}