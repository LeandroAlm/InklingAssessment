Shader "Custom/Inflatable" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Shininess ("Shininess", Range(0, 1)) = 0.5
        _InflateAmount ("Inflate Amount", Range(0, 1)) = 0.5
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
        sampler2D _BumpMap;
        fixed4 _Color;
        float _Shininess;
        float _InflateAmount;

        struct Input {
            float2 uv_MainTex;
            INTERNAL_DATA
            float3 worldNormal;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Sample the normal map
            float3 normalMap = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));

            float2 dist = IN.uv_MainTex - float2(0.5, 0.5);
            float distSq = dot(dist, dist);
            float inflate = smoothstep(0, 0.25, distSq) * _InflateAmount;
            c.rgb *= 1 + inflate;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Metallic = 0.5;
            o.Smoothness = _Shininess;

            // Calculate lighting
            float3 worldNormal = normalize(IN.worldNormal);
            float3 worldPos = IN.worldPos;
            float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
            o.Normal = normalMap;

            // Diffuse
            float3 lightDir = _WorldSpaceLightPos0 - worldPos;
            float diffuse = max(0, dot(worldNormal, normalize(lightDir)));
            o.Emission = _LightColor0.rgb * diffuse;

            // Specular
            float3 halfwayDir = normalize(lightDir + viewDir);
            float specular = pow(max(0, dot(worldNormal, halfwayDir)), _Shininess * 128);
            o.Emission += _LightColor0.rgb * specular;
        }
        ENDCG
    }
    FallBack "Diffuse"
}