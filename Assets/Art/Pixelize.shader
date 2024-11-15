Shader "Hidden/Pixelize"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        HLSLINCLUDE
        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        TEXTURE2D(_MainTex);
        float4 _MainTex_TexelSize;
        float4 _MainTex_ST;

        SamplerState sampler_point_clamp;

        uniform float2 _BlockCount;
        uniform float2 _BlockSize;
        uniform float2 _HalfBlockSize;

        v2f vert(appdata v)
        {
            v2f o;
            o.vertex = TransformObjectToHClip(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            return o;
        }
        ENDHLSL

        Pass
        {
            Name "Pixelation"
            HLSLPROGRAM
            half4 frag(v2f i) : SV_Target
            {
                float2 blockPos = floor(i.uv * _BlockCount);
                float2 blockCenter = blockPos * _BlockSize + _HalfBlockSize;

                float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_point_clamp, blockCenter);

                return tex;
            }
            ENDHLSL
        }
    }
}