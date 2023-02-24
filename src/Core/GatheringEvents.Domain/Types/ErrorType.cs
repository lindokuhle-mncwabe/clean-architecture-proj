namespace GatheringEvents.Domain.Types;

public enum ErrorType
{
    // Request succeeded and that the requested information is in the response. 
    Ok = 200,

    // Request resulted in a new resource created before the response was sent.
    Created = 201,

    // Request has been accepted for further processing.
    Accepted = 202,

    // Returned meta information is from a cached copy instead of the origin server and therefore may be incorrect.
    NonAuthoritativeInformation = 203,

    // Request has been successfully processed and that the response is intentionally blank.
    NoContent = 204,

    // Client should reset (not reload) the current resource.
    ResetContent = 205,

    // Response is a partial response as requested by a GET request that includes a byte range.
    PartialContent = 206,

    // multiple status codes for a single response during a Web Distributed Authoring and Versioning 
    // (WebDAV) operation. The response body contains XML that describes the status codes.
    MultiStatus = 207,

    // members of a WebDAV binding have already been enumerated in a preceding part of the multistatus response, 
    // and are not being included again.
    AlreadyReported = 208,

    // Requested information has multiple representations.
    Ambiguous = 300,

    // Requested information has been moved to the URI specified in the Location header. 
    // The default action when this status is received is to follow the Location header associated with the response. 
    Moved = 301,   

    // Requested information is located at the URI specified in the Location header. 
    // The default action when this status is received is to follow the Location header associated with the response. 
    Redirect = 302,

    // Automatically redirects the client to the URI specified in the Location header as the result of a POST. 
    SeeOther = 303,

    // Client's cached copy is up to date. The contents of the resource are not transferred.
    NotModified = 304,

    // Request should use the proxy server at the URI specified in the Location header.
    UseProxy = 305,   

    // Request information is located at the URI specified in the Location header. 
    // The default action when this status is received is to follow the Location header associated with the response. 
    TemporaryRedirect = 307,

    // Request information is located at the URI specified in the Location header. 
    // The default action when this status is received is to follow the Location header associated with the response. 
    PermanentRedirect = 308,

    // Request could not be understood by the server. BadRequest is sent when no other error is applicable, 
    // or if the exact error is unknown or does not have its own error code.
    BadRequest = 400,

    // Requested resource requires authentication. 
    Unauthorized = 401,

    // PaymentRequired is reserved for future use.
    PaymentRequired = 402,

    // Server refuses to fulfill the request.
    Forbidden = 403,

    // Requested resource does not exist on the server.
    NotFound = 404,

    // Request method (POST or GET) is not allowed on the requested resource.
    MethodNotAllowed = 405,

    // Client has indicated with Accept headers that it will not accept any of the available representations of the resource.
    NotAcceptable = 406,

    // Requested proxy requires authentication. 
    // The Proxy-authenticate header contains the details of how to perform the authentication.
    ProxyAuthenticationRequired = 407,    

    // Client did not send a request within the time the server was expecting the request.
    RequestTimeout = 408,   

    // Request could not be carried out because of a conflict on the server.
    Conflict = 409,

    // Requested resource is no longer available.
    Gone = 410,

    // Required Content-length header is missing.
    LengthRequired = 411,

    // Condition set for this request failed, and the request cannot be carried out. 
    // Conditions are set with conditional request headers like If-Match, If-None-Match, or If-Unmodified-Since.
    PreconditionFailed = 412,

    // Request is too large for the server to process.
    RequestEntityTooLarge = 413,

    // Indicates that the URI is too long.
    RequestUriTooLong = 414,

    // Indicates that the request is an unsupported type.
    UnsupportedMediaType = 415,

    // The range of data requested from the resource cannot be returned, 
    // either because the beginning of the range is before the beginning of the resource,
    // or the end of the range is after the end of the resource.
    RequestedRangeNotSatisfiable = 416,

    // An expectation given in an Expect header could not be met by the server.
    ExpectationFailed = 417,

    // A generic error has occurred on the server.
    InternalServerError = 500,
    
    // The server does not support the requested function.
    NotImplemented = 501,

    // An intermediate proxy server received a bad response from another proxy or the origin server.
    BadGateway = 502,

    // The server is temporarily unavailable, usually due to high load or maintenance.
    ServiceUnavailable = 503,

    // An intermediate proxy server timed out while waiting for a response from another proxy or the origin server.
    GatewayTimeout = 504,

    // The requested HTTP version is not supported by the server.
    HttpVersionNotSupported = 505,

    // The chosen variant resource is configured to engage in transparent content negotiation itself and, 
    // therefore, isn't a proper endpoint in the negotiation process.
    VariantAlsoNegotiates = 506,

    // Server is unable to store the representation needed to complete the request.
    InsufficientStorage = 507,

    // Server terminated an operation because it encountered an infinite loop while processing.
    LoopDetected = 508,

    // Further extensions to the request are required for the server to fulfill it.
    NotExtended = 510,

    // Client needs to authenticate to gain network access; 
    // it's intended for use by intercepting proxies used to control access to the network.
    NetworkAuthenticationRequired = 511
}