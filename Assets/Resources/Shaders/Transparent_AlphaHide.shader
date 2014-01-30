Shader "Custom/Transparent/AlphaHide"
{
	Properties 
	{
		_MainTex("_MainTex", 2D) = "black" {}
		_XOffset("_XOffset", Range(0,1)) = 0.33
		_YOffset("_YOffset", Range(0,1)) = 1
		_XDirection("_XDirection direction 0 or 1", Range(0,1)) = 0
		_YDirection("_YDirection direction 0 or 1", Range(0,1)) = 0
	}
	
	SubShader 
	{
		Tags {"Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha
		
		sampler2D _MainTex;
		float _XOffset;
		float _YOffset;
		float _XDirection;
		float _YDirection;

		
		struct Input 
		{
			float2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) 
		{
				float4 TexMain = tex2D(_MainTex,IN.uv_MainTex);

				//Reverse the offcets
				_XOffset = 1 - _XOffset;
				_YOffset = 1 - _YOffset;
				
				//fist get the step (0 or 1) on an axis 
				//than inverse it if the direction is -1
				float xStep = abs(_XDirection - step(_XOffset,IN.uv_MainTex.x));
				float yStep = abs(_YDirection - step(_YOffset,IN.uv_MainTex.y));

				o.Albedo = TexMain.rgb;	
				//min the value of the two step results an multiply it whit the alpha. to display alpha or not.		
				o.Alpha = min(xStep,yStep) * TexMain.a;
		}
		ENDCG
	}
	Fallback "Transparent/VertexLit"
}


