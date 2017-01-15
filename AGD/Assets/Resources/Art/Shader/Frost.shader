// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:1,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:6,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:2865,x:32538,y:33044,varname:node_2865,prsc:2|normal-8642-RGB,emission-8678-OUT,voffset-4177-OUT;n:type:ShaderForge.SFN_TexCoord,id:6793,x:31733,y:33224,varname:node_6793,prsc:2,uv:0;n:type:ShaderForge.SFN_ProjectionParameters,id:8707,x:31733,y:33437,varname:node_8707,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:9496,x:31932,y:33224,varname:node_9496,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-6793-UVOUT;n:type:ShaderForge.SFN_Append,id:6538,x:31932,y:33396,varname:node_6538,prsc:2|A-5896-OUT,B-8707-SGN;n:type:ShaderForge.SFN_Vector1,id:5896,x:31733,y:33378,varname:node_5896,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:4177,x:32105,y:33315,varname:node_4177,prsc:2|A-9496-OUT,B-6538-OUT;n:type:ShaderForge.SFN_Color,id:4354,x:31735,y:32772,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4354,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:4893,x:31735,y:32980,ptovrint:False,ptlb:node_4893,ptin:_node_4893,varname:node_4893,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:210743b88c99bcf45b28c3a4a4a3e178,ntxv:0,isnm:False|UVIN-9496-OUT;n:type:ShaderForge.SFN_Multiply,id:2368,x:31997,y:32939,varname:node_2368,prsc:2|A-4354-RGB,B-4893-RGB;n:type:ShaderForge.SFN_Length,id:7826,x:32211,y:32682,varname:node_7826,prsc:2|IN-9496-OUT;n:type:ShaderForge.SFN_Slider,id:7134,x:32111,y:32870,ptovrint:False,ptlb:node_7134,ptin:_node_7134,varname:node_7134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:5890,x:32480,y:32802,varname:node_5890,prsc:2|A-935-OUT,B-7134-OUT;n:type:ShaderForge.SFN_Subtract,id:935,x:32417,y:32640,varname:node_935,prsc:2|A-7826-OUT,B-292-OUT;n:type:ShaderForge.SFN_Vector1,id:292,x:32190,y:32625,varname:node_292,prsc:2,v1:1.25;n:type:ShaderForge.SFN_Multiply,id:8678,x:32245,y:33012,varname:node_8678,prsc:2|A-5890-OUT,B-2368-OUT;n:type:ShaderForge.SFN_Tex2d,id:8642,x:32269,y:33170,ptovrint:False,ptlb:node_8642,ptin:_node_8642,varname:node_8642,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:065247d136abaa042a330ab6a7b030fc,ntxv:3,isnm:True|UVIN-9496-OUT;proporder:4354-4893-7134-8642;pass:END;sub:END;*/

Shader "Shader Forge/Frost" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _node_4893 ("node_4893", 2D) = "white" {}
        _node_7134 ("node_7134", Range(0, 1)) = 1
        _node_8642 ("node_8642", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZTest Always
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
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _node_4893; uniform float4 _node_4893_ST;
            uniform float _node_7134;
            uniform sampler2D _node_8642; uniform float4 _node_8642_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float2 node_9496 = (o.uv0*2.0+-1.0);
                v.vertex.xyz = float3((node_9496*float2(1.0,_ProjectionParams.r)),0.0);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = v.vertex;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_9496 = (i.uv0*2.0+-1.0);
                float3 _node_8642_var = UnpackNormal(tex2D(_node_8642,TRANSFORM_TEX(node_9496, _node_8642)));
                float3 normalLocal = _node_8642_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 _node_4893_var = tex2D(_node_4893,TRANSFORM_TEX(node_9496, _node_4893));
                float3 emissive = (((length(node_9496)-1.25)+_node_7134)*(_Color.rgb*_node_4893_var.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _node_4893; uniform float4 _node_4893_ST;
            uniform float _node_7134;
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
                float2 node_9496 = (o.uv0*2.0+-1.0);
                v.vertex.xyz = float3((node_9496*float2(1.0,_ProjectionParams.r)),0.0);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float2 node_9496 = (i.uv0*2.0+-1.0);
                float4 _node_4893_var = tex2D(_node_4893,TRANSFORM_TEX(node_9496, _node_4893));
                o.Emission = (((length(node_9496)-1.25)+_node_7134)*(_Color.rgb*_node_4893_var.rgb));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
