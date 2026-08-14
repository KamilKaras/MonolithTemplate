import { describe, expect, it } from "vitest";
import { buildReturnUrl, sanitizeReturnUrl } from "./safeReturnUrl";

describe("safeReturnUrl", () => {
  it.each([
    "/home",
    "/orders/123",
    "/orders/123?tab=history",
    "/orders/123?tab=history#notes",
  ])("keeps valid internal destination %s", (value) => {
    expect(sanitizeReturnUrl(value)).toBe(value);
  });

  it.each([
    "//evil.example.com",
    "https://evil.example.com",
    "http://evil.example.com",
    "javascript:alert(1)",
    "/%2F%2Fevil.example.com",
    "",
    "   ",
    null,
    undefined,
  ])("falls back safely for invalid input %s", (value) => {
    expect(sanitizeReturnUrl(value)).toBe("/home");
  });

  it("preserves pathname, search and hash through encode/decode", () => {
    const original = "/orders/123?tab=history#notes";

    expect(
      sanitizeReturnUrl(
        decodeURIComponent(
          buildReturnUrl("/orders/123", "?tab=history", "#notes"),
        ),
      ),
    ).toBe(original);
  });

  it("encodes the returnUrl value once", () => {
    expect(buildReturnUrl("/orders/123", "?tab=history", "#notes")).toBe(
      "%2Forders%2F123%3Ftab%3Dhistory%23notes",
    );
  });
});
