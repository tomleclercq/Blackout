// Shader created with Shader Forge Beta 0.17 
// Shader Forge (c) Joachim 'Acegikmo' Holmer
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.17;sub:START;pass:START;ps:lgpr:1,nrmq:1,limd:1,blpr:0,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:True,uamb:True,mssp:True,ufog:False,aust:True,igpj:False,qofs:0,lico:1,qpre:1,flbk:,rntp:1,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|diff-9-OUT;n:type:ShaderForge.SFN_ViewVector,id:3,x:33537,y:32652;n:type:ShaderForge.SFN_NormalVector,id:4,x:33522,y:32779,pt:False;n:type:ShaderForge.SFN_Lerp,id:9,x:33006,y:32642|A-12-RGB,B-13-RGB,T-118-OUT;n:type:ShaderForge.SFN_Color,id:12,x:33180,y:32396,ptlb:OutlineColor,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:13,x:33509,y:32458,ptlb:MainTexture,tex:364c04ac06a768048815ba73288352c4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:118,x:33064,y:32911|A-172-OUT,B-119-OUT,GT-189-OUT,EQ-189-OUT,LT-159-OUT;n:type:ShaderForge.SFN_Lerp,id:119,x:33457,y:32964|A-197-OUT,B-198-OUT,T-126-OUT;n:type:ShaderForge.SFN_Max,id:126,x:33679,y:33098|A-128-OUT,B-132-OUT;n:type:ShaderForge.SFN_Vector1,id:128,x:33964,y:33123,v1:0;n:type:ShaderForge.SFN_Dot,id:132,x:33917,y:33205,dt:0|A-136-OUT,B-134-OUT;n:type:ShaderForge.SFN_ViewVector,id:134,x:34228,y:33289;n:type:ShaderForge.SFN_NormalVector,id:136,x:34228,y:33146,pt:False;n:type:ShaderForge.SFN_Vector1,id:140,x:33238,y:33204,v1:0;n:type:ShaderForge.SFN_Max,id:159,x:33060,y:33204|A-140-OUT,B-163-OUT;n:type:ShaderForge.SFN_Dot,id:163,x:33294,y:33303,dt:0|A-164-OUT,B-165-OUT;n:type:ShaderForge.SFN_NormalVector,id:164,x:33596,y:33281,pt:False;n:type:ShaderForge.SFN_LightVector,id:165,x:33486,y:33386;n:type:ShaderForge.SFN_Dot,id:172,x:33275,y:32701,dt:0|A-3-OUT,B-4-OUT;n:type:ShaderForge.SFN_Vector1,id:189,x:33371,y:33123,v1:1;n:type:ShaderForge.SFN_Slider,id:197,x:33764,y:32914,ptlb:Unlit Outline Thickness,min:0,cur:0.7425918,max:1;n:type:ShaderForge.SFN_Slider,id:198,x:33764,y:33015,ptlb:Lit Outline Thickness,min:0,cur:0.3007519,max:1;proporder:12-13-198-197;pass:END;sub:END;*/

Shader "Shader Forge/Outline" {
    Properties {
        _OutlineColor ("OutlineColor", Color) = (1,0,0,1)
        _MainTexture ("MainTexture", 2D) = "white" {}
        _LitOutlineThickness ("Lit Outline Thickness", Range(0, 1)) = 0
        _UnlitOutlineThickness ("Unlit Outline Thickness", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _OutlineColor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _UnlitOutlineThickness;
            uniform float _LitOutlineThickness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = normalize(i.normalDir);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz;
                float node_118_if_leA = step(dot(viewDirection,i.normalDir),lerp(_UnlitOutlineThickness,_LitOutlineThickness,max(0.0,dot(i.normalDir,viewDirection))));
                float node_118_if_leB = step(lerp(_UnlitOutlineThickness,_LitOutlineThickness,max(0.0,dot(i.normalDir,viewDirection))),dot(viewDirection,i.normalDir));
                float node_189 = 1.0;
                float3 finalColor = diffuse * lerp(_OutlineColor.rgb,tex2D(_MainTexture,TRANSFORM_TEX(i.uv0.rg, _MainTexture)).rgb,lerp((node_118_if_leA*max(0.0,dot(i.normalDir,lightDirection)))+(node_118_if_leB*node_189),node_189,node_118_if_leA*node_118_if_leB));
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _OutlineColor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _UnlitOutlineThickness;
            uniform float _LitOutlineThickness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = normalize(i.normalDir);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float node_118_if_leA = step(dot(viewDirection,i.normalDir),lerp(_UnlitOutlineThickness,_LitOutlineThickness,max(0.0,dot(i.normalDir,viewDirection))));
                float node_118_if_leB = step(lerp(_UnlitOutlineThickness,_LitOutlineThickness,max(0.0,dot(i.normalDir,viewDirection))),dot(viewDirection,i.normalDir));
                float node_189 = 1.0;
                float3 finalColor = diffuse * lerp(_OutlineColor.rgb,tex2D(_MainTexture,TRANSFORM_TEX(i.uv0.rg, _MainTexture)).rgb,lerp((node_118_if_leA*max(0.0,dot(i.normalDir,lightDirection)))+(node_118_if_leB*node_189),node_189,node_118_if_leA*node_118_if_leB));
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
