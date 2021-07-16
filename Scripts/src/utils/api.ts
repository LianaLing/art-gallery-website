import { ApiResponse } from "../types/api";

/**
 * Fetch an endpoint with typed response
 * @param request - information about the request
 * @returns the typed response received
 */
const http = async <T>(request: RequestInfo): Promise<ApiResponse<T>> => {
  try {
    const response = await fetch(request);
    const responseJSON: { d: string } = await response.json();
    const { data, error } = JSON.parse(responseJSON.d) as ApiResponse<T>;
    return { data, error };
  } catch (err) {
    return { data: null, error: err };
  }
};

/**
 * Make a POST request to the specified endpoint
 * @param path - the API Endpoint to call
 * @param body - the body params of the request (default to json)
 * @param args - override request arguments
 * @returns
 */
export async function post<T>(
  path: string,
  body: any,
  args: RequestInit = {
    method: "post",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  }
): Promise<ApiResponse<T>> {
  return await http<T>(new Request(path, args));
}
