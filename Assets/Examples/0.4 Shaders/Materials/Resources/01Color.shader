Shader "Custom/01Color"
{
	Properties{
		_Color ("Color",color)=(1,1,1,1)//shader name, ((inspector)editor name, type).

	}
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
				float4 vertex : SV_POSITION;
			};

			float4 _Color; // Our first "shader constant". Notice same name as property name.
			
			ToFrag Vert( ToVert v )
			{
				ToFrag o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				return o;
			}
			
			half4 Frag( ToFrag i ) : SV_Target
			{
				return _Color;
			}

			ENDCG
		}
	}
}