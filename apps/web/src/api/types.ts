export type FetchResult<T> =
  | { ok: true; data: T }
  | { ok: false; error: string };
