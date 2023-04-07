sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1; //grayish
float2 uImageSize2; // greenish
float2 uImageSize3; //black
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float4 gl_FragCoord;
float2 uZoom;


float4 FilterMyShader(float2 coords : TEXCOORD0, float2 uv : SV_Position) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    color.xy = uv.yx;
    return color;
}

technique Technique1
{
    pass sstest
    {
        PixelShader = compile ps_2_0 FilterMyShader();
    }
}
