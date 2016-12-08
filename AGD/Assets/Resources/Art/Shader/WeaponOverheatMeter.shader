// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:1,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.4632353,fgcg:0.4632353,fgcb:0.4632353,fgca:1,fgde:0.05,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:33626,y:32559,varname:node_2865,prsc:2|emission-9464-OUT,alpha-8942-OUT,clip-8942-OUT;n:type:ShaderForge.SFN_TexCoord,id:8765,x:31105,y:32989,varname:node_8765,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:9452,x:31311,y:32989,varname:node_9452,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8765-UVOUT;n:type:ShaderForge.SFN_Color,id:256,x:31957,y:32535,ptovrint:False,ptlb:EmptyColour,ptin:_EmptyColour,varname:node_256,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:3,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:9618,x:31957,y:32356,ptovrint:False,ptlb:FullColour,ptin:_FullColour,varname:node_9618,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:3,c3:0.1724138,c4:1;n:type:ShaderForge.SFN_Lerp,id:5260,x:32373,y:32566,varname:node_5260,prsc:2|A-9618-RGB,B-256-RGB,T-6400-OUT;n:type:ShaderForge.SFN_Length,id:6844,x:31651,y:33047,varname:node_6844,prsc:2|IN-9452-OUT;n:type:ShaderForge.SFN_Floor,id:1838,x:32172,y:32959,varname:node_1838,prsc:2|IN-6844-OUT;n:type:ShaderForge.SFN_OneMinus,id:7112,x:32386,y:32959,varname:node_7112,prsc:2|IN-1838-OUT;n:type:ShaderForge.SFN_Floor,id:2411,x:32386,y:33095,varname:node_2411,prsc:2|IN-6455-OUT;n:type:ShaderForge.SFN_Add,id:6455,x:32187,y:33114,varname:node_6455,prsc:2|A-6844-OUT,B-8980-OUT;n:type:ShaderForge.SFN_Multiply,id:7369,x:32764,y:32862,varname:node_7369,prsc:2|A-4108-OUT,B-7112-OUT,C-2411-OUT;n:type:ShaderForge.SFN_ArcTan2,id:7253,x:31852,y:32812,varname:node_7253,prsc:2,attp:2|A-7798-G,B-7798-R;n:type:ShaderForge.SFN_ComponentMask,id:7798,x:31616,y:32812,varname:node_7798,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9452-OUT;n:type:ShaderForge.SFN_Ceil,id:8966,x:32229,y:32776,varname:node_8966,prsc:2|IN-8810-OUT;n:type:ShaderForge.SFN_Subtract,id:8810,x:32034,y:32776,varname:node_8810,prsc:2|A-6400-OUT,B-7253-OUT;n:type:ShaderForge.SFN_OneMinus,id:4108,x:32432,y:32776,varname:node_4108,prsc:2|IN-8966-OUT;n:type:ShaderForge.SFN_Slider,id:8980,x:31800,y:33189,ptovrint:False,ptlb:Width,ptin:_Width,varname:node_8980,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.1,max:0.5;n:type:ShaderForge.SFN_Multiply,id:2493,x:32764,y:33012,varname:node_2493,prsc:2|A-8966-OUT,B-7112-OUT,C-2411-OUT;n:type:ShaderForge.SFN_Multiply,id:9464,x:33004,y:32529,varname:node_9464,prsc:2|A-5260-OUT,B-7369-OUT;n:type:ShaderForge.SFN_Add,id:8942,x:33129,y:32990,varname:node_8942,prsc:2|A-7369-OUT,B-2493-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6400,x:31616,y:32666,ptovrint:False,ptlb:CurrentOverheatValue,ptin:_CurrentOverheatValue,varname:node_6400,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Color,id:2179,x:32612,y:32950,ptovrint:False,ptlb:Color_copy_copy,ptin:_Color_copy_copy,varname:_Color_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;proporder:6400-256-9618-8980;pass:END;sub:END;*/

Shader "Shader Forge/WeaponOverheatMeter" {
    Properties {
        _CurrentOverheatValue ("CurrentOverheatValue", Float ) = 0
        [HDR]_EmptyColour ("EmptyColour", Color) = (3,0,0,1)
        [HDR]_FullColour ("FullColour", Color) = (0,3,0.1724138,1)
        _Width ("Width", Range(0.1, 0.5)) = 0.1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "DEFERRED"
            Tags {
                "LightMode"="Deferred"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_DEFERRED
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile ___ UNITY_HDR_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _EmptyColour;
            uniform float4 _FullColour;
            uniform float _Width;
            uniform float _CurrentOverheatValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            void frag(
                VertexOutput i,
                out half4 outDiffuse : SV_Target0,
                out half4 outSpecSmoothness : SV_Target1,
                out half4 outNormal : SV_Target2,
                out half4 outEmission : SV_Target3 )
            {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 node_9452 = (i.uv0*2.0+-1.0);
                float2 node_7798 = node_9452.rg;
                float node_8966 = ceil((_CurrentOverheatValue-((atan2(node_7798.g,node_7798.r)/6.28318530718)+0.5)));
                float node_6844 = length(node_9452);
                float node_7112 = (1.0 - floor(node_6844));
                float node_2411 = floor((node_6844+_Width));
                float node_7369 = ((1.0 - node_8966)*node_7112*node_2411);
                float node_8942 = (node_7369+(node_8966*node_7112*node_2411));
                clip(node_8942 - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (lerp(_FullColour.rgb,_EmptyColour.rgb,_CurrentOverheatValue)*node_7369);
                float3 finalColor = emissive;
                outDiffuse = half4( 0, 0, 0, 1 );
                outSpecSmoothness = half4(0,0,0,0);
                outNormal = half4( normalDirection * 0.5 + 0.5, 1 );
                outEmission = half4( (lerp(_FullColour.rgb,_EmptyColour.rgb,_CurrentOverheatValue)*node_7369), 1 );
                #ifndef UNITY_HDR_ON
                    outEmission.rgb = exp2(-outEmission.rgb);
                #endif
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
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
            uniform float4 _EmptyColour;
            uniform float4 _FullColour;
            uniform float _Width;
            uniform float _CurrentOverheatValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float2 node_9452 = (i.uv0*2.0+-1.0);
                float2 node_7798 = node_9452.rg;
                float node_8966 = ceil((_CurrentOverheatValue-((atan2(node_7798.g,node_7798.r)/6.28318530718)+0.5)));
                float node_6844 = length(node_9452);
                float node_7112 = (1.0 - floor(node_6844));
                float node_2411 = floor((node_6844+_Width));
                float node_7369 = ((1.0 - node_8966)*node_7112*node_2411);
                float node_8942 = (node_7369+(node_8966*node_7112*node_2411));
                clip(node_8942 - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (lerp(_FullColour.rgb,_EmptyColour.rgb,_CurrentOverheatValue)*node_7369);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_8942);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Width;
            uniform float _CurrentOverheatValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_9452 = (i.uv0*2.0+-1.0);
                float2 node_7798 = node_9452.rg;
                float node_8966 = ceil((_CurrentOverheatValue-((atan2(node_7798.g,node_7798.r)/6.28318530718)+0.5)));
                float node_6844 = length(node_9452);
                float node_7112 = (1.0 - floor(node_6844));
                float node_2411 = floor((node_6844+_Width));
                float node_7369 = ((1.0 - node_8966)*node_7112*node_2411);
                float node_8942 = (node_7369+(node_8966*node_7112*node_2411));
                clip(node_8942 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
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
            uniform float4 _EmptyColour;
            uniform float4 _FullColour;
            uniform float _Width;
            uniform float _CurrentOverheatValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float2 node_9452 = (i.uv0*2.0+-1.0);
                float2 node_7798 = node_9452.rg;
                float node_8966 = ceil((_CurrentOverheatValue-((atan2(node_7798.g,node_7798.r)/6.28318530718)+0.5)));
                float node_6844 = length(node_9452);
                float node_7112 = (1.0 - floor(node_6844));
                float node_2411 = floor((node_6844+_Width));
                float node_7369 = ((1.0 - node_8966)*node_7112*node_2411);
                o.Emission = (lerp(_FullColour.rgb,_EmptyColour.rgb,_CurrentOverheatValue)*node_7369);
                
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
