import type { PayloadAction } from "@reduxjs/toolkit";
import { createSlice } from "@reduxjs/toolkit";
import { initialAuthState, type SessionProps } from "./types";

export const authSlice = createSlice({
  name: "auth",
  initialState: initialAuthState,
  reducers: {
    setSession: (state, action: PayloadAction<SessionProps>) => {
      state.session = action.payload;
    },
    clearSession: (state) => {
      state.session = undefined;
    },
  },
});

export const { setSession, clearSession } = authSlice.actions;

export default authSlice;
