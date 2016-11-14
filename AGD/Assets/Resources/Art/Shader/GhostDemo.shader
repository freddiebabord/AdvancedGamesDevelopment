// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32988,y:32722,varname:node_4795,prsc:2|emission-5358-OUT;n:type:ShaderForge.SFN_Fresnel,id:2785,x:32034,y:32971,varname:node_2785,prsc:2|EXP-6893-OUT;n:type:ShaderForge.SFN_Slider,id:6893,x:31663,y:33093,ptovrint:False,ptlb:EdgeBlend,ptin:_EdgeBlend,varname:node_6893,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4683482,max:5;n:type:ShaderForge.SFN_Color,id:3814,x:32034,y:32808,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_3814,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.8344827,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2955,x:32274,y:32871,varname:node_2955,prsc:2|A-3814-RGB,B-2785-OUT;n:type:ShaderForge.SFN_Multiply,id:5358,x:32689,y:32952,varname:node_5358,prsc:2|A-176-OUT,B-3515-OUT;n:type:ShaderForge.SFN_Slider,id:9414,x:31985,y:33149,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_9414,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:87,x:32123,y:33225,varname:node_87,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:3515,x:32338,y:33123,varname:node_3515,prsc:2|A-9414-OUT,B-87-OUT;n:type:ShaderForge.SFN_Tex2d,id:6539,x:32034,y:32604,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6539,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5ecc7b48572fee747a7ea20fa129308b,ntxv:2,isnm:False|UVIN-4710-UVOUT;n:type:ShaderForge.SFN_Multiply,id:176,x:32469,y:32850,varname:node_176,prsc:2|A-6539-RGB,B-2955-OUT;n:type:ShaderForge.SFN_Time,id:4078,x:31546,y:32633,varname:node_4078,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:1632,x:31546,y:32476,varname:node_1632,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4710,x:31785,y:32564,varname:node_4710,prsc:2,spu:1,spv:1|UVIN-1632-UVOUT,DIST-3421-OUT;n:type:ShaderForge.SFN_Multiply,id:3421,x:31716,y:32737,varname:node_3421,prsc:2|A-4078-T,B-7968-OUT;n:type:ShaderForge.SFN_Slider,id:7968,x:31297,y:32804,ptovrint:False,ptlb:TexturePanSpeed,ptin:_TexturePanSpeed,varname:node_7968,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.464874,max:1;proporder:6893-3814-9414-6539-7968;pass:END;sub:END;*/

Shader "Shader Forge/GhostDemo" {
    Properties {
        _EdgeBlend ("EdgeBlend", Range(0, 5)) = 0.4683482
        [HDR]_Colour ("Colour", Color) = (0,0.8344827,1,1)
        _Intensity ("Intensity", Range(0, 1)) = 1
        _Texture ("Texture", 2D) = "black" {}
        _TexturePanSpeed ("TexturePanSpeed", Range(0, 1)) = 0.464874
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
            uniform float _EdgeBlend;
            uniform float4 _Colour;
            uniform float _Intensity;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _TexturePanSpeed;
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
////// Lighting:
////// Emissive:
                float4 node_4078 = _Time + _TimeEditor;
                float2 node_4710 = (i.uv0+(node_4078.g*_TexturePanSpeed)*float2(1,1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_4710, _Texture));
                float3 node_176 = (_Texture_var.rgb*(_Colour.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeBlend)));
                float3 emissive = (node_176*(_Intensity*10.0));
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
