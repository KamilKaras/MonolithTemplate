import type { PayloadAction } from "@reduxjs/toolkit";
import { createSlice } from "@reduxjs/toolkit";
import type { User } from "../../../../shared/types";

export interface CredentialsState {
  user: User | undefined;
}

const initialState: CredentialsState = {
  user: undefined,
};

export const meSlice = createSlice({
  name: "me",
  initialState,
  reducers: {
    setCredentials: (state, action: PayloadAction<User>) => {
      state.user = action.payload;
    },
  },
});

export const { setCredentials } = meSlice.actions;

export default meSlice;
