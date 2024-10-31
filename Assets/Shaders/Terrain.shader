Shader "Custom/Terrain"
 {
    Properties {
        [Header(Textures)]
        [MainTexture] _MainTex("Main Texture", 2D) = "white" {}
        _HeightMap("Height Map", 2D) = "grey" {}
        _DisplaceStrength("Displacement strength", Range(0.1, 50)) = 0.5
    }

    SubShader
    {

        Tags { "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "Terrain"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Terrain.hlsl"

            ENDHLSL
        }

    }

 }