Shader "Custom/DestelloAzul" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionDistance ("Emission Distance", Range(0, 10)) = 1
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _EmissionColor;
            float _EmissionDistance;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                float dist = distance(i.vertex, _WorldSpaceCameraPos);
                float emission = smoothstep(_EmissionDistance, _EmissionDistance + 0.5, dist) * 2;
                fixed4 emissionColor = _EmissionColor * emission;
                col.rgb += emissionColor.rgb;
                col.a *= emissionColor.a;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
