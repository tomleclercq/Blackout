// Shader created with Shader Forge Beta 0.17 
// Shader Forge (c) Joachim 'Acegikmo' Holmer
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.17;sub:START;pass:START;ps:lgpr:1,nrmq:1,limd:1,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,uamb:True,mssp:True,ufog:True,aust:True,igpj:True,qofs:0,lico:1,qpre:3,flbk:,rntp:2,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,hqsc:True,hqlp:False,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300;n:type:ShaderForge.SFN_Final,id:1,x:31910,y:32632|diff-6817-OUT,spec-7191-OUT,emission-6838-OUT,alpha-9720-OUT,olwid-2309-OUT,olcol-6978-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33825,y:32721,ptlb:MainTex,tex:404a5b16a5c03ee4fae7d9f1b2e774d5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:15,x:33057,y:32799,ptlb:MainColor,c1:0.6156863,c2:0.4980392,c3:0.9960784,c4:1;n:type:ShaderForge.SFN_Multiply,id:241,x:33126,y:33155|A-15-A,B-2-A;n:type:ShaderForge.SFN_Fresnel,id:765,x:33613,y:32225;n:type:ShaderForge.SFN_Add,id:797,x:33430,y:32225|A-765-OUT,B-798-OUT;n:type:ShaderForge.SFN_Vector1,id:798,x:33613,y:32355,v1:0.25;n:type:ShaderForge.SFN_Power,id:799,x:33249,y:32241|VAL-797-OUT,EXP-800-OUT;n:type:ShaderForge.SFN_Vector1,id:800,x:33430,y:32355,v1:128;n:type:ShaderForge.SFN_Clamp,id:801,x:33072,y:32323|IN-799-OUT,MIN-802-OUT,MAX-2259-OUT;n:type:ShaderForge.SFN_Vector1,id:802,x:33249,y:32392,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:885,x:33072,y:32521|A-801-OUT,B-2259-OUT;n:type:ShaderForge.SFN_Add,id:1662,x:32880,y:33155|A-885-OUT,B-241-OUT;n:type:ShaderForge.SFN_Multiply,id:1932,x:32769,y:32400|A-6978-OUT,B-885-OUT;n:type:ShaderForge.SFN_Multiply,id:1953,x:32593,y:32449|A-1932-OUT,B-1954-OUT;n:type:ShaderForge.SFN_Vector1,id:1954,x:32769,y:32523,v1:0.5;n:type:ShaderForge.SFN_Color,id:2154,x:33072,y:32146,ptlb:DamageColor,c1:1,c2:0.1,c3:0.1,c4:1;n:type:ShaderForge.SFN_Add,id:2181,x:32422,y:32589|A-1953-OUT,B-2217-OUT;n:type:ShaderForge.SFN_Multiply,id:2217,x:32841,y:32738|A-15-RGB,B-2-RGB;n:type:ShaderForge.SFN_Slider,id:2259,x:33825,y:32613,ptlb:DamgeSlider,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:2269,x:32641,y:33527,v1:0.15;n:type:ShaderForge.SFN_Clamp,id:2309,x:32432,y:33421|IN-2340-OUT,MIN-2334-OUT,MAX-2269-OUT;n:type:ShaderForge.SFN_Vector1,id:2334,x:32641,y:33451,v1:0;n:type:ShaderForge.SFN_Multiply,id:2340,x:32880,y:33387|A-6967-OUT,B-2346-OUT;n:type:ShaderForge.SFN_Vector1,id:2346,x:33105,y:33505,v1:0.15;n:type:ShaderForge.SFN_Tex2d,id:6762,x:32722,y:31914,ptlb:PowerUp,tex:72a655524e3ca8f418172d1750efd774,ntxv:0,isnm:False|UVIN-6858-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6763,x:32419,y:32332|A-6764-OUT,B-7131-OUT;n:type:ShaderForge.SFN_Slider,id:6764,x:33825,y:32503,ptlb:PowerUpSlider,min:0,cur:0,max:1;n:type:ShaderForge.SFN_TexCoord,id:6767,x:33074,y:31914,uv:0;n:type:ShaderForge.SFN_Add,id:6817,x:32223,y:32455|A-6763-OUT,B-2181-OUT;n:type:ShaderForge.SFN_Add,id:6838,x:32230,y:32730|A-6763-OUT,B-1953-OUT;n:type:ShaderForge.SFN_Panner,id:6858,x:32898,y:31914,spu:0.05,spv:0|UVIN-6767-UVOUT;n:type:ShaderForge.SFN_Add,id:6967,x:33105,y:33361|A-6764-OUT,B-2259-OUT;n:type:ShaderForge.SFN_If,id:6978,x:33072,y:32640|A-6764-OUT,B-2259-OUT,GT-6979-RGB,EQ-2154-RGB,LT-2154-RGB;n:type:ShaderForge.SFN_Color,id:6979,x:32769,y:32203,ptlb:PowerUpColor,c1:1,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7131,x:32461,y:32075|A-6762-A,B-6979-RGB;n:type:ShaderForge.SFN_Tex2d,id:7190,x:32039,y:33185,ptlb:SpocularTex,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7191,x:31860,y:33129|A-7192-OUT,B-7190-RGB;n:type:ShaderForge.SFN_Slider,id:7192,x:32029,y:33089,ptlb:SpecularSlider,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:7247,x:32895,y:33059,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:7285,x:32363,y:33155|A-7911-OUT,B-1662-OUT;n:type:ShaderForge.SFN_OneMinus,id:7879,x:33413,y:32710|IN-2259-OUT;n:type:ShaderForge.SFN_Add,id:7910,x:32733,y:33015|A-7879-OUT,B-7247-OUT;n:type:ShaderForge.SFN_Clamp,id:7911,x:32552,y:33039|IN-7910-OUT,MIN-7247-OUT,MAX-7912-OUT;n:type:ShaderForge.SFN_Vector1,id:7912,x:32733,y:33200,v1:1;n:type:ShaderForge.SFN_Multiply,id:9720,x:32363,y:32982|A-15-A,B-7911-OUT;proporder:15-2-2154-2259-6762-6979-6764-7190-7192;pass:END;sub:END;*/

Shader "Blackout/Ship" {
    Properties {
        _MainColor ("MainColor", Color) = (0.6156863,0.4980392,0.9960784,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _DamageColor ("DamageColor", Color) = (1,0.1,0.1,1)
        _DamgeSlider ("DamgeSlider", Range(0, 1)) = 0
        _PowerUp ("PowerUp", 2D) = "white" {}
        _PowerUpColor ("PowerUpColor", Color) = (1,0,1,1)
        _PowerUpSlider ("PowerUpSlider", Range(0, 1)) = 0
        _SpocularTex ("SpocularTex", 2D) = "white" {}
        _SpecularSlider ("SpecularSlider", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _DamageColor;
            uniform float _DamgeSlider;
            uniform float _PowerUpSlider;
            uniform float4 _PowerUpColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                float node_6764 = _PowerUpSlider;
                float node_2259 = _DamgeSlider;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*clamp(((node_6764+node_2259)*0.15),0.0,0.15),1));
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float node_6764 = _PowerUpSlider;
                float node_2259 = _DamgeSlider;
                float node_6978_if_leA = step(node_6764,node_2259);
                float node_6978_if_leB = step(node_2259,node_6764);
                float4 node_2154 = _DamageColor;
                float4 node_6979 = _PowerUpColor;
                float3 node_6978 = lerp((node_6978_if_leA*node_2154.rgb)+(node_6978_if_leB*node_6979.rgb),node_2154.rgb,node_6978_if_leA*node_6978_if_leB);
                return fixed4(node_6978,0);
            }
            ENDCG
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MainColor;
            uniform float4 _DamageColor;
            uniform float _DamgeSlider;
            uniform sampler2D _PowerUp; uniform float4 _PowerUp_ST;
            uniform float _PowerUpSlider;
            uniform float4 _PowerUpColor;
            uniform sampler2D _SpocularTex; uniform float4 _SpocularTex_ST;
            uniform float _SpecularSlider;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = normalize(i.normalDir);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz*2;
////// Emissive:
                float node_6764 = _PowerUpSlider;
                float4 node_9811 = _Time + _TimeEditor;
                float4 node_6979 = _PowerUpColor;
                float3 node_6763 = (node_6764*(tex2D(_PowerUp,TRANSFORM_TEX((i.uv0.rg+node_9811.g*float2(0.05,0)), _PowerUp)).a*node_6979.rgb));
                float node_2259 = _DamgeSlider;
                float node_6978_if_leA = step(node_6764,node_2259);
                float node_6978_if_leB = step(node_2259,node_6764);
                float4 node_2154 = _DamageColor;
                float3 node_6978 = lerp((node_6978_if_leA*node_2154.rgb)+(node_6978_if_leB*node_6979.rgb),node_2154.rgb,node_6978_if_leA*node_6978_if_leB);
                float node_885 = (clamp(pow(((1.0-max(0,dot(normalDirection, viewDirection)))+0.25),128.0),0.05,node_2259)*node_2259);
                float3 node_1953 = ((node_6978*node_885)*0.5);
                float3 emissive = (node_6763+node_1953);
///////// Gloss:
                float gloss = exp2(0.5*10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float2 node_9810 = i.uv0;
                float3 specularColor = (_SpecularSlider*tex2D(_SpocularTex,TRANSFORM_TEX(node_9810.rg, _SpocularTex)).rgb);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),gloss) * specularColor;
                float4 node_15 = _MainColor;
                float4 node_2 = tex2D(_MainTex,TRANSFORM_TEX(node_9810.rg, _MainTex));
                float3 finalColor = diffuse * (node_6763+(node_1953+(node_15.rgb*node_2.rgb))) + specular + emissive;
                float node_7247 = 0.5;
                float node_7911 = clamp(((1.0 - node_2259)+node_7247),node_7247,1.0);
/// Final Color:
                return fixed4(finalColor,(node_15.a*node_7911));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MainColor;
            uniform float4 _DamageColor;
            uniform float _DamgeSlider;
            uniform sampler2D _PowerUp; uniform float4 _PowerUp_ST;
            uniform float _PowerUpSlider;
            uniform float4 _PowerUpColor;
            uniform sampler2D _SpocularTex; uniform float4 _SpocularTex_ST;
            uniform float _SpecularSlider;
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
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = exp2(0.5*10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float2 node_9812 = i.uv0;
                float3 specularColor = (_SpecularSlider*tex2D(_SpocularTex,TRANSFORM_TEX(node_9812.rg, _SpocularTex)).rgb);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),gloss) * specularColor;
                float node_6764 = _PowerUpSlider;
                float4 node_9813 = _Time + _TimeEditor;
                float4 node_6979 = _PowerUpColor;
                float3 node_6763 = (node_6764*(tex2D(_PowerUp,TRANSFORM_TEX((i.uv0.rg+node_9813.g*float2(0.05,0)), _PowerUp)).a*node_6979.rgb));
                float node_2259 = _DamgeSlider;
                float node_6978_if_leA = step(node_6764,node_2259);
                float node_6978_if_leB = step(node_2259,node_6764);
                float4 node_2154 = _DamageColor;
                float3 node_6978 = lerp((node_6978_if_leA*node_2154.rgb)+(node_6978_if_leB*node_6979.rgb),node_2154.rgb,node_6978_if_leA*node_6978_if_leB);
                float node_885 = (clamp(pow(((1.0-max(0,dot(normalDirection, viewDirection)))+0.25),128.0),0.05,node_2259)*node_2259);
                float3 node_1953 = ((node_6978*node_885)*0.5);
                float4 node_15 = _MainColor;
                float4 node_2 = tex2D(_MainTex,TRANSFORM_TEX(node_9812.rg, _MainTex));
                float3 finalColor = diffuse * (node_6763+(node_1953+(node_15.rgb*node_2.rgb))) + specular;
                float node_7247 = 0.5;
                float node_7911 = clamp(((1.0 - node_2259)+node_7247),node_7247,1.0);
/// Final Color:
                return fixed4(finalColor * (node_15.a*node_7911),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
