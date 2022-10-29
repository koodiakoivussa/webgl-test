    precision mediump float;
    
    varying vec3 fragColor;
    uniform float uTime;
    uniform vec2 uResolution;


    float r21(vec2 p){
        p = fract(p*vec2(123.34, 456.21));
        p += dot(p, p+45.32);
        return fract(p.x*p.y);
    }

    float createOrb(vec2 uv, float glow){
        float distanceToCenter = length(uv);
        float orb = glow/distanceToCenter;
        orb *= smoothstep(1.0, 0.0, distanceToCenter);
        return orb;
    }

    void main()
    {
        // Normalize resolution and coordinates
        vec2 res = uResolution / uResolution;
        vec2 uv = fract((fragColor.xy*res.xy) / res.y);

        float scale = 20.;

        vec2 grid = 0.5-fract(uv * scale);
        vec2 id = floor(uv * scale);
        vec3 o = vec3(0.);

        

        for(int y=-1; y <= 1; y++){

            for(int x=-1; x <= 1; x++){

                vec2 offset = vec2(x, y);                
                float n = r21(id+offset);
                float size = fract(n*345.25);

                float orb = createOrb( grid + offset - vec2(n, fract(n*12.)) + .5, .03);
                vec3 color = sin(vec3(.2, .3, .9) * fract(n*2345.45)*123.3)*.5+.5;
                color = color*vec3(1, 0, 1);

                o += orb * size * color;
                
            }
        }

        gl_FragColor = vec4(o, 1.);
    }
