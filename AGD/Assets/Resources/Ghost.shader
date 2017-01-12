// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32978,y:32731,varname:node_2865,prsc:2|emission-3537-OUT;n:type:ShaderForge.SFN_Fresnel,id:7453,x:31737,y:32747,varname:node_7453,prsc:2|EXP-8509-OUT;n:type:ShaderForge.SFN_Slider,id:8509,x:31318,y:32819,ptovrint:False,ptlb:Edge Blend,ptin:_EdgeBlend,varname:node_8509,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.5,max:5;n:type:ShaderForge.SFN_Color,id:1173,x:31692,y:32575,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_1173,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0,c3:0,c4:0;n:type:ShaderForge.SFN_Tex2d,id:6202,x:32064,y:32441,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-3323-UVOUT;n:type:ShaderForge.SFN_Panner,id:3323,x:31941,y:32117,varname:node_3323,prsc:2,spu:1,spv:1|UVIN-7718-UVOUT,DIST-9666-OUT;n:type:ShaderForge.SFN_TexCoord,id:7718,x:31670,y:32108,varname:node_7718,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9666,x:31755,y:32312,varname:node_9666,prsc:2|A-475-T,B-2964-OUT;n:type:ShaderForge.SFN_Time,id:475,x:31405,y:32217,varname:node_475,prsc:2;n:type:ShaderForge.SFN_Slider,id:2964,x:31224,y:32461,ptovrint:False,ptlb:Pan Speed,ptin:_PanSpeed,varname:_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_DepthBlend,id:6719,x:32394,y:33150,varname:node_6719,prsc:2|DIST-1061-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1061,x:31985,y:33312,ptovrint:False,ptlb:DepthBelnd,ptin:_DepthBelnd,varname:node_1061,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:3537,x:32628,y:32991,varname:node_3537,prsc:2|A-673-OUT,B-6719-OUT,C-4787-OUT;n:type:ShaderForge.SFN_Multiply,id:1105,x:32122,y:33024,varname:node_1105,prsc:2|A-1173-RGB,B-9201-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9201,x:31863,y:33171,ptovrint:False,ptlb:SoftColour,ptin:_SoftColour,varname:node_9201,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Lerp,id:673,x:32375,y:32902,varname:node_673,prsc:2|A-1105-OUT,B-1173-RGB,T-7453-OUT;n:type:ShaderForge.SFN_Multiply,id:4787,x:32600,y:32533,varname:node_4787,prsc:2|A-527-OUT,B-7453-OUT;n:type:ShaderForge.SFN_Vector1,id:7954,x:32209,y:32721,varname:node_7954,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:527,x:32396,y:32620,varname:node_527,prsc:2|A-6202-RGB,B-7954-OUT;proporder:8509-1173-6202-2964-1061-9201;pass:END;sub:END;*/

Shader "Shader Forge/Ghost" {
    Properties {
        _EdgeBlend ("Edge Blend", Range(0, 5)) = 2.5
        [HDR]_Colour ("Colour", Color) = (0.5,0,0,0)
        _Texture ("Texture", 2D) = "black" {}
        _PanSpeed ("Pan Speed", Range(0, 1)) = 0.1
        _DepthBelnd ("DepthBelnd", Float ) = 1
        _SoftColour ("SoftColour", Float ) = 0.2
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform float _EdgeBlend;
            uniform float4 _Colour;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _PanSpeed;
            uniform float _DepthBelnd;
            uniform float _SoftColour;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float node_7453 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeBlend);
                float4 node_475 = _Time + _TimeEditor;
                float2 node_3323 = (i.uv0+(node_475.g*_PanSpeed)*float2(1,1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_3323, _Texture));
                float node_7954 = 0.2;
                float3 emissive = (lerp((_Colour.rgb*_SoftColour),_Colour.rgb,node_7453)*saturate((sceneZ-partZ)/_DepthBelnd)*((_Texture_var.rgb*node_7954)*node_7453));
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
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform float _EdgeBlend;
            uniform float4 _Colour;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _PanSpeed;
            uniform float _DepthBelnd;
            uniform float _SoftColour;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_7453 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeBlend);
                float4 node_475 = _Time + _TimeEditor;
                float2 node_3323 = (i.uv0+(node_475.g*_PanSpeed)*float2(1,1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_3323, _Texture));
                float node_7954 = 0.2;
                o.Emission = (lerp((_Colour.rgb*_SoftColour),_Colour.rgb,node_7453)*saturate((sceneZ-partZ)/_DepthBelnd)*((_Texture_var.rgb*node_7954)*node_7453));
                
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
