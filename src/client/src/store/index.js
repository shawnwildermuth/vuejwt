import { createStore } from 'vuex'
import createHttp from "@/services/http";
import router from "@/router";

export default createStore({
  state: {
    colors: [],
    token: "",
    expiration: Date.now(),
    isBusy: false,
    error: ""
  },
  mutations: {
    setColors: (state, colors) => state.colors = colors,
    setBusy: (state) => state.isBusy = true,
    clearBusy: (state) => state.isBusy = false,
    setError: (state, error) => state.error = error,
    clearError: (state) => state.error = "",
    setToken: (state, model) => {
      state.token = model.token;
      state.expiration = new Date(model.expiration)
    },
    clearToken: (state) => {
      state.token = "";
      state.expiration = Date.now();
    }
  },
  getters: {
    isAuthenticated: (state) => state.token.length > 0 && state.expiration > Date.now()
  }, 
  actions: {
    loadColors: async ({ commit }) => {
      try {
        commit("setBusy");
        commit("clearError");
        const http = createHttp(); // secured
        const colors = await http.get("/api/colors");
        commit("setColors", colors.data);
      } catch {
        commit("setError", "Failed getting colors");
      } finally {
        commit("clearBusy");
      }
    }, 
    login: async ({ commit }, model) => {
      try {
        commit("setBusy");
        commit("clearError");
        const http = createHttp(false); // unsecured
        const result = await http.post("/api/auth", model);
        if (result.data.success) {
          commit("setToken", result.data);
          router.push("/");
        }
        else {
          commit("setError", "Authentication Failed");
        }
      } catch {
        commit("setError", "Failed to login");
      } finally {
        commit("clearBusy");
      }
    },
    logout: ({ commit }) => {
      commit("clearToken");
      router.push("/");
    }
  }
})
