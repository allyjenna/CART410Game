Shader "Custom/GlitchShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlitchAmount ("Glitch Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GlitchAmount;

            // Simple pseudo-random function based on UV coordinates
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // Apply glitch effect
                if (rand(i.uv) < _GlitchAmount)
                {
                    col.rgb = float4(rand(i.uv), rand(i.uv), rand(i.uv), 1.0); // Random color
                }
                return col;
            }
            ENDCG
        }
    }
}
