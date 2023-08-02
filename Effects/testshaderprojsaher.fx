sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float2 uTargetPosition;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;

sampler _GradientTexture : register(s3);
    
float4 testshaderproj(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float2 texture = Tex2D(_GradientTexture, coords);
    float2 newtexture = lerp(float4(0,0,0,0),clamp(sin(sampleColor * uTime)) , texture);
    return newtexture;
}
    
technique Technique1
{
    pass testshaderproj
    {
        PixelShader = compile ps_2_0 testshaderproj();
    }
}