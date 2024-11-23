Shader "Custom/GradientBackground"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (0.5, 0.8, 1, 1)
        _Color2 ("Color 2", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color1;
            float4 _Color2;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float gradient = i.uv.y; // Simple vertical gradient
                return lerp(_Color1, _Color2, gradient);
            }
            ENDCG
        }
    }
}
