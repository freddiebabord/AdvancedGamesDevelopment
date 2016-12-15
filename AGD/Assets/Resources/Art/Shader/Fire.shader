// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33273,y:32472,varname:node_4795,prsc:2|emission-2393-OUT,alpha-1738-R;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32079,y:32627,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-5839-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32939,y:32420,varname:node_2393,prsc:2|A-797-RGB,B-1738-OUT;n:type:ShaderForge.SFN_Color,id:797,x:32628,y:32273,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.9586205,c4:1;n:type:ShaderForge.SFN_Tex2d,id:5227,x:32068,y:32392,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0000000000000000f000000000000000,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:2791,x:32343,y:32467,varname:node_2791,prsc:2,blmd:1,clmp:True|SRC-5227-RGB,DST-6074-RGB;n:type:ShaderForge.SFN_Panner,id:5839,x:31746,y:32598,varname:node_5839,prsc:2,spu:0,spv:-1|UVIN-1676-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1676,x:31527,y:32598,varname:node_1676,prsc:2,uv:0;n:type:ShaderForge.SFN_Clamp01,id:4031,x:32524,y:32467,varname:node_4031,prsc:2|IN-2791-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1738,x:32739,y:32467,varname:node_1738,prsc:2,cc1:0,cc2:0,cc3:0,cc4:-1|IN-4031-OUT;proporder:6074-797-5227;pass:END;sub:END;*/

Shader "Shader Forge/Fire" {
    Properties {
        _MainTex ("MainTex", 2D) = "black" {}
        [HDR]_TintColor ("Color", Color) = (0,1,0.9586205,1)
        _Mask ("Mask", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 node_362 = _Time + _TimeEditor;
                float2 node_5839 = (i.uv0+node_362.g*float2(0,-1));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_5839, _MainTex));
                float3 node_4031 = saturate(saturate((_Mask_var.rgb*_MainTex_var.rgb)));
                float3 node_1738 = node_4031.rrr;
                float3 emissive = (_TintColor.rgb*node_1738);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_1738.r);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
