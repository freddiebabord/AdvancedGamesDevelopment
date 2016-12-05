// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.4632353,fgcg:0.4632353,fgcb:0.4632353,fgca:1,fgde:0.05,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:34556,y:32587,varname:node_4795,prsc:2|emission-288-OUT,alpha-2549-OUT,clip-2549-OUT,voffset-1245-OUT;n:type:ShaderForge.SFN_Color,id:6658,x:32746,y:32742,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_9618,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:3,c3:0.1724138,c4:1;n:type:ShaderForge.SFN_Multiply,id:1823,x:32018,y:32996,varname:node_1823,prsc:2|A-9471-OUT,B-1925-OUT,C-3140-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9471,x:31756,y:32934,ptovrint:False,ptlb:Count,ptin:_Count,varname:node_9471,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_Sin,id:4867,x:32239,y:32996,varname:node_4867,prsc:2|IN-1823-OUT;n:type:ShaderForge.SFN_RemapRange,id:4403,x:32437,y:32996,varname:node_4403,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-4867-OUT;n:type:ShaderForge.SFN_Clamp01,id:6129,x:32626,y:32996,varname:node_6129,prsc:2|IN-4403-OUT;n:type:ShaderForge.SFN_Tau,id:1925,x:31789,y:33015,varname:node_1925,prsc:2;n:type:ShaderForge.SFN_Ceil,id:9268,x:32876,y:32976,varname:node_9268,prsc:2|IN-6129-OUT;n:type:ShaderForge.SFN_Multiply,id:5337,x:33073,y:32754,varname:node_5337,prsc:2|A-6658-RGB,B-9268-OUT;n:type:ShaderForge.SFN_Time,id:3039,x:31115,y:33121,varname:node_3039,prsc:2;n:type:ShaderForge.SFN_Add,id:3140,x:31756,y:33159,varname:node_3140,prsc:2|A-6189-OUT,B-3139-OUT;n:type:ShaderForge.SFN_Multiply,id:6189,x:31426,y:33154,varname:node_6189,prsc:2|A-3039-T,B-822-OUT;n:type:ShaderForge.SFN_ValueProperty,id:822,x:31115,y:33286,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.1;n:type:ShaderForge.SFN_ComponentMask,id:446,x:32430,y:33327,varname:node_446,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3099-X;n:type:ShaderForge.SFN_Time,id:9495,x:32430,y:33508,varname:node_9495,prsc:2;n:type:ShaderForge.SFN_Add,id:3116,x:32653,y:33378,varname:node_3116,prsc:2|A-446-OUT,B-9495-TSL;n:type:ShaderForge.SFN_Multiply,id:8103,x:32903,y:33343,varname:node_8103,prsc:2|A-2713-OUT,B-3116-OUT,C-7726-OUT;n:type:ShaderForge.SFN_Tau,id:7726,x:32678,y:33516,varname:node_7726,prsc:2;n:type:ShaderForge.SFN_Sin,id:9729,x:33134,y:33343,varname:node_9729,prsc:2|IN-8103-OUT;n:type:ShaderForge.SFN_RemapRange,id:3944,x:33332,y:33343,varname:node_3944,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-9729-OUT;n:type:ShaderForge.SFN_Clamp01,id:8246,x:33517,y:33324,varname:node_8246,prsc:2|IN-3944-OUT;n:type:ShaderForge.SFN_NormalVector,id:1739,x:33517,y:33483,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:4932,x:33828,y:33383,varname:node_4932,prsc:2|A-8246-OUT,B-1739-OUT,C-4957-OUT;n:type:ShaderForge.SFN_Vector1,id:4957,x:33690,y:33622,varname:node_4957,prsc:2,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:2713,x:32653,y:33214,ptovrint:False,ptlb:Fuzzyness,ptin:_Fuzzyness,varname:node_2713,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_FragmentPosition,id:3099,x:32164,y:33321,varname:node_3099,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:5876,x:30523,y:33640,varname:node_5876,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:3109,x:30729,y:33640,varname:node_3109,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5876-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:9937,x:30943,y:33640,varname:node_9937,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3109-OUT;n:type:ShaderForge.SFN_Ceil,id:1706,x:32704,y:33693,varname:node_1706,prsc:2|IN-9496-OUT;n:type:ShaderForge.SFN_Multiply,id:1245,x:34149,y:33460,varname:node_1245,prsc:2|A-4932-OUT,B-1706-OUT;n:type:ShaderForge.SFN_Multiply,id:288,x:33791,y:32753,varname:node_288,prsc:2|A-5337-OUT,B-1706-OUT;n:type:ShaderForge.SFN_Multiply,id:2549,x:33810,y:33110,varname:node_2549,prsc:2|A-9268-OUT,B-1706-OUT;n:type:ShaderForge.SFN_Slider,id:8682,x:31916,y:33600,ptovrint:False,ptlb:PercentageComplete,ptin:_PercentageComplete,varname:node_8682,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7359442,max:1;n:type:ShaderForge.SFN_Multiply,id:3139,x:31245,y:33673,varname:node_3139,prsc:2|A-9937-G,B-9937-G;n:type:ShaderForge.SFN_RemapRange,id:7074,x:31651,y:33709,varname:node_7074,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-3139-OUT;n:type:ShaderForge.SFN_Subtract,id:9496,x:32406,y:33702,varname:node_9496,prsc:2|A-8682-OUT,B-7074-OUT;proporder:6658-9471-822-2713-8682;pass:END;sub:END;*/

Shader "Shader Forge/CaptureSphere" {
    Properties {
        [HDR]_Colour ("Colour", Color) = (0,3,0.1724138,1)
        _Count ("Count", Float ) = 8
        _Speed ("Speed", Float ) = -0.1
        _Fuzzyness ("Fuzzyness", Float ) = 1
        _PercentageComplete ("PercentageComplete", Range(0, 1)) = 0.7359442
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
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform float4 _Colour;
            uniform float _Count;
            uniform float _Speed;
            uniform float _Fuzzyness;
            uniform float _PercentageComplete;
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
                float4 node_9495 = _Time + _TimeEditor;
                float2 node_9937 = (o.uv0*2.0+-1.0).rg;
                float node_3139 = (node_9937.g*node_9937.g);
                float node_1706 = ceil((_PercentageComplete-(node_3139*0.5+0.5)));
                v.vertex.xyz += ((saturate((sin((_Fuzzyness*(mul(unity_ObjectToWorld, v.vertex).r.r+node_9495.r)*6.28318530718))*0.5+0.5))*v.normal*0.1)*node_1706);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 node_3039 = _Time + _TimeEditor;
                float2 node_9937 = (i.uv0*2.0+-1.0).rg;
                float node_3139 = (node_9937.g*node_9937.g);
                float node_9268 = ceil(saturate((sin((_Count*6.28318530718*((node_3039.g*_Speed)+node_3139)))*2.0+-1.0)));
                float node_1706 = ceil((_PercentageComplete-(node_3139*0.5+0.5)));
                float node_2549 = (node_9268*node_1706);
                clip(node_2549 - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = ((_Colour.rgb*node_9268)*node_1706);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_2549);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.4632353,0.4632353,0.4632353,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
