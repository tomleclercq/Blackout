// Shader created with Shader Forge Beta 0.17 
// Shader Forge (c) Joachim 'Acegikmo' Holmer
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.17;sub:START;pass:START;ps:lgpr:1,nrmq:1,limd:2,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,uamb:True,mssp:True,ufog:True,aust:False,igpj:True,qofs:1,lico:1,qpre:3,flbk:,rntp:2,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,hqsc:True,hqlp:False,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300;n:type:ShaderForge.SFN_Final,id:1,x:31994,y:32713|diff-15-RGB,emission-2042-OUT,alpha-1662-OUT,clip-268-OUT,olwid-2095-OUT,olcol-15-RGB;n:type:ShaderForge.SFN_Tex2d,id:2,x:33641,y:32521,ptlb:MainShield,tex:404a5b16a5c03ee4fae7d9f1b2e774d5,ntxv:0,isnm:False|UVIN-105-UVOUT;n:type:ShaderForge.SFN_Color,id:15,x:33110,y:32638,ptlb:MainColor,c1:0.6156863,c2:0.4980392,c3:0.9960784,c4:0.5;n:type:ShaderForge.SFN_Panner,id:105,x:33844,y:32521,spu:0,spv:0.05;n:type:ShaderForge.SFN_Add,id:126,x:33400,y:32615|A-2-A,B-127-A;n:type:ShaderForge.SFN_Tex2d,id:127,x:33641,y:32712,ptlb:SecondShield,tex:e78909bd5e3ad3643810dbb96b2b1e7c,ntxv:0,isnm:False|UVIN-144-UVOUT;n:type:ShaderForge.SFN_Panner,id:144,x:33844,y:32712,spu:-0.05,spv:-0.05;n:type:ShaderForge.SFN_Tex2d,id:213,x:34217,y:32966,ptlb:CloudTex,tex:9eea4f0d9ed7b7040942be442dfc76f8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:241,x:32965,y:32853|A-15-A,B-126-OUT;n:type:ShaderForge.SFN_Multiply,id:253,x:34016,y:32966|A-213-A,B-465-OUT;n:type:ShaderForge.SFN_OneMinus,id:254,x:34016,y:33096|IN-465-OUT;n:type:ShaderForge.SFN_Multiply,id:261,x:33679,y:33223|A-254-OUT,B-564-A;n:type:ShaderForge.SFN_Multiply,id:263,x:33691,y:33077|A-253-OUT,B-534-OUT;n:type:ShaderForge.SFN_Add,id:264,x:33515,y:33132|A-263-OUT,B-261-OUT;n:type:ShaderForge.SFN_Add,id:265,x:33353,y:33062|A-465-OUT,B-264-OUT;n:type:ShaderForge.SFN_Power,id:266,x:33150,y:33062|VAL-265-OUT,EXP-267-OUT;n:type:ShaderForge.SFN_Vector1,id:267,x:33353,y:33188,v1:100;n:type:ShaderForge.SFN_ConstantClamp,id:268,x:32918,y:33092,min:0.2,max:1|IN-266-OUT;n:type:ShaderForge.SFN_Slider,id:465,x:34217,y:32841,ptlb:DesolveSlider,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:534,x:33896,y:33034,v1:4;n:type:ShaderForge.SFN_Tex2d,id:564,x:33853,y:33267,ptlb:CloudTex2,tex:9eea4f0d9ed7b7040942be442dfc76f8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:765,x:33613,y:32225;n:type:ShaderForge.SFN_Add,id:797,x:33430,y:32225|A-765-OUT,B-798-OUT;n:type:ShaderForge.SFN_Vector1,id:798,x:33613,y:32355,v1:0.25;n:type:ShaderForge.SFN_Power,id:799,x:33243,y:32225|VAL-797-OUT,EXP-800-OUT;n:type:ShaderForge.SFN_Vector1,id:800,x:33430,y:32355,v1:128;n:type:ShaderForge.SFN_Clamp,id:801,x:33066,y:32321|IN-799-OUT,MIN-802-OUT,MAX-803-OUT;n:type:ShaderForge.SFN_Vector1,id:802,x:33243,y:32355,v1:0.05;n:type:ShaderForge.SFN_Vector1,id:803,x:33243,y:32414,v1:1;n:type:ShaderForge.SFN_Multiply,id:885,x:33110,y:32498|A-801-OUT,B-803-OUT;n:type:ShaderForge.SFN_Add,id:1662,x:32736,y:32853|A-885-OUT,B-241-OUT;n:type:ShaderForge.SFN_Multiply,id:1932,x:32843,y:32470|A-15-RGB,B-885-OUT;n:type:ShaderForge.SFN_Multiply,id:1953,x:32661,y:32543|A-1932-OUT,B-1954-OUT;n:type:ShaderForge.SFN_Vector1,id:1954,x:32843,y:32601,v1:0.6;n:type:ShaderForge.SFN_Add,id:2042,x:32442,y:32765|A-1953-OUT,B-2057-OUT;n:type:ShaderForge.SFN_Multiply,id:2057,x:32736,y:32725|A-2074-OUT,B-127-A;n:type:ShaderForge.SFN_Vector1,id:2074,x:32965,y:32792,v1:0.25;n:type:ShaderForge.SFN_Vector1,id:2095,x:32414,y:33011,v1:0.15;proporder:15-2-127-213-564-465;pass:END;sub:END;*/

Shader "Blackout/Shield" {
    Properties {
        _MainColor ("MainColor", Color) = (0.6156863,0.4980392,0.9960784,0.5)
        _MainShield ("MainShield", 2D) = "white" {}
        _SecondShield ("SecondShield", 2D) = "white" {}
        _CloudTex ("CloudTex", 2D) = "white" {}
        _CloudTex2 ("CloudTex2", 2D) = "white" {}
        _DesolveSlider ("DesolveSlider", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+1"
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
            uniform float4 _MainColor;
            uniform sampler2D _CloudTex; uniform float4 _CloudTex_ST;
            uniform float _DesolveSlider;
            uniform sampler2D _CloudTex2; uniform float4 _CloudTex2_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*0.15,1));
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float node_465 = _DesolveSlider;
                float2 node_2178 = i.uv0;
                clip(clamp(pow((node_465+(((tex2D(_CloudTex,TRANSFORM_TEX(node_2178.rg, _CloudTex)).a*node_465)*4.0)+((1.0 - node_465)*tex2D(_CloudTex2,TRANSFORM_TEX(node_2178.rg, _CloudTex2)).a))),100.0),0.2,1) - 0.5);
                float4 node_15 = _MainColor;
                return fixed4(node_15.rgb,0);
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
            uniform sampler2D _MainShield; uniform float4 _MainShield_ST;
            uniform float4 _MainColor;
            uniform sampler2D _SecondShield; uniform float4 _SecondShield_ST;
            uniform sampler2D _CloudTex; uniform float4 _CloudTex_ST;
            uniform float _DesolveSlider;
            uniform sampler2D _CloudTex2; uniform float4 _CloudTex2_ST;
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
                float node_465 = _DesolveSlider;
                float2 node_2179 = i.uv0;
                clip(clamp(pow((node_465+(((tex2D(_CloudTex,TRANSFORM_TEX(node_2179.rg, _CloudTex)).a*node_465)*4.0)+((1.0 - node_465)*tex2D(_CloudTex2,TRANSFORM_TEX(node_2179.rg, _CloudTex2)).a))),100.0),0.2,1) - 0.5);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = normalize(i.normalDir);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz*2;
////// Emissive:
                float4 node_15 = _MainColor;
                float node_803 = 1.0;
                float node_885 = (clamp(pow(((1.0-max(0,dot(normalDirection, viewDirection)))+0.25),128.0),0.05,node_803)*node_803);
                float4 node_2180 = _Time + _TimeEditor;
                float4 node_127 = tex2D(_SecondShield,TRANSFORM_TEX((node_2179.rg+node_2180.g*float2(-0.05,-0.05)), _SecondShield));
                float3 emissive = (((node_15.rgb*node_885)*0.6)+(0.25*node_127.a));
                float3 finalColor = diffuse * node_15.rgb + emissive;
/// Final Color:
                return fixed4(finalColor,(node_885+(node_15.a*(tex2D(_MainShield,TRANSFORM_TEX((node_2179.rg+node_2180.g*float2(0,0.05)), _MainShield)).a+node_127.a))));
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
            uniform sampler2D _MainShield; uniform float4 _MainShield_ST;
            uniform float4 _MainColor;
            uniform sampler2D _SecondShield; uniform float4 _SecondShield_ST;
            uniform sampler2D _CloudTex; uniform float4 _CloudTex_ST;
            uniform float _DesolveSlider;
            uniform sampler2D _CloudTex2; uniform float4 _CloudTex2_ST;
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
                float node_465 = _DesolveSlider;
                float2 node_2181 = i.uv0;
                clip(clamp(pow((node_465+(((tex2D(_CloudTex,TRANSFORM_TEX(node_2181.rg, _CloudTex)).a*node_465)*4.0)+((1.0 - node_465)*tex2D(_CloudTex2,TRANSFORM_TEX(node_2181.rg, _CloudTex2)).a))),100.0),0.2,1) - 0.5);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = normalize(i.normalDir);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float4 node_15 = _MainColor;
                float3 finalColor = diffuse * node_15.rgb;
                float node_803 = 1.0;
                float node_885 = (clamp(pow(((1.0-max(0,dot(normalDirection, viewDirection)))+0.25),128.0),0.05,node_803)*node_803);
                float4 node_2182 = _Time + _TimeEditor;
                float4 node_127 = tex2D(_SecondShield,TRANSFORM_TEX((node_2181.rg+node_2182.g*float2(-0.05,-0.05)), _SecondShield));
/// Final Color:
                return fixed4(finalColor * (node_885+(node_15.a*(tex2D(_MainShield,TRANSFORM_TEX((node_2181.rg+node_2182.g*float2(0,0.05)), _MainShield)).a+node_127.a))),0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _CloudTex; uniform float4 _CloudTex_ST;
            uniform float _DesolveSlider;
            uniform sampler2D _CloudTex2; uniform float4 _CloudTex2_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float4 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float node_465 = _DesolveSlider;
                float2 node_2183 = i.uv0;
                clip(clamp(pow((node_465+(((tex2D(_CloudTex,TRANSFORM_TEX(node_2183.rg, _CloudTex)).a*node_465)*4.0)+((1.0 - node_465)*tex2D(_CloudTex2,TRANSFORM_TEX(node_2183.rg, _CloudTex2)).a))),100.0),0.2,1) - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _CloudTex; uniform float4 _CloudTex_ST;
            uniform float _DesolveSlider;
            uniform sampler2D _CloudTex2; uniform float4 _CloudTex2_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float node_465 = _DesolveSlider;
                float2 node_2184 = i.uv0;
                clip(clamp(pow((node_465+(((tex2D(_CloudTex,TRANSFORM_TEX(node_2184.rg, _CloudTex)).a*node_465)*4.0)+((1.0 - node_465)*tex2D(_CloudTex2,TRANSFORM_TEX(node_2184.rg, _CloudTex2)).a))),100.0),0.2,1) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
