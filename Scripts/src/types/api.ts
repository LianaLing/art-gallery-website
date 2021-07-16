/**
 * This is the general structure of an api response returned from backend.
 * A model type (eg. Art) should be provided as generic to ensure
 * all data are properly typed.
 */
export type ApiResponse<T> = {
  data: T | null;
  error: Error | null;
};

// This is the general structure of an error returned from backend
export type Error = {
  errorType: string;
  errorCode: string;
  message: string;
  statusCode: number;
};
