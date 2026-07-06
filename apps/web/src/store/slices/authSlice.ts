import { createSlice } from "@reduxjs/toolkit";
import { initialAuthState } from "./types";

export const authSlice = createSlice({
  name: "auth",
  initialState: initialAuthState,
  reducers: {},
});

export default authSlice;
