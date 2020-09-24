Shader "Custom/02ModelPositionAsColor"
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
			};
			
			struct ToFrag
			{
				float4 vertex : SV_POSITION; // Required
				float4 positionModelSpace : TEXCOORD0; // Send on first uv coordinate channel.
			};

			
			ToFrag Vert( ToVert v )
			{
				ToFrag o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				o.positionModelSpace = v.vertex;
				return o;
			}
			
			half4 Frag( ToFrag i ) : SV_Target
			{

				return i.positionModelSpace;
			}

			ENDCG
		}
	}
}