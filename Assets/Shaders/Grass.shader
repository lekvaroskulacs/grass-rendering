Shader "Custom/Grass"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Pass{
        
        

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #pragma target 4.5

        #include "UnityPBSLighting.cginc"
        #include "AutoLight.cginc"

        struct VertexData {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        
        v2f vert(VertexData vd) {
            v2f o;
            o.uv = vd.uv, _MainTex;
            o.vertex = vd.vertex;
            return o;
        }

        fixed4 frag(v2f i) : SV_TARGET {
            fixed4 col = tex2D(_MainTex, i.uv);
            clip(-(0.5 - col.a));
            float3 lightDir = _WorldSpaceLightPos0.xyz;
            float ndotl = DotClamped(lightDir, normalize(float3(0, 1, 0)));
            
            return col * ndotl;
        }   


        ENDCG
    }
    }

    FallBack "Diffuse"
}
