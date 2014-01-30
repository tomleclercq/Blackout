Shader "Custom/Transparent/TextureLayering"
{
	Properties 
	{
		_MainTex("_MainTex", 2D) = "black" {}
		_OverlayTex("_OverlayTex", 2D) = "black" {}
		_YOffset("_YOffset", Range(0,1)) = 1
		_XOffset("_XOffset", Range(0,1)) = 0.33
	}
	
	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha
		
		sampler2D _MainTex;
		sampler2D _OverlayTex;
		float _XOffset;
		float _YOffset;

		
		struct Input 
		{
			float2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) 
		{
				float4 TexMain = tex2D(_MainTex,IN.uv_MainTex);
				float4 TexOverlay = tex2D(_OverlayTex,IN.uv_MainTex);
				float signedValueX = sign(step(IN.uv_MainTex.x,_XOffset));		
				float signedValueY = sign(step(IN.uv_MainTex.y,_YOffset));	
				float signedValue = max( signedValueX ,signedValueY);
				o.Albedo = lerp(TexMain.rgb,TexOverlay.rgb ,signedValue);			
				o.Alpha = TexMain.a;
		}
		ENDCG
	}
	Fallback "Transparent/VertexLit"
}


