// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|emission-4127-OUT;n:type:ShaderForge.SFN_Multiply,id:2837,x:32354,y:32576,varname:node_2837,prsc:2|A-6202-RGB,B-4127-OUT;n:type:ShaderForge.SFN_Multiply,id:4127,x:31962,y:32639,varname:node_4127,prsc:2|A-1173-RGB,B-7453-OUT;n:type:ShaderForge.SFN_Fresnel,id:7453,x:31755,y:32743,varname:node_7453,prsc:2|EXP-8509-OUT;n:type:ShaderForge.SFN_Slider,id:8509,x:31318,y:32819,ptovrint:False,ptlb:Edge Blend,ptin:_EdgeBlend,varname:node_8509,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.5,max:5;n:type:ShaderForge.SFN_Color,id:1173,x:31692,y:32575,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_1173,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0,c3:0,c4:0;n:type:ShaderForge.SFN_Tex2d,id:6202,x:32128,y:32441,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a181ca8e3c62f3e4b8f183f6c586b032,ntxv:2,isnm:False|UVIN-3323-UVOUT;n:type:ShaderForge.SFN_Panner,id:3323,x:31941,y:32117,varname:node_3323,prsc:2,spu:1,spv:1|UVIN-7718-UVOUT,DIST-9666-OUT;n:type:ShaderForge.SFN_TexCoord,id:7718,x:31670,y:32108,varname:node_7718,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9666,x:31755,y:32312,varname:node_9666,prsc:2|A-475-T,B-2964-OUT;n:type:ShaderForge.SFN_Time,id:475,x:31405,y:32217,varname:node_475,prsc:2;n:type:ShaderForge.SFN_Slider,id:2964,x:31224,y:32461,ptovrint:False,ptlb:Pan Speed,ptin:_PanSpeed,varname:_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;proporder:8509-1173-6202-2964;pass:END;sub:END;*/

Shader "Shader Forge/Ghost" {
    Properties {
        _EdgeBlend ("Edge Blend", Range(0, 5)) = 2.5
        [HDR]_Colour ("Colour", Color) = (0.5,0,0,0)
        _Texture ("Texture", 2D) = "black" {}
        _PanSpeed ("Pan Speed", Range(0, 1)) = 0.1
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
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _EdgeBlend;
            uniform float4 _Colour;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float3 node_4127 = (_Colour.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeBlend));
                float3 emissive = node_4127;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _EdgeBlend;
            uniform float4 _Colour;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float3 node_4127 = (_Colour.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeBlend));
                o.Emission = node_4127;
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
