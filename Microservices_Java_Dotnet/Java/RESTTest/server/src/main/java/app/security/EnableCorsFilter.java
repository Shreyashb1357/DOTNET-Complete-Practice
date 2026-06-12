package app.security;

import java.io.IOException;

import jakarta.ws.rs.container.ContainerRequestContext;
import jakarta.ws.rs.container.ContainerResponseContext;
import jakarta.ws.rs.container.ContainerResponseFilter;
import jakarta.ws.rs.ext.Provider;

//web-browser only allows client-side script that originates from
//a given endpoint to consume resources exchanged by the same
//endpoint (same origin policy) or from another endpoint which
//permits cross-origin resource sharing (CORS) by sending
//required headers (Access-Control-Allow-*)

@Provider
public class EnableCorsFilter implements ContainerResponseFilter {

    @Override
    public void filter(ContainerRequestContext requestContext, ContainerResponseContext responseContext) throws IOException {
        var headers = responseContext.getHeaders();
        headers.add                ("Access-Control-Allow-Origin", "http://localhost:5001");
        headers.add("Access-Control-Allow-Methods", "*");
        headers.add("Access-Control-Allow-Headers", "*");
    }
    
}
