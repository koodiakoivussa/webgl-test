    precision mediump float;
    
    varying vec3 fragColor;
    uniform float uTime;
    uniform vec2 uResolution;

    /*
    Shader test based on The Art of Codes(youtube) tutorial
    https://www.youtube.com/watch?v=rvDo9LvfoVE
    */

    mat2 Rotate(float a){
        float s=sin(a), c=cos(a);
        return mat2(c, -s, s, c);
    }

    float r21(vec2 p){
        p = fract(p*vec2(123.34, 456.21));
        p += dot(p, p+45.32);
        return fract(p.x*p.y);
    }

    float createOrb(vec2 uv, float glow){
        float distanceToCenter = length(uv);
        float orb = glow/distanceToCenter;
        orb *= smoothstep(1., 0., distanceToCenter);
        return orb;
    }

    vec3 createOrbfield(vec2 uv, float scale){

        vec3 orbfield = vec3(0.);

        vec2 grid = 0.5-fract(uv * scale);
        vec2 id = floor(uv * scale);       

        for(int y=-1; y <= 1; y++){

            for(int x=-1; x <= 1; x++){

                vec2 offset = vec2(x, y);                
                float n = r21(id+offset);
                float size = fract(n*345.25);

                float orb = createOrb( grid + offset - vec2(n, fract(n*12.)) + .5, .05);
                vec3 color = sin(vec3(.2, .3, .9) * fract(n*2345.45)*123.3)*.5+.5;
                color = color*vec3(1., 1.0, 0.+size);

                orb *= sin(uTime*3.+n*6.2831)*.5+1.;

                orbfield += orb * size * color;
                
            }
        }

        return orbfield;
    }

    void main()
    {
        // Normalize resolution and coordinates
        vec2 res = uResolution / uResolution;        
        vec2 uv = .5-fract((fragColor.xy*res.xy) / res.y);    

        // This mess somehow holds the aspect ratio together and I have no idea why
        float dimy = uResolution.y/uResolution.x;
        float dimx = uResolution.x/uResolution.y;
        if (dimx > dimy){
            uv.y *= dimy;
        }
        if (dimx < dimy) {
            uv.x *= dimx;
        }
        
        float time = uTime * 0.1;
        uv *= Rotate(time);
        vec3 o = vec3(0.);
        float layers = 5.;
        
        for(float i = 0.; i < 1.; i += 1./8.){
            float depth = fract(i+time);
            float fade = depth*smoothstep(1., .9, depth);
            float scale = mix(20., .5, depth);
            o += createOrbfield(uv * scale + i * 432.12, 2.)*fade;
        }
        
        gl_FragColor = vec4(o, 1.);
    }
