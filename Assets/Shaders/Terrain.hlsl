#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

TEXTURE2D(_MainTex);
SAMPLER(sampler_MainTex);
float4 _MainTex_ST;

TEXTURE2D(_HeightMap);
SAMPLER(sampler_HeightMap);
float4 _HeightMap_ST;

float _DisplaceStrength;

struct VertexData {
    float3 positionOS : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f {
    float4 positionCS : SV_POSITION;
    float2 uv : TEXCOORD0;
};

v2f vert(VertexData input) {

    v2f o;
    float3 vertPosition = input.positionOS;
    float4 heightVal = SAMPLE_TEXTURE2D_LOD(_HeightMap, sampler_HeightMap, input.uv, 1);
    vertPosition.y += heightVal.r * _DisplaceStrength;

    o.positionCS = GetVertexPositionInputs(vertPosition).positionCS;
    o.uv = TRANSFORM_TEX(input.uv, _MainTex);
    
    return o;
}

float4 frag(v2f input) : SV_TARGET {
    return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
}