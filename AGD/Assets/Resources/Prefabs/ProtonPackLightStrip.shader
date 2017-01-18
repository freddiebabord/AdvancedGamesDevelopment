// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:1,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:8697,x:34011,y:32683,varname:node_8697,prsc:2|diff-7979-OUT,spec-3924-R,normal-9143-RGB,emission-512-OUT,difocc-8589-R;n:type:ShaderForge.SFN_Tex2d,id:9980,x:32591,y:32256,ptovrint:False,ptlb:Albedo,ptin:_Albedo,varname:node_9980,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:872f872224e8e83459c63ce99620f477,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9143,x:33457,y:32805,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_9143,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f95bbfaf07a8f8342b48bd166a3f7abd,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:8589,x:33457,y:33153,ptovrint:False,ptlb:Occlusion,ptin:_Occlusion,varname:node_8589,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a6ac996efd70ae24086dd345d93fe6aa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:858,x:32971,y:33069,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_858,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:94cdfb517100a3e4899e287066d0a986,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Color,id:6882,x:32568,y:32643,ptovrint:False,ptlb:OnColour,ptin:_OnColour,varname:node_6882,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.03448272,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:7979,x:33458,y:32393,varname:node_7979,prsc:2|A-9980-RGB,B-9329-OUT;n:type:ShaderForge.SFN_TexCoord,id:2267,x:32670,y:33913,varname:node_2267,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:2038,x:33073,y:33869,varname:node_2038,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2970-OUT;n:type:ShaderForge.SFN_Color,id:1271,x:32568,y:32841,ptovrint:False,ptlb:OffColour,ptin:_OffColour,varname:_OnColour_copy,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:9329,x:32938,y:32766,varname:node_9329,prsc:2|A-6882-RGB,B-1271-RGB,T-9388-OUT;n:type:ShaderForge.SFN_Multiply,id:512,x:33457,y:32990,varname:node_512,prsc:2|A-9329-OUT,B-858-RGB;n:type:ShaderForge.SFN_Slider,id:67,x:31957,y:33332,ptovrint:False,ptlb:Capacity,ptin:_Capacity,varname:node_67,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:3924,x:33458,y:32608,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_3924,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:afe42b813961f864bbc7a3ad8679f55b,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Add,id:8853,x:33314,y:33753,varname:node_8853,prsc:2|A-151-OUT,B-2038-OUT;n:type:ShaderForge.SFN_OneMinus,id:1228,x:32748,y:34160,varname:node_1228,prsc:2|IN-2267-V;n:type:ShaderForge.SFN_Subtract,id:151,x:33074,y:33681,varname:node_151,prsc:2|A-9479-OUT,B-1305-OUT;n:type:ShaderForge.SFN_Vector1,id:1305,x:32856,y:33822,varname:node_1305,prsc:2,v1:1;n:type:ShaderForge.SFN_Ceil,id:9388,x:33518,y:33753,varname:node_9388,prsc:2|IN-8853-OUT;n:type:ShaderForge.SFN_Vector1,id:4012,x:31963,y:33675,varname:node_4012,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3766,x:31976,y:33763,varname:node_3766,prsc:2,v1:6;n:type:ShaderForge.SFN_Divide,id:8909,x:32176,y:33689,varname:node_8909,prsc:2|A-4012-OUT,B-3766-OUT;n:type:ShaderForge.SFN_Divide,id:2392,x:32422,y:33550,varname:node_2392,prsc:2|A-1786-OUT,B-8909-OUT;n:type:ShaderForge.SFN_Round,id:3179,x:32607,y:33550,varname:node_3179,prsc:2|IN-2392-OUT;n:type:ShaderForge.SFN_Multiply,id:9479,x:32856,y:33669,varname:node_9479,prsc:2|A-3179-OUT,B-8909-OUT;n:type:ShaderForge.SFN_OneMinus,id:1786,x:32207,y:33413,varname:node_1786,prsc:2|IN-67-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:2970,x:33004,y:34228,ptovrint:False,ptlb:ReverseDirection,ptin:_ReverseDirection,varname:node_2970,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-2267-V,B-1228-OUT;n:type:ShaderForge.SFN_Fresnel,id:8291,x:31865,y:32875,varname:node_8291,prsc:2|EXP-2733-OUT;n:type:ShaderForge.SFN_Slider,id:2733,x:31446,y:32947,ptovrint:False,ptlb:Edge Blend,ptin:_EdgeBlend,varname:node_8509,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.5,max:5;n:type:ShaderForge.SFN_Color,id:1729,x:31820,y:32703,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_1173,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0,c3:0,c4:0;n:type:ShaderForge.SFN_Tex2d,id:4540,x:32192,y:32569,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-3477-UVOUT;n:type:ShaderForge.SFN_Panner,id:3477,x:32069,y:32245,varname:node_3477,prsc:2,spu:1,spv:1|UVIN-504-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:504,x:31798,y:32236,varname:node_504,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9730,x:31883,y:32440,varname:node_9730,prsc:2|A-2588-T,B-4913-OUT;n:type:ShaderForge.SFN_Time,id:2588,x:31533,y:32345,varname:node_2588,prsc:2;n:type:ShaderForge.SFN_Slider,id:4913,x:31352,y:32589,ptovrint:False,ptlb:Pan Speed,ptin:_PanSpeed,varname:_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_DepthBlend,id:3008,x:32522,y:33278,varname:node_3008,prsc:2|DIST-7223-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7223,x:32113,y:33440,ptovrint:False,ptlb:DepthBelnd,ptin:_DepthBelnd,varname:node_1061,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:3276,x:32756,y:33119,varname:node_3276,prsc:2|A-1342-OUT,B-3008-OUT,C-7086-OUT;n:type:ShaderForge.SFN_Multiply,id:9913,x:32250,y:33152,varname:node_9913,prsc:2|A-1729-RGB,B-6175-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6175,x:31991,y:33299,ptovrint:False,ptlb:SoftColour,ptin:_SoftColour,varname:node_9201,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Lerp,id:1342,x:32503,y:33030,varname:node_1342,prsc:2|A-9913-OUT,B-1729-RGB,T-8291-OUT;n:type:ShaderForge.SFN_Multiply,id:7086,x:32728,y:32661,varname:node_7086,prsc:2|A-3826-OUT,B-8291-OUT;n:type:ShaderForge.SFN_Vector1,id:112,x:32337,y:32849,varname:node_112,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:3826,x:32524,y:32748,varname:node_3826,prsc:2|A-4540-RGB,B-112-OUT;proporder:6882-1271-67-2970-9980-9143-8589-858-3924;pass:END;sub:END;*/

Shader "Custom/ProtonPackLightStrip" {
    Properties {
        [HDR]_OnColour ("OnColour", Color) = (0.03448272,1,0,1)
        [HDR]_OffColour ("OffColour", Color) = (1,0,0,1)
        _Capacity ("Capacity", Range(0, 1)) = 1
        [MaterialToggle] _ReverseDirection ("ReverseDirection", Float ) = 1
        _Albedo ("Albedo", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Occlusion ("Occlusion", 2D) = "white" {}
        _Emission ("Emission", 2D) = "black" {}
        _Specular ("Specular", 2D) = "black" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "DEFERRED"
            Tags {
                "LightMode"="Deferred"
            }
            
            
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
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Occlusion; uniform float4 _Occlusion_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float4 _OnColour;
            uniform float4 _OffColour;
            uniform float _Capacity;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform fixed _ReverseDirection;
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
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.5;
/////// GI Data:
                UnityLight light; // Dummy light
                light.color = 0;
                light.dir = half3(0,1,0);
                light.ndotl = max(0,dot(normalDirection,light.dir));
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = 1;
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
////// Specular:
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float3 specularColor = _Specular_var.r;
                float specularMonochrome;
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float node_8909 = (1.0/6.0);
                float3 node_9329 = lerp(_OnColour.rgb,_OffColour.rgb,ceil((((round(((1.0 - _Capacity)/node_8909))*node_8909)-1.0)+lerp( i.uv0.g, (1.0 - i.uv0.g), _ReverseDirection ).r)));
                float3 diffuseColor = (_Albedo_var.rgb*node_9329); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
/////// Diffuse:
////// Emissive:
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float3 emissive = (node_9329*_Emission_var.rgb);
/// Final Color:
                float4 _Occlusion_var = tex2D(_Occlusion,TRANSFORM_TEX(i.uv0, _Occlusion));
                outDiffuse = half4( diffuseColor, _Occlusion_var.r );
                outSpecSmoothness = half4( specularColor, 0.5 );
                outNormal = half4( normalDirection * 0.5 + 0.5, 1 );
                outEmission = half4( (node_9329*_Emission_var.rgb), 1 );
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Occlusion; uniform float4 _Occlusion_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float4 _OnColour;
            uniform float4 _OffColour;
            uniform float _Capacity;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform fixed _ReverseDirection;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float3 specularColor = _Specular_var.r;
                float specularMonochrome;
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float node_8909 = (1.0/6.0);
                float3 node_9329 = lerp(_OnColour.rgb,_OffColour.rgb,ceil((((round(((1.0 - _Capacity)/node_8909))*node_8909)-1.0)+lerp( i.uv0.g, (1.0 - i.uv0.g), _ReverseDirection ).r)));
                float3 diffuseColor = (_Albedo_var.rgb*node_9329); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz)*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Occlusion_var = tex2D(_Occlusion,TRANSFORM_TEX(i.uv0, _Occlusion));
                indirectDiffuse *= _Occlusion_var.r; // Diffuse AO
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float3 emissive = (node_9329*_Emission_var.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float4 _OnColour;
            uniform float4 _OffColour;
            uniform float _Capacity;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform fixed _ReverseDirection;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float3 specularColor = _Specular_var.r;
                float specularMonochrome;
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float node_8909 = (1.0/6.0);
                float3 node_9329 = lerp(_OnColour.rgb,_OffColour.rgb,ceil((((round(((1.0 - _Capacity)/node_8909))*node_8909)-1.0)+lerp( i.uv0.g, (1.0 - i.uv0.g), _ReverseDirection ).r)));
                float3 diffuseColor = (_Albedo_var.rgb*node_9329); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
