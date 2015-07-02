// Shader created with Shader Forge Beta 0.33 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.33;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:5,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:False,aust:False,igpj:False,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32267,y:32717|custl-196-RGB,alpha-198-A;n:type:ShaderForge.SFN_Tex2d,id:196,x:32608,y:32681,ptlb:textura,ptin:_textura,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:2,isnm:False|UVIN-1004-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:198,x:32591,y:32878,ptlb:slpah_scroll X,ptin:_slpah_scrollX,tex:e6dd57b3ff4b1864dba181b4489eeeb6,ntxv:2,isnm:False|UVIN-250-UVOUT;n:type:ShaderForge.SFN_Panner,id:250,x:32799,y:32878,spu:0.5,spv:0|DIST-388-A;n:type:ShaderForge.SFN_Tex2d,id:388,x:32608,y:33121,ptlb:alpha_scroll Y,ptin:_alpha_scrollY,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:1,isnm:False|UVIN-390-UVOUT;n:type:ShaderForge.SFN_Panner,id:390,x:32824,y:33121,spu:-0.1,spv:-2;n:type:ShaderForge.SFN_Panner,id:1004,x:32812,y:32661,spu:-1,spv:0;proporder:196-198-388;pass:END;sub:END;*/

Shader "flux/AlphaScroll" {
    Properties {
        _textura ("textura", 2D) = "black" {}
        _slpah_scrollX ("slpah_scroll X", 2D) = "black" {}
        _alpha_scrollY ("alpha_scroll Y", 2D) = "gray" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _textura; uniform float4 _textura_ST;
            uniform sampler2D _slpah_scrollX; uniform float4 _slpah_scrollX_ST;
            uniform sampler2D _alpha_scrollY; uniform float4 _alpha_scrollY_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float4 node_1269 = _Time + _TimeEditor;
                float2 node_1268 = i.uv0;
                float2 node_1004 = (node_1268.rg+node_1269.g*float2(-1,0));
                float3 finalColor = tex2D(_textura,TRANSFORM_TEX(node_1004, _textura)).rgb;
                float2 node_390 = (node_1268.rg+node_1269.g*float2(-0.1,-2));
                float4 node_388 = tex2D(_alpha_scrollY,TRANSFORM_TEX(node_390, _alpha_scrollY));
                float2 node_250 = (node_1268.rg+node_388.a*float2(0.5,0));
                float4 node_198 = tex2D(_slpah_scrollX,TRANSFORM_TEX(node_250, _slpah_scrollX));
/// Final Color:
                return fixed4(finalColor,node_198.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
