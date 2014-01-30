 Shader "GUI/Textured Text Shader Overlay" 
 { 
    Properties { 
       _MainTex ("Font Texture", 2D) = "white" {} 
       _Color ("Text Color", Color) = (1,1,1,1) 
    } 
     
    SubShader { 
       Lighting Off 
       cull off 
       
       ZTest Always 
       ZWrite Off 
       
       Fog { Mode Off } 
       Tags {"Queue" = "Overlay+1" } 
       Pass { 
          Blend SrcAlpha OneMinusSrcAlpha 
          Color [_Color]
          SetTexture [_MainTex] {
             Combine primary, texture * primary 
          } 
       } 
    } 
 }

