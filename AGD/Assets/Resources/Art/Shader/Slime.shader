// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33095,y:32839,varname:node_4795,prsc:2|emission-7628-OUT,voffset-6937-OUT;n:type:ShaderForge.SFN_Fresnel,id:6070,x:31801,y:32811,varname:node_6070,prsc:2|EXP-1890-OUT;n:type:ShaderForge.SFN_Slider,id:1890,x:31382,y:32883,ptovrint:False,ptlb:Edge Blend,ptin:_EdgeBlend,varname:node_8509,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.5,max:5;n:type:ShaderForge.SFN_Color,id:6138,x:31756,y:32639,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_1173,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0,c3:0,c4:0;n:type:ShaderForge.SFN_Tex2d,id:8179,x:32128,y:32505,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-4641-UVOUT;n:type:ShaderForge.SFN_Panner,id:4641,x:32005,y:32181,varname:node_4641,prsc:2,spu:1,spv:1|UVIN-1661-UVOUT,DIST-5244-OUT;n:type:ShaderForge.SFN_TexCoord,id:1661,x:31734,y:32172,varname:node_1661,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:5244,x:31819,y:32376,varname:node_5244,prsc:2|A-7638-T,B-6282-OUT;n:type:ShaderForge.SFN_Time,id:7638,x:31469,y:32281,varname:node_7638,prsc:2;n:type:ShaderForge.SFN_Slider,id:6282,x:31288,y:32525,ptovrint:False,ptlb:Pan Speed,ptin:_PanSpeed,varname:_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_DepthBlend,id:6624,x:32458,y:33214,varname:node_6624,prsc:2|DIST-3553-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3553,x:32049,y:33376,ptovrint:False,ptlb:DepthBelnd,ptin:_DepthBelnd,varname:node_1061,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:7628,x:32692,y:33055,varname:node_7628,prsc:2|A-8454-OUT,B-6624-OUT,C-7511-OUT;n:type:ShaderForge.SFN_Multiply,id:1257,x:32186,y:33088,varname:node_1257,prsc:2|A-6138-RGB,B-5367-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5367,x:31927,y:33235,ptovrint:False,ptlb:SoftColour,ptin:_SoftColour,varname:node_9201,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Lerp,id:8454,x:32439,y:32966,varname:node_8454,prsc:2|A-1257-OUT,B-6138-RGB,T-6070-OUT;n:type:ShaderForge.SFN_Multiply,id:7511,x:32664,y:32597,varname:node_7511,prsc:2|A-3172-OUT,B-6070-OUT;n:type:ShaderForge.SFN_Vector1,id:234,x:32273,y:32785,varname:node_234,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:3172,x:32460,y:32684,varname:node_3172,prsc:2|A-8179-RGB,B-234-OUT;n:type:ShaderForge.SFN_Tex2d,id:6752,x:32482,y:33700,varname:node_6752,prsc:2,ntxv:0,isnm:False|UVIN-5719-UVOUT,TEX-6107-TEX;n:type:ShaderForge.SFN_Time,id:4738,x:31723,y:33831,varname:node_4738,prsc:2;n:type:ShaderForge.SFN_Panner,id:5719,x:32305,y:33707,varname:node_5719,prsc:2,spu:1,spv:1|UVIN-1586-UVOUT,DIST-5521-OUT;n:type:ShaderForge.SFN_TexCoord,id:1586,x:31723,y:33663,varname:node_1586,prsc:2,uv:0;n:type:ShaderForge.SFN_Slider,id:8648,x:31604,y:33996,ptovrint:False,ptlb:NoiseTime,ptin:_NoiseTime,varname:node_8648,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:5521,x:31959,y:33819,varname:node_5521,prsc:2|A-4738-TSL,B-8648-OUT;n:type:ShaderForge.SFN_NormalVector,id:1800,x:32493,y:33420,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:6937,x:32792,y:33489,varname:node_6937,prsc:2|A-1800-OUT,B-5224-OUT;n:type:ShaderForge.SFN_Multiply,id:64,x:32792,y:33722,varname:node_64,prsc:2|A-6752-R,B-2803-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2803,x:32469,y:33895,ptovrint:False,ptlb:NoiseScale,ptin:_NoiseScale,varname:node_2803,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;n:type:ShaderForge.SFN_Panner,id:8590,x:32310,y:33968,varname:node_8590,prsc:2,spu:-1,spv:-1|UVIN-1586-UVOUT,DIST-5521-OUT;n:type:ShaderForge.SFN_Tex2d,id:1265,x:32548,y:34001,varname:_Noise_copy,prsc:2,ntxv:0,isnm:False|UVIN-8590-UVOUT,TEX-6107-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:6107,x:32003,y:34097,ptovrint:False,ptlb:NoiseTexture,ptin:_NoiseTexture,varname:node_6107,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6152,x:32769,y:33939,varname:node_6152,prsc:2|A-1265-R,B-2803-OUT;n:type:ShaderForge.SFN_Multiply,id:5224,x:33086,y:33863,varname:node_5224,prsc:2|A-64-OUT,B-6152-OUT;proporder:1890-6138-8179-3553-5367-6282-8648-2803-6107;pass:END;sub:END;*/

Shader "Shader Forge/GhostDemo" {
    Properties {
        _EdgeBlend ("Edge Blend", Range(0, 5)) = 2.5
        [HDR]_Colour ("Colour", Color) = (0.5,0,0,0)
        _Texture ("Texture", 2D) = "black" {}
        _DepthBelnd ("DepthBelnd", Float ) = 1
        _SoftColour ("SoftColour", Float ) = 0.2
        _PanSpeed ("Pan Speed", Range(0, 1)) = 0.1
        _NoiseTime ("NoiseTime", Range(0, 1)) = 0
        _NoiseScale ("NoiseScale", Float ) = 0.25
        _NoiseTexture ("NoiseTexture", 2D) = "white" {}
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
            #pragma glsl
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform float _EdgeBlend;
            uniform float4 _Colour;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _PanSpeed;
            uniform float _DepthBelnd;
            uniform float _SoftColour;
            uniform float _NoiseTime;
            uniform float _NoiseScale;
            uniform sampler2D _NoiseTexture; uniform float4 _NoiseTexture_ST;
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
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_4738 = _Time + _TimeEditor;
                float node_5521 = (node_4738.r*_NoiseTime);
                float2 node_5719 = (o.uv0+node_5521*float2(1,1));
                float4 node_6752 = tex2Dlod(_NoiseTexture,float4(TRANSFORM_TEX(node_5719, _NoiseTexture),0.0,0));
                float node_64 = (node_6752.r*_NoiseScale);
                float2 node_8590 = (o.uv0+node_5521*float2(-1,-1));
                float4 _Noise_copy = tex2Dlod(_NoiseTexture,float4(TRANSFORM_TEX(node_8590, _NoiseTexture),0.0,0));
                v.vertex.xyz += (v.normal*(node_64*(_Noise_copy.r*_NoiseScale)));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float node_6070 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeBlend);
                float4 node_7638 = _Time + _TimeEditor;
                float2 node_4641 = (i.uv0+(node_7638.g*_PanSpeed)*float2(1,1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_4641, _Texture));
                float3 emissive = (lerp((_Colour.rgb*_SoftColour),_Colour.rgb,node_6070)*saturate((sceneZ-partZ)/_DepthBelnd)*((_Texture_var.rgb*0.2)*node_6070));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
