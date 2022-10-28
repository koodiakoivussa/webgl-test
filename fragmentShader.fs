    precision mediump float;
    
    varying vec3 fragColor;
    uniform float uTime;
    uniform vec2 uResolution;
    
    /*
    Voronoi test based on The Art of Codes(youtube) example:
    https://www.youtube.com/watch?v=l-07BXzNdPw
    */

    vec2 n22(vec2 p){
      vec3 a = fract(p.xyx*vec3(123.34, 234.34, 345.56));
      a += dot(a, a+34.45);
      return fract(vec2(a.x*a.y, a.y*a.z));
    }

    void main()
    {
      vec2 res = uResolution;
      res = res / res;
      vec2 uv = (2. * fragColor.xy - res.xy) / res.y;
      
      float m = 0.;
      float t = uTime;
      float minDist = 100.;

      for(float i=1.; i <50.; i++){
         vec2 n = n22(vec2(i, i));
         vec2 p = sin(n*t);
         float d = length(uv-p);
         m += smoothstep(.02, .01, d);
         if (d<minDist){
            minDist = d;
         }
      }

      vec3 col = vec3(minDist);
      vec3 colors = fragColor + 0.25 + 0.25 * vec3(cos(uTime + fragColor.xyx + vec3(0,3,6)));
      
      gl_FragColor = vec4(col, 1);
    }
