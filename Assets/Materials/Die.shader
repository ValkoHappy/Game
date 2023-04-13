Shader "Custom/MyShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _BumpMap ("Normal Map", 2D) = "bump" {}

        _GradientColor1 ("Gradient Color 1", Color) = (1,0,0,1)
        _GradientColor2 ("Gradient Color 2", Color) = (0,1,0,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _GradientColor1;
        fixed4 _GradientColor2;

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
            float3 worldViewDir;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Calculate the gradient color based on the y position of the vertex
            float3 gradientColor = lerp(_GradientColor1.rgb, _GradientColor2.rgb, IN.worldPos.y);

            // Combine the gradient color and the base color
            fixed4 finalColor = fixed4(lerp(_Color.rgb, gradientColor, _Color.a), _Color.a);

            // Set the diffuse color for the material
            o.Albedo = finalColor.rgb;

            // Set the transparency of the material
            o.Alpha = finalColor.a;

            // Set the normal map for the material
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex).rgb);

            // Set the roughness and metallic values for the material
            o.Smoothness = 0.5;
            o.Metallic = 0.5;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
