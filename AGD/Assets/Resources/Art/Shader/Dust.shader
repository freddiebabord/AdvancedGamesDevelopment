// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.4632353,fgcg:0.4632353,fgcb:0.4632353,fgca:1,fgde:0.05,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33590,y:32643,varname:node_4795,prsc:2|diff-2393-OUT,alpha-9949-A;n:type:ShaderForge.SFN_Multiply,id:2393,x:33376,y:32594,varname:node_2393,prsc:2|A-5152-OUT,B-797-RGB;n:type:ShaderForge.SFN_Color,id:797,x:33029,y:32623,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6029412,c2:0.6029412,c3:0.6029412,c4:1;n:type:ShaderForge.SFN_TexCoord,id:1303,x:31296,y:32177,varname:node_1303,prsc:2,uv:0;n:type:ShaderForge.SFN_Code,id:6279,x:32138,y:32127,varname:node_6279,prsc:2,code:ZgBsAG8AYQB0ACAAcgBlAHQAIAA9ACAAMAA7AA0ACgAgACAAaQBuAHQAIABpAHQAZQByAGEAdABpAG8AbgBzACAAPQAgADYAOwANAAoAIAAgAGYAbwByACAAKABpAG4AdAAgAGkAIAA9ACAAMAA7ACAAaQAgADwAIABpAHQAZQByAGEAdABpAG8AbgBzADsAIAArACsAaQApAA0ACgAgACAAewANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAyACAAcAAgAD0AIABmAGwAbwBvAHIAKABVAFYAIAAqACAAKABpACsAMQApACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAyACAAZgAgAD0AIABmAHIAYQBjACgAVQBWACAAKgAgACgAaQArADEAKQApADsADQAKACAAIAAgACAAIABmACAAPQAgAGYAIAAqACAAZgAgACoAIAAoADMALgAwACAALQAgADIALgAwACAAKgAgAGYAKQA7AA0ACgAgACAAIAAgACAAZgBsAG8AYQB0ACAAbgAgAD0AIABwAC4AeAAgACsAIABwAC4AeQAgACoAIAA1ADcALgAwADsADQAKACAAIAAgACAAIABmAGwAbwBhAHQANAAgAG4AbwBpAHMAZQAgAD0AIABmAGwAbwBhAHQANAAoAG4ALAAgAG4AIAArACAAMQAsACAAbgAgACsAIAA1ADcALgAwACwAIABuACAAKwAgADUAOAAuADAAKQA7AA0ACgAgACAAIAAgACAAbgBvAGkAcwBlACAAPQAgAGYAcgBhAGMAKABzAGkAbgAoAG4AbwBpAHMAZQApACoANAAzADcALgA1ADgANQA0ADUAMwApADsADQAKACAAIAAgACAAIAByAGUAdAAgACsAPQAgAGwAZQByAHAAKABsAGUAcgBwACgAbgBvAGkAcwBlAC4AeAAsACAAbgBvAGkAcwBlAC4AeQAsACAAZgAuAHgAKQAsACAAbABlAHIAcAAoAG4AbwBpAHMAZQAuAHoALAAgAG4AbwBpAHMAZQAuAHcALAAgAGYALgB4ACkALAAgAGYALgB5ACkAIAAqACAAKAAgAGkAdABlAHIAYQB0AGkAbwBuAHMAIAAvACAAKABpACsAMQApACkAOwANAAoAIAAgAH0ADQAKACAAIAByAGUAdAB1AHIAbgAgAHIAZQB0AC8AaQB0AGUAcgBhAHQAaQBvAG4AcwA7AA==,output:2,fname:noise,width:581,height:212,input:1,input_1_label:UV|A-6057-OUT;n:type:ShaderForge.SFN_Multiply,id:1052,x:33020,y:32119,varname:node_1052,prsc:2|A-6279-OUT,B-7020-OUT;n:type:ShaderForge.SFN_Vector1,id:7020,x:32758,y:32293,varname:node_7020,prsc:2,v1:0.5;n:type:ShaderForge.SFN_RemapRange,id:4891,x:31489,y:32159,varname:node_4891,prsc:2,frmn:0,frmx:0.2,tomn:0,tomx:1|IN-1303-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5152,x:33313,y:32333,varname:node_5152,prsc:2|A-1052-OUT,B-9949-RGB;n:type:ShaderForge.SFN_Tex2d,id:9949,x:32494,y:32630,ptovrint:False,ptlb:node_9949,ptin:_node_9949,varname:node_9949,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a345823fbeed94346a92425d34c78dec,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Panner,id:8742,x:31719,y:32159,varname:node_8742,prsc:2,spu:1,spv:1|UVIN-4891-OUT,DIST-2453-OUT;n:type:ShaderForge.SFN_Time,id:3846,x:31379,y:32431,varname:node_3846,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2453,x:31559,y:32420,varname:node_2453,prsc:2|A-3846-T,B-8436-OUT;n:type:ShaderForge.SFN_Vector1,id:8436,x:31379,y:32564,varname:node_8436,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Panner,id:895,x:31719,y:32339,varname:node_895,prsc:2,spu:-1,spv:-1|UVIN-4891-OUT,DIST-2453-OUT;n:type:ShaderForge.SFN_Multiply,id:6057,x:31944,y:32212,varname:node_6057,prsc:2|A-8742-UVOUT,B-895-UVOUT;proporder:797-9949;pass:END;sub:END;*/

Shader "Shader Forge/Dust" {
    Properties {
        _TintColor ("Color", Color) = (0.6029412,0.6029412,0.6029412,1)
        _node_9949 ("node_9949", 2D) = "black" {}
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
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            float3 noise( float2 UV ){
            float ret = 0;
              int iterations = 6;
              for (int i = 0; i < iterations; ++i)
              {
                 float2 p = floor(UV * (i+1));
                 float2 f = frac(UV * (i+1));
                 f = f * f * (3.0 - 2.0 * f);
                 float n = p.x + p.y * 57.0;
                 float4 noise = float4(n, n + 1, n + 57.0, n + 58.0);
                 noise = frac(sin(noise)*437.585453);
                 ret += lerp(lerp(noise.x, noise.y, f.x), lerp(noise.z, noise.w, f.x), f.y) * ( iterations / (i+1));
              }
              return ret/iterations;
            }
            
            uniform sampler2D _node_9949; uniform float4 _node_9949_ST;
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
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_3846 = _Time + _TimeEditor;
                float node_2453 = (node_3846.g*0.01);
                float2 node_4891 = (i.uv0*5.0+0.0);
                float3 node_6279 = noise( ((node_4891+node_2453*float2(1,1))*(node_4891+node_2453*float2(-1,-1))) );
                float4 _node_9949_var = tex2D(_node_9949,TRANSFORM_TEX(i.uv0, _node_9949));
                float3 diffuseColor = (((node_6279*0.5)*_node_9949_var.rgb)*_TintColor.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,_node_9949_var.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            float3 noise( float2 UV ){
            float ret = 0;
              int iterations = 6;
              for (int i = 0; i < iterations; ++i)
              {
                 float2 p = floor(UV * (i+1));
                 float2 f = frac(UV * (i+1));
                 f = f * f * (3.0 - 2.0 * f);
                 float n = p.x + p.y * 57.0;
                 float4 noise = float4(n, n + 1, n + 57.0, n + 58.0);
                 noise = frac(sin(noise)*437.585453);
                 ret += lerp(lerp(noise.x, noise.y, f.x), lerp(noise.z, noise.w, f.x), f.y) * ( iterations / (i+1));
              }
              return ret/iterations;
            }
            
            uniform sampler2D _node_9949; uniform float4 _node_9949_ST;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_3846 = _Time + _TimeEditor;
                float node_2453 = (node_3846.g*0.01);
                float2 node_4891 = (i.uv0*5.0+0.0);
                float3 node_6279 = noise( ((node_4891+node_2453*float2(1,1))*(node_4891+node_2453*float2(-1,-1))) );
                float4 _node_9949_var = tex2D(_node_9949,TRANSFORM_TEX(i.uv0, _node_9949));
                float3 diffuseColor = (((node_6279*0.5)*_node_9949_var.rgb)*_TintColor.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * _node_9949_var.a,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
