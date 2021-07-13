type HttpResponse<T> = Response & {
  data?: T;
};

/**
 * Fetch an endpoint with typed response
 * @param request - information about the request
 * @returns the typed response received
 */
export const http = async <T>(
  request: RequestInfo
): Promise<HttpResponse<T>> => {
  const response: HttpResponse<T> = await fetch(request);

  try {
    // catch error if there is one
    response.data = await response.json();
  } catch (err) {
    throw new Error(response.statusText);
  }

  return response;
};

export async function get<T>(
  path: string,
  args: RequestInit = { method: "get" }
): Promise<HttpResponse<T>> {
  return await http<T>(new Request(path, args));
}

export async function post<T>(
  path: string,
  body: any,
  args: RequestInit = {
    method: "post",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  }
): Promise<HttpResponse<T>> {
  return await http<T>(new Request(path, args));
}

export async function put<T>(
  path: string,
  body: any,
  args: RequestInit = {
    method: "put",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  }
): Promise<HttpResponse<T>> {
  return await http<T>(new Request(path, args));
}
